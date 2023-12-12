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
        /// 共通Daoアクセスクラス
        /// </summary>
        public CommonDaoAccess CommonDaoAccess { get; set; }

        /// <summary>
        /// ユーザーテーブルDaoアクセスクラス
        /// </summary>
        public UsersDaoAccess UsersDaoAccess { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DbLogic()
        {
            CommonDaoAccess = new CommonDaoAccess(this);
            UsersDaoAccess = new UsersDaoAccess(this);
        }

        /// <summary>
        /// 初回テーブル作成処理
        /// </summary>
        public void InitCreateTable()
        {
            UsersDaoAccess.CreateTable();
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
