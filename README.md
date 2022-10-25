# Technical Assignment 2

## Library Management App

### Description
This is an app that can store information on books and authors.  
The app is a .NET WebAPI and has been written in C#. The API contacts a database to serve information, this database is stored in a GCP Cloud SQL instance of type PostgreSQL.  
It can be accessed by running an instance on local machine and also through the service running on GCP.

### How to access the app
- To run the app on local machine
    - Clone the repository and navigate to the folder in a terminal
    - Execute the following command
        ```Powershell
            docker-compose up
        ```
    - The app will now be accessible on [`http://localhost:3000/library`](http://localhost:3000/library/)
    - To close the app execute the following command in a terminal
        ```Powershell
            docker-compose down
        ```
- To access the app through GCP service, visit [`http://34.133.246.114/library/`](http://34.133.246.114/library/)

### How to use the app
The app provides standard CRUD functionalities which can be used in the following manner -
- `GET` requests -  
    <table>
    <tr>
    <td>URL</td>
    <td>JSON Parameter</td>
    <td>Required Fields</td>
    <td>Function</td>
    </tr>
    <tr>
    <td><a href="http://34.133.246.114/library/">/library</a></td>
    <td><i>Empty</i></td>
    <td><i>Empty</i></td>
    <td>Returns a list of books and their authors</td>
    </tr>
    <tr>
    <td><a href="http://34.133.246.114/library/books">/library/books</a></td>
    <td><i>Empty</i></td>
    <td><i>Empty</i></td>
    <td>Returns a list of books and their details</td>
    </tr>
    <tr>
    <td><a href="http://34.133.246.114/library/books/search">/library/books/search</a></td>
    <td>

    ```json
    {
        "name": "BookName",
    }
    ```

    </td>
    <td><li>name</li></td>
    <td>Searches a book by name and returns its details</td>
    </tr>
    <tr>
    <td><a href="http://34.133.246.114/library/authors">/library/authors</a></td>
    <td><i>Empty</i></td>
    <td><i>Empty</i></td>
    <td>Returns a list of authors and their details</td>
    </tr>
    <tr>
    <td><a href="http://34.133.246.114/library/authors/search">/library/authors/search</a></td>
    <td>

    ```json
    {
        "name": "AuthorName",
    }
    ```

    </td>
    <td><li>name</li></td>
    <td>Searches an author by name and returns their details</td>
    </tr>
    </table>
- `POST` requests -
    <table>
    <tr>
    <td>URL</td>
    <td>JSON Parameter</td>
    <td>Required Fields</td>
    <td>Function</td>
    </tr>
    <tr>
    <td><a href="http://34.133.246.114/library/">/library</a></td>
    <td>

    ```json
    {
        "book_name": "BookName",
        "book_dateOfFirstPublication": "DateOfFirstPublicationOfBook",
        "author_name": "AuthorName",
        "author_dateOfBirth": "DateOfBirthOfAuthor"
    }
    ```

    </td>
    <td><li>book_name</li><li>book_dateOfFirstPublication</li><li>author_name</li><li>author_dateOfBirth</li></td>
    <td>Creates a new book and author mapping</td>
    </tr>
    <tr>
    <td><a href="http://34.133.246.114/library/books">/library/books</a></td>
    <td>

    ```json
    {
        "name": "BookName",
        "dateOfFirstPublication": "DateOfFirstPublicationOfBook"
    }
    ```

    </td>
    <td><li>book_name</li><li>book_dateOfFirstPublication</li></td>
    <td>Creates a new book entry</td>
    </tr>
    <tr>
    <td><a href="http://34.133.246.114/library/authors">/library/authors</a></td>
    <td>

    ```json
    {
        "name": "AuthorName",
        "dateOfBirth": "DateOfBirthOfAuthor"
    }
    ```

    </td>
    <td><li>author_name</li><li>author_dateOfBirth</li></td>
    <td>Creates a new author entry</td>
    </tr>
    </table>
- `PUT` requests -
    <table>
    <tr>
    <td>URL</td>
    <td>JSON Parameter</td>
    <td>Required Fields</td>
    <td>Function</td>
    </tr>
    <tr>
    <td><a href="http://34.133.246.114/library/books">/library/books</a></td>
    <td>

    ```json
    {
        "name": "BookName",
        "dateOfFirstPublication": "DateOfFirstPublicationOfBook",
        "edition": "BookEdition",
        "publisher": "BookPublisher",
        "originalLanguage": "OriginalLanguageOfBook"
    }
    ```

    </td>
    <td><li>name</li><li>dateOfFirstPublication</li></td>
    <td>Updates an existing book entry</td>
    </tr>
    <tr>
    <td><a href="http://34.133.246.114/library/authors">/library/authors</a></td>
    <td>

    ```json
    {
        "name": "AuthorName",
        "dateOfBirth": "DateOfBirthOfAuthor",
        "country": "CountryOfAuthor"
    }
    ```

    </td>
    <td><li>name</li><li>dateOfBirth</li></td>
    <td>Updates an existing author entry</td>
    </tr>
    </table>
- `DELETE` requests -
    <table>
    <tr>
    <td>URL</td>
    <td>JSON Parameter</td>
    <td>Required Fields</td>
    <td>Function</td>
    </tr>
    <tr>
    <td><a href="http://34.133.246.114/library/">/library</a></td>
    <td>

    ```json
    {
        "book_name": "BookName",
        "book_dateOfFirstPublication": "DateOfFirstPublicationOfBook",
        "author_name": "AuthorName",
        "author_dateOfBirth": "DateOfBirthOfAuthor"
    }
    ```

    </td>
    <td><li>book_name</li><li>book_dateOfFirstPublication</li><li>author_name</li><li>author_dateOfBirth</li></td>
    <td>Deletes an existing book and author mapping</td>
    </tr>
    <tr>
    <td><a href="http://34.133.246.114/library/books">/library/books</a></td>
    <td>

    ```json
    {
        "name": "BookName",
        "dateOfFirstPublication": "DateOfFirstPublicationOfBook"
    }
    ```

    </td>
    <td><li>name</li><li>dateOfFirstPublication</li></td>
    <td>Deletes an existing book entry</td>
    </tr>
    <tr>
    <td><a href="http://34.133.246.114/library/authors">/library/authors</a></td>
    <td>

    ```json
    {
        "name": "AuthorName",
        "dateOfBirth": "DateOfBirthOfAuthor"
    }
    ```

    </td>
    <td><li>name</li><li>dateOfBirth</li></td>
    <td>Deletes an existing author entry</td>
    </tr>
    </table>

### Specifications
Create a data model of the books and authors and develop CRUD REST APIs to manage it.  
The API should be developed and published as in the first assignment, i.e., it should be dockerized and published through a CICD pipeline.

### Assignment Tasks
1. ~~Design the data schema for a relational database (use PostgreSQL), and use SQL statements to create the needed tables~~

2. ~~Use DotNet tools to create the matching classes in C# and associate them with the database tables (probably DotNet has some ORM – Object Relational Mapping) to aid this part, that is to persist in-memory objects to a database, and retrieve them from the database to memory~~ The database queries have been written without the use of an ORM

3. ~~Design and implement the API endpoints to manage the data model (support CRUD operation through standart HTTP REST semantics)~~

4. ~~Develop, Dockerize and Deploy as in the first Assignment.~~