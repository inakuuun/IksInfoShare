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

namespace IksNativeClient.Logic.BootCertification
{
    public class WindowsCertificationLogic : IBootCertificationLogic
    {
        /// <summary>
        /// DBロジック
        /// </summary>
        private DbLogic _dbLogic = CommonDef.DbLogic;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bootCertificationModel"></param>
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
