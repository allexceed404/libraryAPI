-- Setup table 'book'
create table if not exists book(
    name varchar(500) not null,
    dateOfFirstPublication date not null,
    edition int,
    publisher varchar(100),
    originalLanguage varchar(50),
    primary key (name, dateOfFirstPublication),
    unique (name, dateOfFirstPublication)
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
    dateOfBirth DATE not null,
    country varchar(100),
    primary key (name, dateOfBirth),
    unique (name, dateOfBirth)
);

insert into author values('J.K. Rowling','1965-07-31','United Kingdom');
insert into author values('George R.R. Martin','1948-09-20','United States');
insert into author values('Stephen Hawking','1942-01-08','United Kingdom');
insert into author values('Andrzej Sapkowski','1948-06-21','Poland');


-- Setup table 'relation'
create table if not exists relation(
	book_name varchar(500),
	book_dateOfFirstPublication date,
	author_name varchar(100),
	author_dateOfBirth date,
	foreign key (book_name, book_dateOfFirstPublication) references book(name, dateOfFirstPublication) on delete cascade,
	foreign key (author_name, author_dateOfBirth) references author(name, dateOfBirth) on delete cascade,
	primary key (book_name, book_dateOfFirstPublication, author_name, author_dateOfBirth),
	unique (book_name, book_dateOfFirstPublication, author_name, author_dateOfBirth)
);

insert into relation values('Harry Potter and the Philosopher''s Stone','1997-06-26','J.K. Rowling','1965-07-31');
insert into relation values('Harry Potter and the Chamber of Secrets','1998-07-02','J.K. Rowling','1965-07-31');
insert into relation values('Harry Potter and the Prisoner of Azkaban','1999-07-08','J.K. Rowling','1965-07-31');
insert into relation values('Game of Thrones','1996-08-01','George R.R. Martin','1948-09-20');
insert into relation values('A Brief History of Time','1988-09-24','Stephen Hawking','1942-01-08');
insert into relation values('Blood of Elves','1994-06-25','Andrzej Sapkowski','1948-06-21');
insert into relation values('Time of Contempt','1995-03-13','Andrzej Sapkowski','1948-06-21');
insert into relation values('Baptism of Fire','1996-11-05','Andrzej Sapkowski','1948-06-21');