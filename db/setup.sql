-- Setup table 'book'
create table if not exists book(
    name varchar(500) not null,
    date_of_first_publication date not null,
    edition int,
    publisher varchar(100),
    original_language varchar(50),
    primary key (name, date_of_first_publication),
    unique (name, date_of_first_publication)
);

insert into book values('Harry Potter and the Philosopher''s Stone','1997-06-26',1,'Bloomsbury','English');
insert into book values('Harry Potter and the Chamber of Secrets','1998-07-02',1,'Bloomsbury','English');
insert into book values('Harry Potter and the Prisoner of Azkaban','1999-07-08',1,'Bloomsbury','English');
insert into book values('Game of Thrones','1996-08-01',1,'Harper Voyager','English');
insert into book values('A Brief History of Time','1988-09-24',1,'Bantam Books','English');
insert into book values('Blood of Elves','1994-06-25',1,'superNOWA','Polish');
insert into book values('Time of Contempt','1995-03-13',1,'superNOWA','Polish');
insert into book values('Baptism of Fire','1996-11-05',1,'superNOWA','Polish');


-- Setup table 'author'
create table if not exists author(
    name varchar(100) not null,
    date_of_birth DATE not null,
    country varchar(100),
    primary key (name, date_of_birth),
    unique (name, date_of_birth)
);

insert into author values('J.K. Rowling','1965-07-31','United Kingdom');
insert into author values('George R.R. Martin','1948-09-20','United States');
insert into author values('Stephen Hawking','1942-01-08','United Kingdom');
insert into author values('Andrzej Sapkowski','1948-06-21','Poland');


-- Setup table 'relation'
create table if not exists relation(
	book_name varchar(500),
	book_date_of_first_publication date,
	author_name varchar(100),
	author_date_of_birth date,
	foreign key (book_name, book_date_of_first_publication) references book(name, date_of_first_publication) on delete cascade,
	foreign key (author_name, author_date_of_birth) references author(name, date_of_birth) on delete cascade,
	primary key (book_name, book_date_of_first_publication, author_name, author_date_of_birth),
	unique (book_name, book_date_of_first_publication, author_name, author_date_of_birth)
);

insert into relation values('Harry Potter and the Philosopher''s Stone','1997-06-26','J.K. Rowling','1965-07-31');
insert into relation values('Harry Potter and the Chamber of Secrets','1998-07-02','J.K. Rowling','1965-07-31');
insert into relation values('Harry Potter and the Prisoner of Azkaban','1999-07-08','J.K. Rowling','1965-07-31');
insert into relation values('Game of Thrones','1996-08-01','George R.R. Martin','1948-09-20');
insert into relation values('A Brief History of Time','1988-09-24','Stephen Hawking','1942-01-08');
insert into relation values('Blood of Elves','1994-06-25','Andrzej Sapkowski','1948-06-21');
insert into relation values('Time of Contempt','1995-03-13','Andrzej Sapkowski','1948-06-21');
insert into relation values('Baptism of Fire','1996-11-05','Andrzej Sapkowski','1948-06-21');