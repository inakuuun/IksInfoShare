using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComBase.Udp
{
    /// <summary>
    /// UDP接続情報クラス
    /// </summary>
    public class UdpConnectInfo
    {
        /// <summary>
        /// IPアドレス
        /// </summary>
        public string IpAddress { get; set; } = string.Empty;

        /// <summary>
        /// 接続先ポート番号
        /// </summary>
        public short DestPort { get; set; }

        /// <summary>
        /// ポート番号
        /// </summary>
        public short Port { get; set; }
    }
}
