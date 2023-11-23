using IksNativeClient.Common.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IksNativeClient.Common.Common
{
    /// <summary>
    /// 共通定義クラス
    /// </summary>
    public static class CommonDef
    {
        /// <summary>
        /// DBロジッククラス
        /// </summary>
        public static DbLogic DbLogic { get; set; } = new DbLogic();
    }
}
