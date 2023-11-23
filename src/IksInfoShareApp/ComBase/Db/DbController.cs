using ComBase.Logs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ComBase.Common.StractDef;

namespace ComBase.Db
{
    /// <summary>
    /// DBコントローラークラス
    /// </summary>
    /// <typeparam name="TDbConnection"></typeparam>
    /// <typeparam name="TDbCommand"></typeparam>
    public class DbController<TDbConnection, TDbCommand> : IDbControl
        where TDbConnection : DbConnection
        where TDbCommand : DbCommand
    {
        /// <summary>
        /// DB接続文字列
        /// </summary>
        public string ConnectionString { get; set; } = string.Empty;

        /// <summary>
        /// SQLパラメーター設定変数
        /// </summary>
        public DbParameterCollection DbParameters
        {
            get => _dbCommand.Parameters;
        }

        /// <summary>
        /// ログファイル名
        /// </summary>
        private string _logFileName { get => typeof(IDbControl).Name ?? string.Empty; }

        /// <summary>
        /// DB接続変数
        /// </summary>
        private DbConnection _dbConnection { get; set; }

        /// <summary>
        /// DB実行コマンド
        /// </summary>
        private DbCommand _dbCommand;

        /// <summary>
        /// DBトランザクション
        /// </summary>
        private DbTransaction? _dbTransaction;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="command"></param>
        public DbController(DbConnection connection, DbCommand command)
        {
            _dbConnection = connection;
            _dbCommand = command;
            _dbTransaction = null;
        }

        /// <summary>
        /// DB接続
        /// </summary>
        public void Open()
        {
            _dbConnection?.Open();
            if (_dbConnection?.State == ConnectionState.Open)
            {
                Log.Trace(_logFileName, LOGLEVEL.INFO, "DB接続に成功しました。");
            }
        }

        /// <summary>
        /// トランザクション開始
        /// </summary>
        public void TransactionStart()
        {
            _dbTransaction = _dbConnection.BeginTransaction();
        }

        /// <summary>
        /// パラメーターインスタンス取得
        /// </summary>
        /// <returns></returns>
        public DbParameter GetDbParameter()
        {
            return _dbCommand.CreateParameter();
        }

        /// <summary>
        /// SQL実行結果読み取り
        /// </summary>
        public DbDataReader ExecuteReader(SqlBuilder sql)
        {
            // 実行するSQL文を設定
            _dbCommand.CommandText = sql.GetCommandText();
            // SQL読み取り処理実行後インスタンスを返却
            return _dbCommand.ExecuteReader();
        }

        /// <summary>
        /// SQLコマンド実行
        /// </summary>
        public void ExecuteNonQuery(SqlBuilder sql)
        {
            // 実行するSQL文を設定
            _dbCommand.CommandText = sql.GetCommandText();
            // SQL書き込み処理実行後インスタンスを返却
            _dbCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// トランザクションコミット
        /// </summary>
        public void TransactionCommit()
        {
            _dbTransaction?.Commit();
        }

        /// <summary>
        /// トランザクションロールバック
        /// </summary>
        public void TransactionRollback()
        {
            _dbTransaction?.Rollback();
        }

        /// <summary>
        /// メモリ開放
        /// </summary>
        public void Dispose()
        {
            _dbConnection.Close();
            _dbConnection.Dispose();
            _dbCommand.Dispose();
            _dbTransaction?.Dispose();
        }
    }
}
