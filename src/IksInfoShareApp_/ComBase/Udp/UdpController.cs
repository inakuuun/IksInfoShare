using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static ComBase.Common.StractDef;
using ComBase.Db;
using ComBase.Msg;

namespace ComBase.Udp
{
    /// <summary>
    /// UDPコントローラークラス
    /// </summary>
    public class UdpController
    {
        /// <summary>
        /// UDPコントローラー情報
        /// </summary>
        private UdpControllerInfo _controllerInfo = new();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="service">SERVER：TCPサーバー、CLIENT：TCPクライアント</param>
        /// <param name="connectInfo">UDP接続情報</param>
        public UdpController(UDP service, UdpConnectInfo connectInfo)
        {
            // UDPサーバー
            if (service == UDP.SERVER)
            {
                _controllerInfo.IpEndPoint = new IPEndPoint(IPAddress.Any, connectInfo.Port);
            }
            // UDPクライアント
            else
            {
                _controllerInfo.IpEndPoint = new IPEndPoint(IPAddress.Parse(connectInfo.IpAddress), connectInfo.DestPort);
            }
            // UDPサービス識別子をセット
            _controllerInfo.Service = service;
            // リッスンポートを指定してUDPクライアントを初期化
            // ※ポートを指定して初期化することでReceive時に待機できるようになる
            _controllerInfo.UdpClient = new(connectInfo.Port);
        }

        /// <summary>
        /// UDP電文送信処理
        /// </summary>
        /// <param name="msg">UDP電文送信メッセージ</param>
        public void UdpSend(MsgBase msg)
        {
            // 送信電文情報をメモリストリームに書き込み
            msg.MsgWrite();
            // 書き込んだメモリ情報をbyte配列で取得
            byte[] sendBytes = msg.BytesRead();
            // UDPエンドポイントにメッセージとして送信
            _controllerInfo.UdpClient.Send(sendBytes, sendBytes.Length, _controllerInfo.IpEndPoint);
        }

        /// <summary>
        /// UDP受信電文取得処理
        /// </summary>
        /// <returns></returns>
        public byte[] Receive()
        {
            // TODO：エンドポイントをディクショナリに詰めるか検討
            byte[] receiveData = _controllerInfo.UdpClient.Receive(ref _controllerInfo.IpEndPoint);
            return receiveData;
        }
    }
}
