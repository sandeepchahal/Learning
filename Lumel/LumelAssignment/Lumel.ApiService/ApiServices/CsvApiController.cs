using Lumel.Service;
using Microsoft.AspNetCore.Mvc;

namespace Lumel.ApiService.ApiServices;
/// <summary>
/// CSV API to process the file
/// </summary>
/// <param name="csvService"></param>
[Route("api/csv")]
[ApiController]
public class CsvApiController(ICsvService csvService):ControllerBase
{
    /// <summary>
    /// Process file api method
    /// </summary>
    /// <returns></returns>
    [HttpPost("process")]
    public async Task<IActionResult> ProcessFile()
    {
        try
        {
            await csvService.ProcessFile(false);
            return Ok("File is processed successfully");
        }
        catch
        {
            return BadRequest("An error has occurred while processing the file. Please check the logs");
        }
    }
}