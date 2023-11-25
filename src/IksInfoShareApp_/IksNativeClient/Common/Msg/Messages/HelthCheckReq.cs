using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComBase.Msg;
using IksNativeClient.Common.Msg.Deffine;

namespace IksNativeClient.Common.Msg.Messages
{
    /// <summary>
    /// ヘルスチェック要求メッセージクラス
    /// </summary>
    public class HelthCheckReq : MsgBase
    {
        /// <summary>
        /// 電文ID
        /// </summary>
        public override short MessageId { get => _messageId; }
        private short _messageId = MsgDef.MSG_HELTHCHECK_REQ;

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
        public HelthCheckReq() : base() { }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public HelthCheckReq(byte[] bytesmessage) : base(bytesmessage) { }

        /// <summary>
        /// 送信メッセージをメモリストリームに書き込み
        /// </summary>
        public override void MsgWrite()
        {
            if (_msgWriter != null)
            {
                _msgWriter.WtShort(_messageId);
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
            return size;
        }
    }
}
