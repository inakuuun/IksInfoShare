using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ComBase.Tcp
{
    /// <summary>
    /// TCPコントローラー情報クラス
    /// </summary>
    /// <remarks>コントローラーで使用する情報を集約</remarks>
    public class TcpControllerInfo
    {
        /// <summary>
        /// TCPリスナー
        /// </summary>
        public TcpListener? Listener;

        /// <summary>
        /// TCPクライアント
        /// </summary>
        /// <remarks>電文の送信に利用</remarks>
        public TcpClient? Client;

        /// <summary>
        /// 電文送受信用変数
        /// </summary>
        public NetworkStream? NetStream;

        /// <summary>
        /// 受信データ読み取り用バッファ
        /// </summary>
        public byte[]? Buffer;

        /// <summary>
        /// メモリ開放処理
        /// </summary>
        public void Close()
        {
            NetStream?.Close();
            NetStream?.Dispose();
        }
    }
}
