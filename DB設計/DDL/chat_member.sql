-- チャットメンバーテーブル
DROP TABLE chat_member; 

CREATE TABLE IF NOT EXISTS chat_member( 
    room_id integer not null
    , member_id integer not null
    , foreign key (room_id) references chat_room(room_id)
    , foreign key (member_id) references users(user_id)
); 
