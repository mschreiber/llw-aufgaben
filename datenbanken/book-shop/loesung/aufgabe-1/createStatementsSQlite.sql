drop table if exists book_orders;
drop table if exists warehouse_books;
drop table if exists warehouses;
drop table if exists orders;
drop table if exists books;
drop table if exists publishers;
drop table if exists customers;
drop table if exists autors;
drop table if exists states;

create table customers(
  customer_id integer primary key autoincrement, 
  first_name varchar(255) not null, 
  last_name varchar(255) not null, 
  email varchar(255) not null, 
  phone varchar(255)
);

create table states(
  state_id integer primary key autoincrement, 
  state varchar(50)
);

create table orders(
  order_id integer primary key autoincrement, 
  customer_id integer not null references customers(customer_id), 
  state_id integer not null references states(state_id),
  order_date datetime not null
);

create table publishers(
  publisher_id integer primary key autoincrement,
  name varchar(255) not null,
  city varchar(255) not null,
  website varchar(255)
);

create table autors(
  autor_id integer primary key autoincrement,
  first_name varchar(255) not null,
  last_name varchar(255) not null,
  year_of_birth integer
);
  
create table books(
  book_id integer primary key autoincrement,
  isbn varchar(100) not null,
  title varchar(255) not null,
  price deciman(8,2) not null,
  autor_id integer not null references autors(autor_id),
  publisher_id integer not null references publishers(publisher_id)
);

create table warehouses(
  warehouse_id integer primary key autoincrement,
  name varchar(255) not null
);

create table warehouse_books(
  warehouse_id integer not null references warehouses(warehouse_id),
  book_id integer not null references books(book_id),
  book_count integer not null
);
  
create table book_orders(
  book_id integer not null references books(book_id),
  order_id integer not null references orders(order_id),
  amount integer not null
);