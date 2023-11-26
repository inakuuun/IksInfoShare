using IksNativeClient.Common.Common;
using IksNativeClient.Common.Db;
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

        public void Chat()
        {
            Console.WriteLine("Chatを呼び出しました");
        }
    }
}
