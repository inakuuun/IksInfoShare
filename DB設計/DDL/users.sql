-- ユーザーテーブル
DROP TABLE users; 

CREATE TABLE IF NOT EXISTS users( 
    user_id integer not null primary key autoincrement
    , user_name varchar (20) not null
    , password varchar (20) not null
); 