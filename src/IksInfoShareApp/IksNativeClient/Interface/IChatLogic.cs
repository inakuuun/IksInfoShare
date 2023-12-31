﻿using IksNativeClient.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IksNativeClient.Interface
{
    /// <summary>
    /// チャットロジックインタフェース
    /// </summary>
    public interface IChatLogic
    {
        List<ChatListModel> GetChatList();
    }
}
