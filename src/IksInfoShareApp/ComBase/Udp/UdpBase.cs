using ComBase.Threads;
using MyApp.Tcp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComBase.Udp
{
    /// <summary>
    /// UDP基底クラス
    /// </summary>
    public abstract class UdpBase : ThreadManager
    {
        /// <summary>
        /// 接続開始
        /// </summary>
        /// <param name="connectInfo">UDP接続情報インスタンス</param>
        protected abstract void ConnectStart(UdpConnectInfo connectInfo);

        /// <summary>
        /// UDPコネクション確立
        /// </summary>
        protected abstract void Connection();
    }
}
