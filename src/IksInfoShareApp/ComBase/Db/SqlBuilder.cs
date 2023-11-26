using ComBase.Logs;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ComBase.Common.StractDef;

namespace ComBase.Db
{
    /// <summary>
    /// SQL実行用文字列生成クラス
    /// </summary>
    public class SqlBuilder
    {
        /// <summary>
        /// 文字列管理用変数
        /// </summary>
        private StringBuilder _builder = new();

        /// <summary>
        /// コントローラー保持用変数
        /// </summary>
        private IDbControl _control;

        /// <summary>
        /// パラメーターキー抽出用
        /// </summary>
        private readonly char[] PARAMKEY_MATCH_CHAR = { ' ', ';', ')', ',' };

        /// <summary>
        /// パラメーターキー開始文字
        /// </summary>
        private readonly char PARAMKEY_START_CHAR = ':';

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="control">DBコントロール</param>
        public SqlBuilder(IDbControl control)
        {
            _control = control;
            _control.DbParameters.Clear();
        }

        /// <summary>
        /// コマンド実行用SQL文を取得
        /// </summary>
        /// <remarks>コントローラークラスからSQL実行時に呼び出される関数</remarks>
        /// <returns>Add関数により格納されたSQL文</returns>
        public string GetCommandText()
        {
            return _builder.ToString();
        }

        /// <summary>
        /// SQL文追加処理
        /// </summary>
        /// <param name="sql">SQL文字列</param>
        public void Add(string sql)
        {
            // 文字列が空の場合は処理を終了
            if (string.IsNullOrEmpty(sql))
            {
                return;
            }
            // 構文補正処理
            sql = SyntaxReplace(sql);
            // SQL実行用文字列として格納
            _builder.AppendLine(sql);
        }

        /// <summary>
        /// SQL文追加処理(パラメータを含む)
        /// </summary>
        /// <param name="sql">SQL文字列</param>
        /// <param name="objects">置換用パラメーター</param>
        public void Add(string sql, params object[] objects)
        {
            try
            {
                // 文字列が空の場合は処理を終了
                if (string.IsNullOrEmpty(sql))
                {
                    return;
                }
                // 構文補正処理
                sql = SyntaxReplace(sql);
                // SQL実行用文字列として格納
                _builder.AppendLine(sql);
                // パラメーター設定処理
                // パラメーターキー配列取得
                string[] paramKeys = ExtractParam(sql);
                // パラメーターキー配列分ループ処理を実施
                for (int i = 0; i < paramKeys.Length; i++)
                {
                    // コントローラーのパラメーターインスタンス取得
                    // ※パラメーターを設定する度にインスタンス化する必要あり
                    var param = _control.GetDbParameter();
                    // パラメーターが未設定の場合
                    if (!_control.DbParameters.Contains(paramKeys[i]))
                    {
                        // パラメータがnullの場合
                        if(objects == null)
                        {
                            // パラメーターのValueにDBNullをセット
                            param.ParameterName = paramKeys[i];
                            param.Value = DBNull.Value;
                        }
                        else
                        {
                            // パラメーターをセット
                            param.ParameterName = paramKeys[i];
                            param.Value = objects[i];
                        }
                        _control.DbParameters.Add(param);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Trace(string.Empty, LOGLEVEL.WARNING, $"パラメータを含むSQL文追加時異常 => {ex}");
            }
        }

        /// <summary>
        /// 構文補正処理
        /// </summary>
        /// <remarks>SQL文に含みたくない文字列を無効化</remarks>
        /// <returns>置換後文字列</returns>
        private string SyntaxReplace(string sql)
        {
            // 前後空白スペース削除
            string result = sql.Trim();
            // 文字をエスケープ
            // ※「@」をつけることで、取得した値を全てただの文字列として扱うことができる
            result = $@"{result}";
            return result;
        }

        /// <summary>
        /// パラメーターキー抽出処理
        /// </summary>
        /// <param name="sql">抽出対象SQL</param>
        /// <param name="paramKeys">パラメーターキー配列</param>
        /// <param name="arrayIndex">配列格納用インデックス値</param>
        /// <remarks>パラメーターキーを対象文字列から抽出し、配列に格納</remarks>
        /// <returns>パラメーターキー格納後配列</returns>
        private string[] ExtractParam(string sql, string[]? paramKeys = null, int arrayIndex = 0)
        {
            // 抽出対象SQLを退避
            string str = sql;
            // ※初回呼び出しの場合
            if (paramKeys == null)
            {
                // パラメーターキー配列を「:」の出現回数で初期化
                // ※「:」の出現回数＝パラメーター数
                paramKeys = new string[str.Count(ch => ch == PARAMKEY_START_CHAR)];
            }
            // 文字列抽出開始位置
            // ※2つ以上対象文字がある場合、再帰呼び出し時にひっかからないように「対象文字＋1」を開始位置とする
            int startIndex = str.IndexOf(PARAMKEY_START_CHAR) + 1;
            // 文字列抽出終了位置
            int endIndex = -1;
            // 指定の文字から末尾までを抽出
            str = str[startIndex..];
            // 定義配列の中から一致する文字インデックスを文字列抽出終了位置とする
            foreach (char ch in PARAMKEY_MATCH_CHAR)
            {
                // 一致する文字列が存在する場合
                if (str.IndexOf(ch) > -1)
                {
                    // 全ての文字インデックスをチェックして、インデックスが小さい方を選択
                    // ※文字列抽出終了位置初回設定時は-1
                    if (endIndex == -1 || endIndex > str.IndexOf(ch))
                    {
                        // 文字列抽出終了位置を設定
                        endIndex = str.IndexOf(ch);
                    }
                }
            }
            // パラメーターキー
            string paramKey;
            // 文字列抽出終了位置が取得出来た場合
            if (endIndex > -1)
            {
                // 抽出対象文字列からパラメーターキーを抽出
                paramKey = str.Substring(0, endIndex);
            }
            // 文字列抽出終了位置が取得出来なかった場合
            // ※既にパラメーターキーを抽出済みとして判定
            else
            {
                // 末尾まで全て取得
                paramKey = str;
            }

            // パラメーターキー配列に格納
            paramKeys[arrayIndex] = paramKey;
            // パラメーターキー配列に追加可能な場合
            if (paramKeys.Length > arrayIndex + 1)
            {
                // 配列格納用インデックスをカウントアップ
                arrayIndex++;
                // 切り出し後の文字列で再帰呼び出し
                ExtractParam(str, paramKeys, arrayIndex);
            }
            return paramKeys;
        }
    }
}
