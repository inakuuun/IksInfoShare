using ComBase.Common;
using ComBase.FileUtil;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComBase.Db
{
    /// <summary>
    /// DBコントローラー情報生成クラス
    /// </summary>
    public class DbControllerInfo
    {
        /// <summary>
        /// SQLite_接続文字列
        /// </summary>
        private string _SQLiteConnectionString = string.Empty;
        /// <summary>
        /// SQLite_接続先DBファイルパス
        /// </summary>
        private string _SQLiteDataSource = string.Empty;
        /// <summary>
        /// SQLite_バージョン情報
        /// </summary>
        private string _SQLiteVersion = string.Empty;

        /// <summary>
        /// PostgreSQL_接続文字列
        /// </summary>
        private string _PostgreSQLConnectionString = string.Empty;
        /// <summary>
        /// PostgreSQL_接続先サーバー(IPアドレス)
        /// </summary>
        private string _PostgreSQLServer = string.Empty;
        /// <summary>
        /// PostgreSQL_接続先ポート(ポート番号)
        /// </summary>
        private string _PostgreSQLPort = string.Empty;
        /// <summary>
        /// PostgreSQL_ユーザー名
        /// </summary>
        private string _PostgreSQLUsername = string.Empty;
        /// <summary>
        /// PostgreSQL_パスワード
        /// </summary>
        private string _PostgreSQLPassword = string.Empty;
        /// <summary>
        /// PostgreSQL_データベース名
        /// </summary>
        private string _PostgreSQLDatabase = string.Empty;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DbControllerInfo()
        {
            // SQLite
            _SQLiteConnectionString = PropertyReader.GetProperty(PropertyDef.Property_SQLiteConnectionString);
            _SQLiteDataSource = PropertyReader.GetProperty(PropertyDef.Property_SQLiteDataSource);
            _SQLiteVersion = PropertyReader.GetProperty(PropertyDef.Property_SQLiteVersion);

            // PostgreSQL
            _PostgreSQLConnectionString = PropertyReader.GetProperty(PropertyDef.Property_PostgreSQLConnectionString);
            _PostgreSQLServer = PropertyReader.GetProperty(PropertyDef.Property_PostgreSQLServer);
            _PostgreSQLPort = PropertyReader.GetProperty(PropertyDef.Property_PostgreSQLPort);
            _PostgreSQLUsername = PropertyReader.GetProperty(PropertyDef.Property_PostgreSQLUsername);
            _PostgreSQLPassword = PropertyReader.GetProperty(PropertyDef.Property_PostgreSQLPassword);
            _PostgreSQLDatabase = PropertyReader.GetProperty(PropertyDef.Property_PostgreSQLDatabase);
        }

        /// <summary>
        /// SQLite接続文字列取得処理
        /// </summary>
        /// <returns>DB接続文字列</returns>
        public string GetConnectionStringSQLite()
        {
            return string.Format(_SQLiteConnectionString, _SQLiteDataSource, _SQLiteVersion);
        }

        /// <summary>
        /// PostgreSQL接続文字列取得処理
        /// </summary>
        /// <returns>DB接続文字列</returns>
        public string GetConnectionStringPostgreSQL()
        {
            return string.Format(_PostgreSQLConnectionString, _PostgreSQLServer, _PostgreSQLPort, _PostgreSQLUsername, _PostgreSQLPassword, _PostgreSQLDatabase);
        }
    }
}
