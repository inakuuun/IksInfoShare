using IksNativeClient.Common.Common;
using IksNativeClient.Common.Db;
using IksNativeClient.Common.Db.Bean;
using IksNativeClient.Common.Models;
using IksNativeClient.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NGLB = IksNativeClient.Common.Common.CommonDef;

namespace IksNativeClient.Logic.Chat
{
    /// <summary>
    /// Windows：チャットロジッククラス
    /// </summary>
    public class WindowsChatLogic : IChatLogic
    {
        /// <summary>
        /// DBロジック
        /// </summary>
        private DbLogic _dbLogic = NGLB.DbLogic;

        /// <summary>
        /// チャット一覧取得
        /// </summary>
        public List<ChatListModel> GetChatList()
        {
            List<ChatListModel> chatListModelList = new();
            var chatLlistBeanExList = _dbLogic.CommonDaoAccess.GetChatList();
            ChatListModel chatListModel = new();
            foreach (ChatListBeanEx chatLlistBean in chatLlistBeanExList)
            {
                chatListModel = new ChatListModel()
                {
                    RoomId = chatLlistBean.RoomId,
                    RoomImage = chatLlistBean.RoomImage,
                    RoomName = chatLlistBean.RoomName,
                    LatestReceiveDate = chatLlistBean.LatestReceiveDate,
                    LatestMessage = chatLlistBean.LatestMessage,
                };
                chatListModelList.Add(chatListModel);
            }
            return chatListModelList;
        }
    }
}
