using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ComBase.Threads
{
    /// <summary>
    /// スレッド基底クラス
    /// </summary>
    public abstract class ThreadBase
    {
        /// <summary>
        /// スレッド変数
        /// </summary>
        private Thread? _thread;

        /// <summary>
        /// スレッド名
        /// </summary>
        protected string? ThreadName { get; set; }

        /// <summary>
        /// スレッド名設定処理
        /// </summary>
        /// <remarks>BootManagerで実施するスレッドクラス生成時にスレッド名を設定</remarks>
        public void SetThreadName(string threadName)
        {
            ThreadName = threadName;
        }

        /// <summary>
        /// スレッド実行
        /// </summary>
        public void ThreadStart()
        {
            _thread = new Thread(new ThreadStart(ThreadRun));
            _thread.Start();
        }

        /// <summary>
        /// 派生クラスの全スレッドを実行
        /// </summary>
        protected abstract void ThreadRun();
    }
}
