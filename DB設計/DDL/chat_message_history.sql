-- チャットメッセージ履歴テーブル
DROP TABLE chat_message_history; 

CREATE TABLE IF NOT EXISTS chat_message_history( 
    message_id integer not null primary key autoincrement
    , room_id integer not null
    , sent_user_id integer not null
    , message blob
    , sent_date timestamp not null
    , message_kind integer not null
    , foreign key (room_id, sent_user_id) references chat_member(room_id, member_id)
);
