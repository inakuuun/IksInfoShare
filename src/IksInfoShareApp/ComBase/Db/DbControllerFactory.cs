using ComBase.Common;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComBase.Db
{
    /// <summary>
    /// DBインスタンス生成クラス
    /// </summary>
    public class DbControllerFactory
    {
        /// <summary>
        /// DB接続用文字列
        /// </summary>
        private string _connectString = string.Empty;

        /// <summary>
        /// DBコントローラー取得用デリゲート
        /// </summary>
        private static FunctionDef.DbControlDelegate _GetDbControl;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="db">データベース識別子</param>
        public DbControllerFactory(StractDef.DB db)
        {
            // db格納用ディレクトリ
            string dbDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "db");

            // dbディレクトリが存在しない場合は作成
            // 実行環境に生成される
            if (!Directory.Exists(dbDirectory))
            {
                Directory.CreateDirectory(dbDirectory);
            }

            // DBコントローラー情報生成クラスをインスタンス化
            DbControllerInfo dbControlInfo = new();
            // SQLite
            if (db == StractDef.DB.SQLite)
            {
                // SQLiteのDB接続用文字列を取得
                _connectString = dbControlInfo.GetConnectionStringSQLite();
                // SQLiteのコントローラー取得デリゲートを登録
                _GetDbControl = GetDbControlSQLite;
            }
            // PostgreSQL
            else
            {
                // PostgreSQLのDB接続用文字列を取得
                _connectString = dbControlInfo.GetConnectionStringPostgreSQL();
                // PostgreSQLのコントローラー取得デリゲートを登録
                _GetDbControl = GetDbControlPostgreSQL;
            }
        }

        /// <summary>
        /// DBコントローラー取得(SQLite)
        /// </summary>
        /// <return>SQLite接続後インスタンス</return>
        private IDbControl GetDbControlSQLite()
        {
            // コネクションクラスをインスタンス化
            DbConnection connection = new SQLiteConnection()
            {
                ConnectionString = _connectString
            };
            // コマンドクラスをインスタンス化
            DbCommand command = connection.CreateCommand();
            // コントローラークラスを初期化
            var instance = new DbController<SQLiteConnection, SQLiteCommand>(connection, command);
            // インスタンスのコネクションをオープンにしておく
            instance.Open();
            // インスタンスを返却
            return instance;
        }

        /// <summary>
        /// DBコントローラー取得(PostgreSQL)
        /// </summary>
        /// <return>PostgreSQL接続後インスタンス</return>
        private IDbControl GetDbControlPostgreSQL()
        {
            // コネクションクラスをインスタンス化
            DbConnection connection = new NpgsqlConnection()
            {
                ConnectionString = _connectString
            };
            // コマンドクラスをインスタンス化
            DbCommand command = connection.CreateCommand();
            // コントローラークラスを初期化
            var instance = new DbController<NpgsqlConnection, NpgsqlCommand>(connection, command);
            // インスタンスのコネクションをオープンにしておく
            instance.Open();
            // インスタンスを返却
            return instance;
        }

        /// <summary>
        /// DBコントローラー取得
        /// </summary>
        /// <remarks>デリゲートで取得したDBコントローラーインスタンスを返却</remarks>
        public static IDbControl GetControl()
        {
            // DBコントローラーを返却
            return _GetDbControl();
        }
    }
}
