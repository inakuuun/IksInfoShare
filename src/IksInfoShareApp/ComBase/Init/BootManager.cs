using ComBase.Threads;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace ComBase.Init
{
    /// <summary>
    /// スレッド起動管理クラス
    /// </summary>
    public class BootManager
    {
        /// <summary>
        /// ProgramInfo.xml情報格納用ディクショナリ
        /// </summary>
        private static Dictionary<string, ThreadManager> _programInfoDic = new();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BootManager()
        {
            SetProgramInfo();
        }

        /// <summary>
        /// ProgramInfo.xmlで定義されているスレッドをディクショナリに格納
        /// </summary>
        public void SetProgramInfo()
        {
            // XMLファイルからXDocumentをインスタンス化
            XDocument? xd = XDocument.Load("./Config/ProgramInfo.xml");
            // ルート要素を取得
            XElement? info = xd.Element("ProgramInfo");
            if (info != null)
            {
                IEnumerable items = info.XPathSelectElements("Threads/Item");
                foreach (XElement item in items)
                {
                    if (item != null)
                    {
                        // スレッド名を取得
                        string threadName = item.Attribute("threadName")?.Value ?? string.Empty;
                        // スレッドを実行するクラス名を名前空間も含めて取得
                        string className = item.Attribute("className")?.Value ?? string.Empty;

                        // クラス名からTypeオブジェクトを取得
                        Type? threadClass = Type.GetType(className);
                        if (threadClass != null)
                        {
                            // Typeオブジェクトを使用してクラスのインスタンスを生成
                            object? instance = Activator.CreateInstance(threadClass);
                            if (instance != null && instance is ThreadManager obj)
                            {
                                // 同じキーが存在しない場合
                                if (!_programInfoDic.ContainsKey(threadName))
                                {
                                    // 格納するインスタンスのスレッド名をここで設定しておく
                                    // ※RunInit()時に呼び出すのを避けたいため
                                    obj.SetThreadName(threadName);
                                    // classNameをキーにして、スレッド実行体をディクショナリに格納
                                    _programInfoDic.Add(threadName, obj);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 定義されているスレッドを実行
        /// </summary>
        public void SystemStart()
        {
            // スレッドを実行するインスタンス分ループ処理
            foreach (ThreadManager instance in _programInfoDic.Values)
            {
                // スレッドを実行
                instance.ThreadStart();
            }
        }
    }
}
