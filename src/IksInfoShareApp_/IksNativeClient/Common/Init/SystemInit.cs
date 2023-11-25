using ComBase.Common;
using ComBase.FileUtil;
using ComBase.Logs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static ComBase.Common.StractDef;

namespace IksNativeClient.Common.Init
{
    /// <summary>
    /// 起動準備クラス
    /// </summary>
    /// <remarks>プロパティの読み込みなど、システム起動時に必要な情報を事前にメモリに積んでおく</remarks>
    public class SystemInit
    {
        /// <summary>
        /// ログファイル名
        /// </summary>
        private string _logFileName { get => typeof(SystemInit).Name ?? string.Empty; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SystemInit()
        {
            try
            {
                // ログ出力先ディレクトリを作成
                // ※システム起動後一番最初にログ出力先ディレクトリを作成しておく
                Log.CreateLogDirectory();

                // DB定義ファイル設定
                PropertyReader.SetPropertyAll("./Config/DbProperties.xml");

                // データベース実行クラスを生成
                // ※DB接続情報をDB定義ファイルから取得するため、DB定義ファイル取得後に実施
                _ = new ComBase.Db.DbControllerFactory(DB.SQLite);
            }
            catch (Exception ex)
            {
                Log.Trace(_logFileName, LOGLEVEL.ERROR, $"初期処理異常 => {ex}");
            }
        }
    }
}
