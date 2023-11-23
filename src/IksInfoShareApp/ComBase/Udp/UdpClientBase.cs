using ComBase.Events;
using ComBase.Logs;
using ComBase.Msg;
using ComBase.Tcp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static ComBase.Common.StractDef;

namespace ComBase.Udp
{
    /// <summary>
    /// UDPクライアント基底クラス
    /// </summary>
    public abstract class UdpClientBase : UdpBase
    {
        /// <summary>
        /// ログファイル名
        /// </summary>
        private string _logFileName { get => typeof(TcpClientBase).Name ?? string.Empty; }

        /// <summary>
        /// UDPクライアントコントローラー
        /// </summary>
        private UdpController? _udpClient;

        /// <summary>
        /// UDP接続情報
        /// </summary>
        private UdpConnectInfo _udpConnectInfo = new();

        /// <summary>
        /// 接続開始
        /// </summary>
        /// <param name="connectInfo">UDP接続情報インスタンス</param>
        protected override void ConnectStart(UdpConnectInfo connectInfo)
        {
            _udpConnectInfo = connectInfo;
            Task.Run(() => Connection());
        }

        /// <summary>
        /// UDPコネクション確立
        /// </summary>
        sealed protected override void Connection()
        {
            // -------------------------------------------------
            // サーバーとUDP接続
            // サーバーから接続があるまで待機
            // -------------------------------------------------
            _udpClient = new UdpController(UDP.CLIENT, _udpConnectInfo);
            try
            {
                while (true)
                {
                    // サーバーからの受信を待機
                    byte[] message = _udpClient.Receive();
                    // UDP内部電文送信処理
                    if (message is not null)
                    {
                        UdpReceivedSend(new MsgBase(message));
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Trace(_logFileName, LOGLEVEL.WARNING, $"UDPコネクション確立時異常（クライアント）=> {ex}");
            }
        }

        /// <summary>
        /// UDP送信処理
        /// </summary>
        protected void UdpSend(MsgBase msg)
        {
            // サーバーへUDP送信
            _udpClient?.UdpSend(msg);
        }

        /// <summary>
        /// UDP内部電文送信処理
        /// </summary>
        /// <param name="msg">UDP内部電文送信メッセージ</param>
        private new void UdpReceivedSend(MsgBase msg)
        {
            // 基底クラスの内部電文イベントを実行させる
            base.UdpReceivedSend(msg);
        }

        /// <summary>
        /// UDP内部電文受信処理
        /// </summary>
        /// <param name="sender">内部電文メッセージクラス</param>
        /// <param name="e">メッセージイベント生成クラス</param>
        protected abstract override void OnUdpReceive(object? sender, MessageEventArgs e);
    }
}
