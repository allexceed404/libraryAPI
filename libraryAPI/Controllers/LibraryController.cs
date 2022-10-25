// docker run --rm --name library-db -p 5000:5432 -d -e POSTGRES_PASSWORD=password -e POSTGRES_USER=user -e POSTGRES_DB=LibraryDB -v librarydata:/var/lib/postgresql/data postgres 

using Microsoft.AspNetCore.Mvc;
using System.Data;
using Npgsql;

using System.Text;
using System.Text.Json;

namespace libraryAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class LibraryController : ControllerBase
{
    private readonly IConfiguration _configuration;
    public LibraryController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public JsonResult Get()
    {
        string query = @"
            select book.name as ""book_name"", book.dateOfFirstPublication as ""book_dateOfFirstPublication"", 
                    author.name as ""author_name"", author.dateOfBirth as ""author_dateOfBirth""
                from book
                join relation on relation.book_name=book.name and relation.book_dateOfFirstPublication=book.dateOfFirstPublication
                join author on relation.author_name=author.name and relation.author_dateOfBirth=author.dateOfBirth
                order by author.name, book.dateOfFirstPublication;
        ";

        DataTable table = new DataTable();
        string sqlDataSource = _configuration.GetConnectionString("LibraryCon");
        NpgsqlDataReader myReader;
        using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
        {
            myCon.Open();
            using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
            {
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);

                myReader.Close();
                myCon.Close();

            }
        }

        return new JsonResult(table);
    }

    [HttpPost]
    public JsonResult Post(BookAuthor bookauthor)
    {
        string query = @"
            insert into book(name, dateOfFirstPublication) 
                select @book_name, @book_dateOfFirstPublication
                where not exists (
                    select 1 from book where name=@book_name and dateOfFirstPublication=@book_dateOfFirstPublication
                );
            insert into author(name, dateOfBirth) 
                select @author_name, @author_dateOfBirth
                where not exists (
                    select 1 from author where name=@author_name and dateOfBirth=@author_dateOfBirth
                );
            insert into relation(book_name, book_dateOfFirstPublication, author_name, author_dateOfBirth)
                select
                    @book_name, @book_dateOfFirstPublication,
                    @author_name, @author_dateOfBirth
                where not exists (
                    select 1 from relation where book_name=@book_name and book_dateOfFirstPublication=@book_dateOfFirstPublication and
                        author_name=@author_name and author_dateOfBirth=@author_dateOfBirth
                );
            
        ";

        string sqlDataSource = _configuration.GetConnectionString("LibraryCon");
        NpgsqlDataReader myReader;

        DataTable table = new DataTable();
        using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
        {
            myCon.Open();
            using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
            {
                myCommand.Parameters.AddWithValue("@book_name", bookauthor.book_name);
                myCommand.Parameters.AddWithValue("@book_dateOfFirstPublication", bookauthor.book_dateOfFirstPublication);
                myCommand.Parameters.AddWithValue("@author_name", bookauthor.author_name);
                myCommand.Parameters.AddWithValue("@author_dateOfBirth", bookauthor.author_dateOfBirth);
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);

                myReader.Close();
                myCon.Close();

            }
        }

        return new JsonResult("Added Successfully");
    }

    [HttpDelete]
    public JsonResult Delete(BookAuthor bookauthor)
    {
        string query = @"
            delete from relation
                where relation.book_name=(@book_name) and relation.book_dateOfFirstPublication=(@book_dateOfFirstPublication)
                and relation.author_name=(@author_name) and relation.author_dateOfBirth=(@author_dateOfBirth);
        ";

        string sqlDataSource = _configuration.GetConnectionString("LibraryCon");
        NpgsqlDataReader myReader;

        DataTable table = new DataTable();
        using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
        {
            myCon.Open();
            using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
            {
                myCommand.Parameters.AddWithValue("@book_name", bookauthor.book_name);
                myCommand.Parameters.AddWithValue("@book_dateOfFirstPublication", bookauthor.book_dateOfFirstPublication);
                myCommand.Parameters.AddWithValue("@author_name", bookauthor.author_name);
                myCommand.Parameters.AddWithValue("@author_dateOfBirth", bookauthor.author_dateOfBirth);
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);

                myReader.Close();
                myCon.Close();

            }
        }

        return new JsonResult("Deleted Successfully");
    }
}

[ApiController]
[Route("library/books")]
public class BookController : ControllerBase
{
    private readonly IConfiguration _configuration;
    public BookController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public JsonResult Get()
    {
        string query = @"
            select *
                from book;
        ";

        DataTable table = new DataTable();
        string sqlDataSource = _configuration.GetConnectionString("LibraryCon");
        NpgsqlDataReader myReader;
        using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
        {
            myCon.Open();
            using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
            {
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);

                myReader.Close();
                myCon.Close();

            }
        }

        return new JsonResult(table);
    }

    [HttpGet("search")]
    public JsonResult Get(Book book)
    {
        string query = @"
            select book.*, author.name as ""author_name"", author.dateOfBirth as ""author_dateOfBirth""
                from book
                join relation on relation.book_name=book.name and relation.book_dateOfFirstPublication=book.dateOfFirstPublication
                join author on relation.author_name=author.name and relation.author_dateOfBirth=author.dateOfBirth
                where book.name=(@book_name)
                order by author.name;
        ";

        DataTable table = new DataTable();
        string sqlDataSource = _configuration.GetConnectionString("LibraryCon");
        NpgsqlDataReader myReader;
        using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
        {
            myCon.Open();
            using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
            {
                myCommand.Parameters.AddWithValue("@book_name", book.name);
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);

                myReader.Close();
                myCon.Close();

            }
        }

        return new JsonResult(table);
    }

    [HttpPost]
    public JsonResult Post(Book book)
    {
        string query = @"
            insert into book(name, dateOfFirstPublication) 
                select @book_name, @book_dateOfFirstPublication
                where not exists (
                    select 1 from book where name=@book_name and dateOfFirstPublication=@book_dateOfFirstPublication
                );
        ";

        string sqlDataSource = _configuration.GetConnectionString("LibraryCon");
        NpgsqlDataReader myReader;

        DataTable table = new DataTable();
        using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
        {
            myCon.Open();
            using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
            {
                myCommand.Parameters.AddWithValue("@book_name", book.name);
                myCommand.Parameters.AddWithValue("@book_dateOfFirstPublication", book.dateOfFirstPublication);
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);

                myReader.Close();
                myCon.Close();

            }
        }

        return new JsonResult("Added Successfully");
    }

    [HttpPut]
    public JsonResult Put(Book book)
    {
        string query = @"
            update book set edition=(@edition), publisher=(@publisher), originalLanguage=(@originalLanguage)
	            where book.name=(@book_name) and book.dateOfFirstPublication=(@book_dateOfFirstPublication);
        ";

        string sqlDataSource = _configuration.GetConnectionString("LibraryCon");
        NpgsqlDataReader myReader;

        DataTable table = new DataTable();
        using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
        {
            myCon.Open();
            using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
            {
                myCommand.Parameters.AddWithValue("@edition", book.edition);
                myCommand.Parameters.AddWithValue("@publisher", book.publisher);
                myCommand.Parameters.AddWithValue("@originalLanguage", book.originalLanguage);
                myCommand.Parameters.AddWithValue("@book_name", book.name);
                myCommand.Parameters.AddWithValue("@book_dateOfFirstPublication", book.dateOfFirstPublication);
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);

                myReader.Close();
                myCon.Close();

            }
        }

        return new JsonResult("Updated Successfully");
    }

    [HttpDelete]
    public JsonResult Delete(Book book)
    {
        string query = @"
            delete from book where book.name=(@book_name) and book.dateOfFirstPublication=(@book_dateOfFirstPublication);
        ";

        string sqlDataSource = _configuration.GetConnectionString("LibraryCon");
        NpgsqlDataReader myReader;

        DataTable table = new DataTable();
        using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
        {
            myCon.Open();
            using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
            {
                myCommand.Parameters.AddWithValue("@book_name", book.name);
                myCommand.Parameters.AddWithValue("@book_dateOfFirstPublication", book.dateOfFirstPublication);
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);

                myReader.Close();
                myCon.Close();

            }
        }

        return new JsonResult("Deleted Successfully");
    }
}

[ApiController]
[Route("library/authors")]
public class AuthorController : ControllerBase
{
    private readonly IConfiguration _configuration;
    public AuthorController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public JsonResult Get()
    {
        string query = @"
            select *
                from Author;
        ";

        DataTable table = new DataTable();
        string sqlDataSource = _configuration.GetConnectionString("LibraryCon");
        NpgsqlDataReader myReader;
        using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
        {
            myCon.Open();
            using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
            {
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);

                myReader.Close();
                myCon.Close();

            }
        }

        return new JsonResult(table);
    }

    [HttpGet("search")]
    public JsonResult Get(Author author)
    {
        string query = @"
            select author.*, book.name as ""book_name"", book.dateOfFirstPublication as ""book_dateOfFirstPublication""
                from author
                join relation on relation.author_name=author.name and relation.author_dateOfBirth=author.dateOfBirth
                join book on relation.book_name=book.name and relation.book_dateOfFirstPublication=book.dateOfFirstPublication
                where author.name=(@author_name)
                order by book.dateOfFirstPublication;
        ";

        DataTable table = new DataTable();
        string sqlDataSource = _configuration.GetConnectionString("LibraryCon");
        NpgsqlDataReader myReader;
        using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
        {
            myCon.Open();
            using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
            {
                myCommand.Parameters.AddWithValue("@author_name", author.name);
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);

                myReader.Close();
                myCon.Close();

            }
        }

        return new JsonResult(table);
    }

    [HttpPost]
    public JsonResult Post(Author author)
    {
        string query = @"
            insert into author(name, dateOfBirth) 
                select @author_name, @author_dateOfBirth
                where not exists (
                    select 1 from author where name=@author_name and dateOfBirth=@author_dateOfBirth
                );
        ";

        string sqlDataSource = _configuration.GetConnectionString("LibraryCon");
        NpgsqlDataReader myReader;

        DataTable table = new DataTable();
        using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
        {
            myCon.Open();
            using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
            {
                myCommand.Parameters.AddWithValue("@author_name", author.name);
                myCommand.Parameters.AddWithValue("@author_dateOfBirth", author.dateOfBirth);
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);

                myReader.Close();
                myCon.Close();

            }
        }

        return new JsonResult("Added Successfully");
    }

    [HttpPut]
    public JsonResult Put(Author author)
    {
        string query = @"
            update author set country=(@country)
	            where author.name=(@author_name) and author.dateOfBirth=(@author_dateOfBirth);
        ";

        string sqlDataSource = _configuration.GetConnectionString("LibraryCon");
        NpgsqlDataReader myReader;

        DataTable table = new DataTable();
        using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
        {
            myCon.Open();
            using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
            {
                myCommand.Parameters.AddWithValue("@country", author.country);
                myCommand.Parameters.AddWithValue("@author_name", author.name);
                myCommand.Parameters.AddWithValue("@author_dateOfBirth", author.dateOfBirth);
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);

                myReader.Close();
                myCon.Close();

            }
        }

        return new JsonResult("Updated Successfully");
    }

    [HttpDelete]
    public JsonResult Delete(Author author)
    {
        string query = @"
            delete from author where author.name=(@author_name) and author.dateOfBirth=(@author_dateOfBirth);
        ";

        string sqlDataSource = _configuration.GetConnectionString("LibraryCon");
        NpgsqlDataReader myReader;

        DataTable table = new DataTable();
        using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
        {
            myCon.Open();
            using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
            {
                myCommand.Parameters.AddWithValue("@author_name", author.name);
                myCommand.Parameters.AddWithValue("@author_dateOfBirth", author.dateOfBirth);
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);

                myReader.Close();
                myCon.Close();

            }
        }

        return new JsonResult("Deleted Successfully");
    }
}