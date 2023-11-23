using ComBase.Msg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComBase.Events
{
    /// <summary>
    /// メッセージイベント
    /// </summary>
    public class MessageEventArgs : EventArgs
    {
        /// <summary>
        /// 電文ID
        /// </summary>
        public short MessageId { get; set; }

        /// <summary>
        /// 電文メッセージ
        /// </summary>
        public byte[] Message { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="msg"></param>
        public MessageEventArgs(MsgBase msg)
        {
            MessageId = msg.MessageId;
            Message = msg.Message;
        }
    }
}
