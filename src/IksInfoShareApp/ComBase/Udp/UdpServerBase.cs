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
    /// UDPサーバー基底クラス
    /// </summary>
    public abstract class UdpServerBase : UdpBase
    {
        /// <summary>
        /// ログファイル名
        /// </summary>
        private string _logFileName { get => typeof(TcpClientBase).Name ?? string.Empty; }

        /// <summary>
        /// UDPサーバーコントローラー
        /// </summary>
        private UdpController? _ucpServer;

        /// <summary>
        /// UDP接続情報
        /// </summary>
        private UdpConnectInfo _connectInfo = new();

        /// <summary>
        /// 接続開始
        /// </summary>
        /// <param name="connectInfo">UDP接続情報インスタンス</param>
        protected override void ConnectStart(UdpConnectInfo connectInfo)
        {
            _connectInfo = connectInfo;
            Task.Run(() => Connection());
        }

        /// <summary>
        /// UDPコネクション確立
        /// </summary>
        sealed protected override void Connection()
        {
            // -------------------------------------------------
            // クライアントとUDP接続確立
            // クライアントから接続があるまで待機
            // -------------------------------------------------
            _ucpServer = new UdpController(UDP.SERVER, _connectInfo);
            try
            {
                while (true)
                {
                    // クライアントからの受信を待機
                    byte[] message = _ucpServer.Receive();
                    // UDP内部電文送信処理
                    if (message is not null)
                    {
                        UdpReceivedSend(new MsgBase(message));
                    }
                    byte[] response = Encoding.UTF8.GetBytes("サーバーからの応答");
                    UdpSend(new MsgBase(response));
                }
            }
            catch (Exception ex)
            {
                Log.Trace(_logFileName, LOGLEVEL.WARNING, $"UDPコネクション確立時異常（サーバー） => {ex}");
            }
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
        /// UDP電文送信処理
        /// </summary>
        /// <param name="msg">UDP電文送信メッセージ</param>
        protected void UdpSend(MsgBase msg)
        {
            _ucpServer?.UdpSend(msg);
        }

        /// <summary>
        /// UDP内部電文受信処理
        /// </summary>
        /// <param name="sender">内部電文メッセージクラス</param>
        /// <param name="e">メッセージイベント生成クラス</param>
        protected abstract override void OnUdpReceive(object? sender, MessageEventArgs e);
    }
}
