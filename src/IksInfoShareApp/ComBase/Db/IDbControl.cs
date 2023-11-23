using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComBase.Db
{
    /// <summary>
    /// DBコントローラーインタフェース
    /// </summary>
    public interface IDbControl : IDisposable
    {
        /// <summary>
        /// SQLパラメーター設定変数
        /// </summary>
        DbParameterCollection DbParameters { get; }

        /// <summary>
        /// DB接続
        /// </summary>
        void Open();

        /// <summary>
        /// トランザクション開始
        /// </summary>
        void TransactionStart();

        /// <summary>
        /// パラメーターインスタンス取得
        /// </summary>
        DbParameter GetDbParameter();

        /// <summary>
        /// SQL実行結果読み取り
        /// </summary>
        DbDataReader ExecuteReader(SqlBuilder sql);

        /// <summary>
        /// SQLコマンド実行
        /// </summary>
        void ExecuteNonQuery(SqlBuilder str);

        /// <summary>
        /// トランザクションコミット
        /// </summary>
        void TransactionCommit();

        /// <summary>
        /// トランザクションロールバック
        /// </summary>
        void TransactionRollback();
    }
}
