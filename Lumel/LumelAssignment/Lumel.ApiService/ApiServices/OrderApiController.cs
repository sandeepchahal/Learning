using Lumel.Service;
using Microsoft.AspNetCore.Mvc;
namespace Lumel.ApiService.ApiServices;

[Route("api/order")]
[ApiController]
public class OrderApiController(IOrderService orderService):ControllerBase
{
    /// <summary>
    /// Get revenue calculations for a date range.
    /// </summary>
    /// <param name="startDate">Start date (format: yyyy-MM-dd)</param>
    /// <param name="endDate">End date (optional, format: yyyy-MM-dd)</param>
    [HttpGet("revenue")]
    public async Task<IActionResult> GetRevenue(
        [FromQuery] DateTime startDate, 
        DateTime? endDate)
    {
        if (startDate > endDate)
            return BadRequest("Start Date must be lesser than End Date");
        var result = await orderService.CalculateRevenue(startDate, endDate?? DateTime.Now);
        return Ok(result);
    }
}