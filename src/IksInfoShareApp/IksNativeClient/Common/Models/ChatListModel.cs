using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IksNativeClient.Common.Models
{
    /// <summary>
    /// チャット一覧モデル
    /// </summary>
    public class ChatListModel
    {
        /// <summary>
        /// ルームID
        /// </summary>
        public long RoomId { get; set; }

        /// <summary>
        /// ルーム画像
        /// </summary>
        public byte RoomImage { get; set; }

        /// <summary>
        /// ルーム名
        /// </summary>
        public string RoomName { get; set; }

        /// <summary>
        /// 最新受信日時
        /// </summary>
        /// <remarks>
        /// 現在受信済みのルームに基づく最新のチャットメッセージの送信日時
        /// </remarks>
        public string LatestReceiveDate { get; set; }

        /// <summary>
        /// 最新チャットメッセージ
        /// </summary>
        public string LatestMessage { get; set; }
    }
}
