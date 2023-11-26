using ComBase.Db;
using ComBase.Logs;
using IksNativeClient.Common.Db.Bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ComBase.Common.StractDef;

namespace IksNativeClient.Common.Db.Dao
{
    /// <summary>
    /// ユーザーテーブルDaoアクセスクラス
    /// </summary>
    public class UsersDaoAccess
    {
        /// <summary>
        /// ログファイル名
        /// </summary>
        private string _logFileName { get => typeof(UsersDaoAccess).Name ?? string.Empty; }

        /// <summary>
        /// DBロジック
        /// </summary>
        DbLogic _pObject;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="obj"></param>
        public UsersDaoAccess(DbLogic obj)
        {
            _pObject = obj;
        }

        /// <summary>
        /// テーブル作成処理
        /// </summary>
        public void CreateTable()
        {
            _pObject.SQLCommand((control) =>
            {
                try
                {
                    // トランザクション開始
                    control.TransactionStart();

                    var br = new SqlBuilder(control);
                    br.Add("CREATE TABLE IF NOT EXISTS users( ");
                    br.Add("    user_id integer not null primary key");
                    br.Add("    , user_name varchar (20)");
                    br.Add("    , password varchar (20)");
                    br.Add(");");

                    // SQL実行
                    var result = control.ExecuteReader(br);
                    //// トランザクションコミット
                    control.TransactionCommit();
                }
                catch (Exception ex)
                {
                    Log.Trace(_logFileName, LOGLEVEL.ERROR, $"SQL実行時異常 => {ex}");
                    control.TransactionRollback();
                }
            });
        }

        /// <summary>
        /// ユーザーテーブル INSERT
        /// </summary>
        public void UsersInsert(UsersBean usersBean)
        {
            // 
            _pObject.SQLCommand((control) =>
            {
                try
                {
                    // トランザクション開始
                    control.TransactionStart();
                    var br = new SqlBuilder(control);

                    br.Add("INSERT INTO users (");
                    br.Add("  user_id");
                    br.Add("  ,user_name");
                    br.Add("  ,password");
                    br.Add(") VALUES (");
                    br.Add("  :user_id", null);
                    br.Add("  ,:user_name", usersBean.UserName);
                    br.Add("  ,:password", usersBean.Password);
                    br.Add(");");

                    // SQL実行
                    var result = control.ExecuteReader(br);
                    //// トランザクションコミット
                    control.TransactionCommit();
                }
                catch (Exception ex)
                {
                    Log.Trace(_logFileName, LOGLEVEL.ERROR, $"SQL実行時異常 => {ex}");
                    control.TransactionRollback();
                }
            });
        }
    }
}
