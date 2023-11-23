using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static ComBase.Common.StractDef;
using ComBase.Msg;
using ComBase.Logs;
using ComBase.Common;

namespace ComBase.Tcp
{
    /// <summary>
    /// TCPコントローラークラス
    /// </summary>
    public class TcpController
    {
        /// <summary>
        /// TCPコントローラー情報
        /// </summary>
        private TcpControllerInfo _controllerInfo = new();

        /// <summary>
        /// TCPコントローラー取得用デリゲート
        /// </summary>
        /// <returns></returns>
        private FunctionDef.TcpControllerDelegate _Connect;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="service">SERVER：TCPサーバー、CLIENT：TCPクライアント</param>
        public TcpController(TCP service)
        {
            // TCPサーバー
            if (service == TCP.SERVER)
            {
                _Connect = TcpServerConnect;
            }
            // TCPクライアント
            else
            {
                _Connect = TcpClientConnect;
            }
        }

        /// <summary>
        /// TCPコネクション初期処理
        /// </summary>
        /// <param name="connectInfo">TCP接続情報格納インスタンス</param>
        public void Connect(TcpConnectInfo connectInfo)
        {
            try
            {
                // コネクション確立
                _Connect(connectInfo);
            }
            catch
            {
                // エラーハンドリングは呼び出し元で実装
                throw;
            }
        }

        /// <summary>
        /// TCP電文送信処理
        /// </summary>
        /// <param name="msg">TCP電文送信メッセージ</param>
        public void TcpSend(MsgBase msg)
        {
            try
            {
                // 送信電文情報をメモリストリームに書き込み
                msg.MsgWrite();
                // 書き込んだメモリ情報をbyte配列で取得
                byte[] sendBytes = msg.BytesRead();
                // TCPエンドポイントにメッセージとして送信
                _controllerInfo.NetStream?.Write(sendBytes, 0, sendBytes.Length);
            }
            catch
            {
                // エラーハンドリングは呼び出し元で実装
                throw;
            }
        }

        /// <summary>
        /// TCP受信電文取得処理
        /// </summary>
        public byte[] TcpRead()
        {
            try
            {
                if (_controllerInfo.NetStream != null && _controllerInfo.Buffer != null)
                {
                    // サーバからデータの送信があるまで処理を待機
                    int bytesRead = _controllerInfo.NetStream.Read(_controllerInfo.Buffer, 0, _controllerInfo.Buffer.Length);
                    if (bytesRead == 0)
                    {
                        throw new Exception("クライアントが切断しました。");
                    }
                }
                else
                {
                    throw new Exception("TCP電文取得処理異常 => ハンドルのnull値を検出");
                }
            }
            catch
            {
                // エラーハンドリングは呼び出し元で実装
                throw;
            }
            return _controllerInfo.Buffer;
        }

        /// <summary>
        /// コネクションの確立(サーバー)
        /// </summary>
        /// <param name="connectInfo">TCP接続情報格納インスタンス</param>
        private void TcpServerConnect(TcpConnectInfo connectInfo)
        {
            // ====================================================
            // 使用されているポートの判定処理
            // C#でリスニング状態のポートを取得する方法
            // https://usefuledge.com/csharp-check-port-open.html
            // 使用済みポート判定フラグ
            bool isUsePort = false;
            // ネットワーク関連のグローバルなプロパティと設定情報を取得
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            // アクティブなTCPリスナーポートの情報を取得
            IPEndPoint[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpListeners();
            // 指定したポート番号がTCP/IPリスナーポートとして使用されているかチェック
            // ※通信異常から復帰したタイミングでリスナーポートの競合が起きてしまうためチェックが必要
            foreach (IPEndPoint endpoint in tcpConnInfoArray)
            {
                // 使用しているポートがある場合ONを設定
                if (endpoint.Port == connectInfo.Port)
                {
                    isUsePort = true;
                }
            }
            // TCP/IPリスナーポートを使用していない場合にリッスンする
            if (!isUsePort)
            {
                _controllerInfo.Listener = new TcpListener(connectInfo.IpAddress, connectInfo.Port);
                _controllerInfo.Listener.Start();
                Log.Trace(string.Empty, LOGLEVEL.DEBUG, "Waiting for connection...");
            }

            try
            {
                // 初回起動時に別サーバーで同じリスナーポートを使用していると、TCPリスナーを初期化するタイミングと
                // 呼び出し元でサーバーを落とすタイミングがないことによって無限ループになってしまうため、こちらの方で明示的にサーバーを落とす
                // ※初回起動時にリスナーポートが被るとTCPリスナーがnull値で検出される
                if (_controllerInfo.Listener == null)
                {
                    throw new Exception("TCPリスナーがnull値で検出されました。");
                }
                else
                {
                    // クライアントからの接続要求待ち
                    _controllerInfo.Client = _controllerInfo.Listener.AcceptTcpClient();
                    // データを読み書きするインスタンスを取得
                    _controllerInfo.NetStream = _controllerInfo.Client.GetStream();
                    // 受信するデータのバッファサイズを指定して初期化
                    _controllerInfo.Buffer = new byte[_controllerInfo.Client.ReceiveBufferSize];
                }
            }
            catch
            {
                // 明示的にサーバーを落とす
                throw;
            }
        }

        /// <summary>
        /// コネクションの確立(クライアント)
        /// </summary>
        /// <param name="connectInfo">TCP接続情報格納インスタンス</param>
        private void TcpClientConnect(TcpConnectInfo connectInfo)
        {
            if (_controllerInfo.Client == null || !_controllerInfo.Client.Connected)
            {
                // サーバーへの接続要求
                _controllerInfo.Client = new TcpClient();
                _controllerInfo.Client.Connect(connectInfo.IpAddress, connectInfo.DestPort);
            }

            if (_controllerInfo.Client != null)
            {
                // データを読み書きするインスタンスを取得
                _controllerInfo.NetStream = _controllerInfo.Client.GetStream();
                // 受信するデータのバッファサイズを指定して初期化
                _controllerInfo.Buffer = new byte[_controllerInfo.Client.ReceiveBufferSize];
            }
            Log.Trace(string.Empty, LOGLEVEL.DEBUG, $"Server is listening on {connectInfo.IpAddress}:{connectInfo.DestPort}");
        }

        /// <summary>
        /// メモリ開放処理
        /// </summary>
        /// <remarks>異常終了などによって電文送受信用変数のメモリを開放する必要がある場合に実行</remarks>
        public void Close()
        {
            _controllerInfo.Close();
        }
    }
}
