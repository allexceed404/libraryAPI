using Microsoft.AspNetCore.Mvc;
using libraryAPI.Models;
using libraryAPI.EfCore;
namespace libraryAPI.Controllers;

[ApiController]
[Route("api/library/dummydata")]
public class DummyDataController : ControllerBase
{
    private readonly DbHelper _db;
    public DummyDataController(EF_DataContext eF_DataContext)
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
}