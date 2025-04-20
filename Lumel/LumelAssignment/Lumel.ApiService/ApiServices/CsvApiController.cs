using Lumel.Service;
using Microsoft.AspNetCore.Mvc;

namespace Lumel.ApiService.ApiServices;
[Route("api/csv")]
[ApiController]
public class CsvApiController(ICsvService csvService):ControllerBase
{
    [HttpPost("process")]
    public async Task<IActionResult> ProcessFile()
    {
        try
        {
            await csvService.ProcessFile();
            return Ok("File is process successfully");
        }
        catch
        {
            return BadRequest("An error has occurred while processing the file. Please check the logs");
        }
    }
}