using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IksNativeClient.Common.Common
{
    /// <summary>
    /// プロパティ定義クラス
    /// </summary>
    public static class PropertyDef
    {
        /// <summary>SQLite_接続文字列</summary>
        public const string Property_SQLiteConnectionString = "SQLite.ConnectionString";
        /// <summary>SQLite_接続先DBファイルパス</summary>
        public const string Property_SQLiteDataSource = "SQLite.DataSource";
        /// <summary>SQLite_バージョン情報</summary>
        public const string Property_SQLiteVersion = "SQLite.Version";

        /// <summary>PostgreSQL_接続文字列</summary>
        public const string Property_PostgreSQLConnectionString = "PostgreSQL.ConnectionString";
        /// <summary>PostgreSQL_接続先サーバー(IPアドレス)</summary>
        public const string Property_PostgreSQLServer = "PostgreSQL.Server";
        /// <summary>PostgreSQL_接続先ポート(ポート番号)</summary>
        public const string Property_PostgreSQLPort = "PostgreSQL.Port";
        /// <summary>PostgreSQL_ユーザー名</summary>
        public const string Property_PostgreSQLUsername = "PostgreSQL.Username";
        /// <summary>PostgreSQL_パスワード</summary>
        public const string Property_PostgreSQLPassword = "PostgreSQL.Password";
        /// <summary>PostgreSQL_データベース名</summary>
        public const string Property_PostgreSQLDatabase = "PostgreSQL.Database";
    }
}
