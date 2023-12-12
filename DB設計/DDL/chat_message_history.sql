-- チャットメッセージ履歴テーブル
DROP TABLE chat_message_history; 

CREATE TABLE IF NOT EXISTS chat_message_history( 
    message_id integer not null primary key 
    , room_id integer not null
    , sent_user_id integer not null
    , binary_message blob
    , message text
    , sent_date timestamp not null
    , message_kind integer not null
    , foreign key (room_id) references chat_room(room_id)
    , foreign key (sent_user_id) references users(user_id)
);
