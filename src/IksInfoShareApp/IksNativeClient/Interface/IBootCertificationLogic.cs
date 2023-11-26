using IksNativeClient.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IksNativeClient.Interface
{
    /// <summary>
    /// 起動時認証ロジックインタフェース
    /// </summary>
    public interface IBootCertificationLogic
    {
        /// <summary>
        /// 起動時認証処理
        /// </summary>
        /// <param name="bootCertificationModel">起動時認証モデル</param>
        void Certification(BootCertificationModel bootCertificationModel);
    }
}
