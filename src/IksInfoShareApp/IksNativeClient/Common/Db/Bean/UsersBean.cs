using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IksNativeClient.Common.Db.Bean
{
    /// <summary>
    /// ユーザーテーブルBean
    /// </summary>
    public class UsersBean
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        public string UserId { get; set; }
        
        /// <summary>
        /// ユーザー名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// パスワード
        /// </summary>
        public string Password { get; set; }
    }
}
