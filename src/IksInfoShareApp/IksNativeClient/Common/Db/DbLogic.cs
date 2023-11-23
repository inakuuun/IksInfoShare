using ComBase.Db;
using IksNativeClient.Common.Db.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IksNativeClient.Common.Db
{
    /// <summary>
    /// DBロジッククラス
    /// </summary>
    /// <remarks>Daoアクセスクラスの管理</remarks>
    public class DbLogic : DbLogicBase
    {
        /// <summary>
        /// チャットDaoアクセスクラス
        /// </summary>
        public ChatDaoAccess ChatDaoAccess { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DbLogic()
        {
            ChatDaoAccess = new ChatDaoAccess(this);
        }

        /// <summary>
        /// SQL実行処理
        /// </summary>
        /// <param name="action"></param>
        public new void SQLCommand(Action<IDbControl> action)
        {
            base.SQLCommand(action);
        }
    }
}
