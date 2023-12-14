using ComBase.Db;
using ComBase.Logs;
using IksNativeClient.Common.Db;
using IksNativeClient.Common.Db.Bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ComBase.Common.StractDef;

namespace IksNativeClient.Common.Db.Dao
{
    /// <summary>
    /// 共通Daoアクセスクラス
    /// </summary>
    /// <remarks>テーブル結合を行う際のSQLを実行するDaoアクセスクラス</remarks>
    public class CommonDaoAccess
    {
        /// <summary>
        /// ログファイル名
        /// </summary>
        private string _logFileName { get => typeof(CommonDaoAccess).Name ?? string.Empty; }

        /// <summary>
        /// DBロジック
        /// </summary>
        DbLogic _pObject;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="obj"></param>
        public CommonDaoAccess(DbLogic obj)
        {
            _pObject = obj;
        }

        /// <summary>
        /// チャット一覧テーブル取得処理
        /// </summary>
        public List<ChatListBeanEx> GetChatList()
        {
            List<ChatListBeanEx> chatLlistBeanExList = new();
            _pObject.SQLCommand((control) =>
            {
                try
                {
                    var br = new SqlBuilder(control);
                    br.Add("SELECT");
                    br.Add("    room_id");
                    br.Add("    , room_name");
                    br.Add("    , room_image");
                    br.Add("    , sent_date AS latest_sent_date");
                    br.Add("    , message AS latest_message ");
                    br.Add("FROM");
                    br.Add("    ( ");
                    br.Add("        SELECT");
                    br.Add("            chat_room.room_id");
                    br.Add("            , chat_room.room_name");
                    br.Add("            , chat_room.room_image");
                    br.Add("            , chat_message_history.sent_date");
                    br.Add("            , chat_message_history.message ");
                    br.Add("        FROM");
                    br.Add("            users ");
                    br.Add("            INNER JOIN chat_room ");
                    br.Add("                ON chat_room.create_user_id = users.user_id ");
                    br.Add("            LEFT JOIN ( ");
                    br.Add("                SELECT");
                    br.Add("                    room_id");
                    br.Add("                    , sent_user_id");
                    br.Add("                    , sent_date");
                    br.Add("                    , message ");
                    br.Add("                FROM");
                    br.Add("                    chat_message_history ");
                    br.Add("                WHERE");
                    br.Add("                    sent_date = ( ");
                    br.Add("                        SELECT");
                    br.Add("                            MAX(sent_date) ");
                    br.Add("                        FROM");
                    br.Add("                            chat_message_history ");
                    br.Add("                        WHERE");
                    br.Add("                            room_id = :room_id", 1);
                    br.Add("                    )");
                    br.Add("            ) chat_message_history ");
                    br.Add("                ON chat_message_history.room_id = chat_room.room_id");
                    br.Add("            WHERE chat_room.room_id = :room_id", 1);
                    br.Add("    );");

                    // SQL実行
                    var result = control.ExecuteReader(br);

                    // 実行結果を取得
                    using var rd = new SqlReader(result);
                    ChatListBeanEx chatLlistBeanEx;
                    while (rd.Reader.Read())
                    {
                        chatLlistBeanEx = new()
                        {
                            RoomId = rd.ToShort("room_id"),
                            RoomName = rd.ToStr("room_name"),
                            RoomImage = rd.ToByte("room_image"),
                            LatestReceiveDate = rd.ToStr("latest_sent_date"),
                            LatestMessage = rd.ToStr("latest_message")
                        };
                        chatLlistBeanExList.Add(chatLlistBeanEx);
                    }
                }
                catch (Exception ex)
                {
                    Log.Trace(_logFileName, LOGLEVEL.ERROR, $"SQL実行時異常 => {ex}");
                }
            });
            return chatLlistBeanExList;
        }
    }
}
