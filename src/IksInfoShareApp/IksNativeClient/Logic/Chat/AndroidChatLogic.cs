using IksNativeClient.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IksNativeClient.Logic.Chat
{
    public class AndroidChatLogic : IChatLogic
    {
        public void Chat()
        {
            Console.WriteLine("Chatを呼び出しました");
        }
    }
}
