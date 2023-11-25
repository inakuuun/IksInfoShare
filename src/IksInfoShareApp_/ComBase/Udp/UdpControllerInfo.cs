using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ComBase.Common;

namespace ComBase.Udp
{
    /// <summary>
    /// UDPコントローラー情報クラス
    /// </summary>
    /// <remarks>コントローラーで使用する情報を集約</remarks>
    public class UdpControllerInfo
    {
        /// <summary>
        /// UDPサービス識別子
        /// </summary>
        public StractDef.UDP Service;

        /// <summary>
        /// UDPクライアント
        /// </summary>
        public UdpClient UdpClient { get; set; } = new UdpClient();

        /// <summary>
        /// IPエンドポイント
        /// </summary>
        /// <remarks>接続先情報を保持</remarks>
        public IPEndPoint? IpEndPoint;
    }
}
