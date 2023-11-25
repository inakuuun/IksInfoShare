using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComBase.Db
{
    /// <summary>
    /// DBロジック基底クラス
    /// </summary>
    public abstract class DbLogicBase
    {
        /// <summary>
        /// SQL実行処理
        /// </summary>
        /// <param name="action"></param>
        protected void SQLCommand(Action<IDbControl> action)
        {
            using var ctrl = DbControllerFactory.GetControl();
            action(ctrl);
        }
    }
}
