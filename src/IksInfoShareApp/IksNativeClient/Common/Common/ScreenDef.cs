﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IksNativeClient.Common.Common
{
    /// <summary>
    /// 画面部定義クラス
    /// </summary>
    public static class ScreenDef
    {
        /// <summary>機能ID：未定義</summary>
        public const short FNC_ID_NONE = -1;
        /// <summary>機能ID：フレンド</summary>
        public const short FNC_ID_FRIEND = 0;
        /// <summary>機能ID：チャット</summary>
        public const short FNC_ID_CHAT = 1;
        /// <summary>機能ID：ユーザー</summary>
        public const short FNC_ID_USER = 2;

        /// <summary>画面ID：フレンド</summary>
        public const short WIN_ID_FRIEND_LIST = 0;
        /// <summary>機能ID：チャット</summary>
        public const short WIN_ID_CHAT = 1;
        /// <summary>機能ID：ユーザー</summary>
        public const short WIN_ID_USER = 2;

        /// <summary>非表示CSS</summary>
        public const string DISPLAY_NONE_CSS = "display_none";
    }
}
