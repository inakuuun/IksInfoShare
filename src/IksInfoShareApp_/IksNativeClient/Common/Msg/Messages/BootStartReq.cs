using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComBase.Msg;
using IksNativeClient.Common.Msg.Deffine;

namespace IksNativeClient.Common.Msg.Messages
{
    /// <summary>
    /// 初期起動通知要求メッセージクラス
    /// </summary>
    public class BootStartReq : MsgBase
    {
        /// <summary>
        /// 電文ID
        /// </summary>
        public override short MessageId { get => _messageId; }
        private short _messageId = MsgDef.MSG_BOOTSTART_REQ;

        /// <summary>
        /// ユーザーID
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// ユーザー名
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// ユーザーIPアドレス
        /// </summary>
        public string UserIp { get; set; } = string.Empty;

        /// <summary>
        /// メッセージ読み取りインスタンス
        /// </summary>
        private MsgReader _msgReader
        {
            get => MsgReader;
            set => MsgReader = value;
        }

        /// <summary>
        /// メッセージ生成インスタンス
        /// </summary>
        private MsgWriter _msgWriter
        {
            get => MsgWriter;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BootStartReq() : base() { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BootStartReq(byte[] bytesmessage) : base(bytesmessage)
        {
            if (_msgReader != null)
            {
                UserId = _msgReader.RdStr();
                UserName = _msgReader.RdStr();
                UserIp = _msgReader.RdStr();
            }
        }

        /// <summary>
        /// 送信メッセージをメモリストリームに書き込み
        /// </summary>
        public override void MsgWrite()
        {
            if (_msgWriter != null)
            {
                _msgWriter.WtShort(_messageId);
                _msgWriter.WtStr(UserId);
                _msgWriter.WtStr(UserName);
                _msgWriter.WtStr(UserIp);
            }
        }

        /// <summary>
        /// 電文長取得
        /// </summary>
        /// <returns>プロパティのサイズを全て加算した電文長</returns>
        sealed protected override int GetLength()
        {
            int size = 0;
            size = GetSize(_messageId, size);
            size = GetSize(UserId, size);
            size = GetSize(UserName, size);
            size = GetSize(UserIp, size);
            return size;
        }
    }
}
