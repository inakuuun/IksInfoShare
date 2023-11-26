using IksNativeClient.Common.Common;
using IksNativeClient.Common.Db;
using IksNativeClient.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IksNativeClient.Logic.Chat
{
    public class WindowsChatLogic : IChatLogic
    {
        /// <summary>
        /// DBロジック
        /// </summary>
        private DbLogic _dbLogic = CommonDef.DbLogic;

        public void Chat()
        {
            Console.WriteLine("Chatを呼び出しました");
        }
    }
}
