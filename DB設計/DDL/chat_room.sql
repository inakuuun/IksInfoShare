-- チャットルームテーブル
DROP TABLE chat_room; 

CREATE TABLE IF NOT EXISTS chat_room( 
    room_id integer not null primary key autoincrement
    , room_name varchar (20)
    , create_user_id integer not null
    , create_date timestamp not null
    , room_kind integer not null
    , foreign key (create_user_id) references users(user_id)
); 
