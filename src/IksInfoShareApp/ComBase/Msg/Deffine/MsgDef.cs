using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComBase.Msg.Deffine
{
    /// <summary>
    /// 電文メッセージ定義クラス
    /// </summary>
    public static class MsgDef
    {
        /// <summary>システム起動完了通知</summary>
        public const short MSG_SYSTEMBOOT_NOTICE = 0;

        /// <summary>ヘルスチェック要求</summary>
        public const short MSG_HELTHCHECK_REQ = 20;
        /// <summary>初期起動通知要求</summary>
        public const short MSG_BOOTSTART_REQ = 21;

        /// <summary>ヘルスチェック応答</summary>
        public const short MSG_HELTHCHECK_RES = 40;
        /// <summary>初期起動通知応答</summary>
        public const short MSG_BOOTSTART_RES = 41;
    }
}
