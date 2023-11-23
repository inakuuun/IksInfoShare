using ComBase.Events;
using ComBase.Logs;
using ComBase.Msg;
using ComBase.Tcp;
using IksNativeClient.Common.Msg.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ComBase.Common;
using static ComBase.Common.StractDef;

namespace IksNativeClient.Common.Tcp
{
    /// <summary>
    /// TCPクライアント基底クラス
    /// </summary>
    public abstract class TcpClientBase : TcpBase
    {
        /// <summary>
        /// ログファイル名
        /// </summary>
        private string _logFileName { get => typeof(TcpClientBase).Name ?? string.Empty; }

        /// <summary>
        /// TCP接続情報
        /// </summary>
        private TcpConnectInfo _connectInfo = new();

        /// <summary>
        /// TCPクライアントコントローラー
        /// </summary>
        private TcpController? _tcpClient;

        /// <summary>
        /// ヘルスチェック要求メッセージクラス
        /// </summary>
        private HelthCheckReq _helthCheckReq = new();

        /// <summary>
        /// ヘルスチェック応答メッセージクラス
        /// </summary>
        private HelthCheckRes _helthCheckRes = new();

        /// <summary>
        /// 接続開始
        /// </summary>
        /// <param name="connectInfo">TCP接続情報インスタンス</param>
        protected override void ConnectStart(TcpConnectInfo connectInfo)
        {
            // 接続情報インスタンスを設定
            _connectInfo = connectInfo;
            // ヘルスチェックが必要な場合
            if (_connectInfo.IsHelthCheck)
            {
                // ヘルスチェック処理
                HelthCheck();
            }
            else
            {
                // TCPコネクション確立
                Connection();
            }
        }

        /// <summary>
        /// TCPコネクション確立
        /// </summary>
        sealed protected override void Connection()
        {
            // -------------------------------------------------
            // サーバーとTCP接続
            // サーバーから接続があるまで待機
            // -------------------------------------------------
            // クライアントコントローラーを生成
            _tcpClient = new TcpController(TCP.CLIENT);
            // 接続を維持するためにwhile文が必要そう
            while (true)
            {
                try
                {
                    // TCPコネクション確立
                    _tcpClient?.Connect(_connectInfo);
                    // クライアントはリッスンしないため、別スレッドでメッセージの受信を制御することができない
                    while (true)
                    {
                        try
                        {
                            // TCP接続状態をON
                            CommonDef.IsTcpClientConnected = true;
                            // サーバーからの受信を待機
                            byte[]? message = _tcpClient?.TcpRead();
                            // TCP内部電文処理
                            if (message is not null)
                            {
                                TcpReceivedSend(new MsgBase(message));
                            }
                        }
                        catch (Exception ex)
                        {
                            // TCP接続状態をOFF
                            CommonDef.IsTcpClientConnected = false;
                            // 電文送受信用インスタンスを開放
                            // ※NetStreamのみ開放し、コネクションは開放しない。
                            _tcpClient?.Close();
                            Log.Trace(_logFileName, LOGLEVEL.WARNING, $"{ex.Message}");
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // TCP接続状態をOFF
                    CommonDef.IsTcpClientConnected = false;
                    // 電文送受信用インスタンスを開放
                    // ※NetStreamのみ開放し、コネクションは開放しない。
                    _tcpClient?.Close();
                    Log.Trace(_logFileName, LOGLEVEL.WARNING, $"TCPコネクション確立時異常（クライアント）{ex}");
                    // サーバーへデータを送信する時間を指定時間遅らせる
                    // ※TCPコネクション確立処理で落ちる可能性もあるため、エラー時に指定秒数処理を遅延させる
                    // ※エラー発生時、待機時間が平均的に2秒遅いため「インターバル - 2秒」を設定
                    //_ = new Timer(new TimerCallback(ReConnect), null, 10000, Timeout.Infinite);
                    Thread.Sleep(_connectInfo.HelthCheckInterval - 2000);
                }
            }
        }

        /// <summary>
        /// ヘルスチェック処理
        /// </summary>
        sealed protected override void HelthCheck()
        {
            // -------------------------------------------------
            // サーバーとTCP接続＆ヘルスチェック
            // サーバーへ接続要求を送信する
            // -------------------------------------------------
            // 遅延判定フラグ
            // ※遅延が必要かを判定するフラグ
            bool needDelay = true;
            // クライアントコントローラーを生成
            _tcpClient = new TcpController(TCP.CLIENT);
            // 接続を維持するためにwhile文が必要そう
            while (true)
            {
                try
                {
                    // TCPコネクション確立
                    _tcpClient?.Connect(_connectInfo);
                    // 通信異常がない間ループ処理を実施
                    while (true)
                    {
                        // TCP接続状態をON
                        CommonDef.IsTcpClientConnected = true;
                        // 遅延が必要な場合
                        // ※エラー発生時に遅延処理を入れるため、エラー発生後1回目の処理は遅延処理を実施しない
                        if (needDelay)
                        {
                            // サーバーへデータを送信する時間を指定時間遅らせる
                            Thread.Sleep(_connectInfo.HelthCheckInterval);
                        }
                        // サーバーへ送信するデータ
                        _helthCheckReq = new();
                        // TCP電文送信処理
                        _tcpClient?.TcpSend(_helthCheckReq);
                        // TCP受信電文取得処理
                        byte[]? receivedData = _tcpClient?.TcpRead();
                        if (receivedData is not null)
                        {
                            _helthCheckRes = new HelthCheckRes(receivedData);
                            // ヘルスチェック内部電文処理
                            OnHelthCheck(_helthCheckRes);
                        }
                        // エラーからの復帰の場合にフラグを更新する必要あり
                        // ※while文中の処理が正常の場合は「true」を維持
                        if (!needDelay)
                        {
                            needDelay = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Trace(_logFileName, LOGLEVEL.WARNING, $"コネクション確立時異常 => {_connectInfo.IpAddress}:{_connectInfo.Port} {ex}");
                    // TCP接続状態をOFF
                    CommonDef.IsTcpClientConnected = false;
                    // 正常処理の遅延処理を実施しないようにfalseを設定
                    needDelay = false;
                    // 電文送受信用インスタンスを開放
                    // ※NetStreamのみ開放し、コネクションは開放しない。
                    _tcpClient?.Close();
                    // サーバーへデータを送信する時間を指定時間遅らせる
                    // ※TCPコネクション確立処理で落ちる可能性もあるため、エラー時に指定秒数処理を遅延させる
                    // ※エラー発生時、待機時間が平均的に2秒遅いため「インターバル - 2秒」を設定
                    //_ = new Timer(new TimerCallback(ReConnect), null, 10000, Timeout.Infinite);
                    Thread.Sleep(_connectInfo.HelthCheckInterval - 2000);
                }
            }
        }

        /// <summary>
        /// TCP電文送信処理
        /// </summary>
        /// <param name="msg">TCP電文送信メッセージ</param>
        protected void TcpSend(MsgBase msg)
        {
            _tcpClient?.TcpSend(msg);
        }

        /// <summary>
        /// TCP内部電文送信処理
        /// </summary>
        /// <param name="msg">内部電文送信メッセージ</param>
        private new void TcpReceivedSend(MsgBase msg)
        {
            // 基底クラスのTCP内部電文受信イベントを実行させる
            base.TcpReceivedSend(msg);
        }

        /// <summary>
        /// 接続解除
        /// </summary>
        protected override void Close()
        {
            // TODO：不要な処理であれば後で削除
            // 現状呼び出し元から接続解除する予定はないが、念のため残しておく
        }

        /// <summary>
        /// ヘルスチェック内部電文処理
        /// </summary>
        protected abstract override void OnHelthCheck(MsgBase msg);

        /// <summary>
        /// TCP内部電文受信処理
        /// </summary>
        /// <param name="sender">内部電文メッセージクラス</param>
        /// <param name="e">メッセージイベント生成クラス</param>
        protected abstract override void OnTcpReceive(object? sender, MessageEventArgs e);
    }
}
