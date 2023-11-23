using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComBase.Common
{
    /// <summary>
    /// 構造体クラス
    /// </summary>
    public struct StractDef
    {
        /// <summary>
        /// データベース識別子
        /// </summary>
        public enum DB
        {
            SQLite,
            PostgreSQL,
        }

        /// <summary>
        /// データベース識別子
        /// </summary>
        public enum LOGLEVEL
        {
            DEBUG,
            INFO,
            WARNING,
            ERROR,
        }

        /// <summary>
        /// TCPサービス識別子
        /// </summary>
        public enum TCP
        {
            SERVER,
            CLIENT,
        }

        /// <summary>
        /// UDPサービス識別子
        /// </summary>
        public enum UDP
        {
            SERVER,
            CLIENT,
        }
    }
}
