using Microsoft.AspNetCore.Mvc;
using libraryAPI.Models;
using libraryAPI.EfCore;
namespace libraryAPI.Controllers;

[ApiController]
[Route("api/library")]
public class LibraryController : ControllerBase
{
    private readonly DbHelper _db;
    public LibraryController(EF_DataContext eF_DataContext)
    {
        _db = new DbHelper(eF_DataContext);
    }

    [HttpGet]
    public IActionResult Get()
    {
        ResponseType type = ResponseType.Success;
        try
        {
            IEnumerable<RelationModel> data = _db.GetEntries();
            if(!data.Any())
            {
                type = ResponseType.NotFound;
            }
            return Ok(ResponseHandler.GetAppResponse(type, data));
        }
        catch (Exception ex)
        {
            type = ResponseType.Faliure;
            return BadRequest(ResponseHandler.GetExceptionResponse(ex));
        }
    }

    [HttpPost]
    public IActionResult Post(RelationModel model)
    {
        try
        {
            ResponseType type = ResponseType.Success;
            _db.PostEntry(model);
            return Ok(ResponseHandler.GetAppResponse(type, model));
        }
        catch (Exception ex)
        {
            return BadRequest(ResponseHandler.GetExceptionResponse(ex));
        }
    }
    [HttpDelete]
    public IActionResult Delete([FromBody] RelationModel model)
    {
        try
        {
            ResponseType type = ResponseType.Success;
            _db.DeleteEntry(model);
            return Ok(ResponseHandler.GetAppResponse(type, "Deleted Successfully"));
        }
        catch (Exception ex)
        {
            return BadRequest(ResponseHandler.GetExceptionResponse(ex));
        }
    }
}

[ApiController]
[Route("api/library/books")]
public class BookController : ControllerBase
{
    private readonly DbHelper _db;
    public BookController(EF_DataContext eF_DataContext)
    {
        _db = new DbHelper(eF_DataContext);
    }

    [HttpGet]
    public IActionResult Get()
    {
        ResponseType type = ResponseType.Success;
        try
        {
            IEnumerable<BookModel> data = _db.GetBooks();
            if(!data.Any())
            {
                type = ResponseType.NotFound;
            }
            return Ok(ResponseHandler.GetAppResponse(type, data));
        }
        catch (Exception ex)
        {
            type = ResponseType.Faliure;
            return BadRequest(ResponseHandler.GetExceptionResponse(ex));
        }
    }
    [HttpGet("search")]
    public IActionResult Search(BookModel model)
    {
        ResponseType type = ResponseType.Success;
        try
        {
            IEnumerable<BookModel> data = _db.SearchBook(model);
            if(!data.Any())
            {
                type = ResponseType.NotFound;
            }
            return Ok(ResponseHandler.GetAppResponse(type, data));
        }
        catch (Exception ex)
        {
            type = ResponseType.Faliure;
            return BadRequest(ResponseHandler.GetExceptionResponse(ex));
        }
    }
    [HttpPost]
    public IActionResult Post(BookModel model)
    {
        try
        {
            ResponseType type = ResponseType.Success;
            _db.PostBook(model);
            return Ok(ResponseHandler.GetAppResponse(type, model));
        }
        catch (Exception ex)
        {
            return BadRequest(ResponseHandler.GetExceptionResponse(ex));
        }
    }
    [HttpPut]
    public IActionResult Put(BookModel model)
    {
        try
        {
            ResponseType type = ResponseType.Success;
            _db.PostBook(model);
            return Ok(ResponseHandler.GetAppResponse(type, model));
        }
        catch (Exception ex)
        {
            return BadRequest(ResponseHandler.GetExceptionResponse(ex));
        }
    }
    [HttpDelete]
    public IActionResult Delete(BookModel model)
    {
        try
        {
            ResponseType type = ResponseType.Success;
            _db.DeleteBook(model);
            return Ok(ResponseHandler.GetAppResponse(type, model));
        }
        catch (Exception ex)
        {
            return BadRequest(ResponseHandler.GetExceptionResponse(ex));
        }
    }
}

[ApiController]
[Route("api/library/authors")]
public class AuthorController : ControllerBase
{
    private readonly DbHelper _db;
    public AuthorController(EF_DataContext eF_DataContext)
    {
        _db = new DbHelper(eF_DataContext);
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        ResponseType type = ResponseType.Success;
        try
        {
            IEnumerable<AuthorModel> data = _db.GetAuthors();
            if(!data.Any())
            {
                type = ResponseType.NotFound;
            }
            return Ok(ResponseHandler.GetAppResponse(type, data));
        }
        catch (Exception ex)
        {
            type = ResponseType.Faliure;
            return BadRequest(ResponseHandler.GetExceptionResponse(ex));
        }
    }
    [HttpGet("search")]
    public IActionResult Search(AuthorModel model)
    {
        ResponseType type = ResponseType.Success;
        try
        {
            IEnumerable<AuthorModel> data = _db.SearchAuthor(model);
            if(!data.Any())
            {
                type = ResponseType.NotFound;
            }
            return Ok(ResponseHandler.GetAppResponse(type, data));
        }
        catch (Exception ex)
        {
            type = ResponseType.Faliure;
            return BadRequest(ResponseHandler.GetExceptionResponse(ex));
        }
    }
    [HttpPost]
    public IActionResult Post(AuthorModel model)
    {
        try
        {
            ResponseType type = ResponseType.Success;
            _db.PostAuthor(model);
            return Ok(ResponseHandler.GetAppResponse(type, model));
        }
        catch (Exception ex)
        {
            return BadRequest(ResponseHandler.GetExceptionResponse(ex));
        }
    }
    [HttpPut]
    public IActionResult Put(AuthorModel model)
    {
        try
        {
            ResponseType type = ResponseType.Success;
            _db.PostAuthor(model);
            return Ok(ResponseHandler.GetAppResponse(type, model));
        }
        catch (Exception ex)
        {
            return BadRequest(ResponseHandler.GetExceptionResponse(ex));
        }
    }
    [HttpDelete]
    public IActionResult Delete(AuthorModel model)
    {
        try
        {
            ResponseType type = ResponseType.Success;
            _db.DeleteAuthor(model);
            return Ok(ResponseHandler.GetAppResponse(type, model));
        }
        catch (Exception ex)
        {
            return BadRequest(ResponseHandler.GetExceptionResponse(ex));
        }
    }
}