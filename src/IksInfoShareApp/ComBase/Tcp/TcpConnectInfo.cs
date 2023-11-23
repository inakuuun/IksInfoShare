using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ComBase.Tcp
{
    /// <summary>
    /// TCP接続情報クラス
    /// </summary>
    public class TcpConnectInfo
    {
        /// <summary>
        /// IPアドレス
        /// </summary>
        public IPAddress IpAddress = IPAddress.Any;

        /// <summary>
        /// 接続先ポート番号
        /// </summary>
        public int DestPort;

        /// <summary>
        /// ポート番号
        /// </summary>
        public int Port;

        /// <summary>
        /// ヘルスチェック有無
        /// </summary>
        public bool IsHelthCheck;

        /// <summary>
        /// ヘルスチェック間隔(ミリ秒)
        /// </summary>
        public int HelthCheckInterval;
    }
}
