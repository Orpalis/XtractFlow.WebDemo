using Microsoft.AspNetCore.Mvc;
using XtractFlow.Web.Services.DataStore;

namespace XtractFlow.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class Thumbnails(IDocumentDataStore documents) : ControllerBase
{
    [HttpGet("{documentId}")]
    public IActionResult GetThumbnail([FromRoute]Guid documentId)
    {
        var document = documents.GetDocumentById(documentId);
        if (document is null)
        {
            return NotFound();
        }
        
        return File(document.Thumbnail, "image/png");
    }
}