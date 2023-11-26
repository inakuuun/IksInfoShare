using IksNativeClient.Common.Common;
using IksNativeClient.Common.Db;
using IksNativeClient.Common.Db.Bean;
using IksNativeClient.Common.Models;
using IksNativeClient.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NGLB = IksNativeClient.Common.Common.CommonDef;

namespace IksNativeClient.Logic.BootCertification
{
    /// <summary>
    /// Windows：起動時認証ロジッククラス
    /// </summary>
    public class WindowsCertificationLogic : IBootCertificationLogic
    {
        /// <summary>
        /// DBロジック
        /// </summary>
        private DbLogic _dbLogic = NGLB.DbLogic;

        /// <summary>
        /// 起動時認証処理
        /// </summary>
        /// <param name="bootCertificationModel">起動時認証モデル</param>
        public void Certification(BootCertificationModel bootCertificationModel)
        {
            // ユーザーテーブルBean
            var usersBean = new UsersBean()
            {
                UserName = bootCertificationModel.UserName,
                Password = bootCertificationModel.Password
            };
            // 登録処理
            _dbLogic.UsersDaoAccess.UsersInsert(usersBean);
        }
    }
}
