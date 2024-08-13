using System.Security.Claims;
using CheckoutService.Coordinator;
using CheckoutService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Int32;

namespace CheckoutService.Controllers;

[Route("api/checkout")]
[Authorize(Roles = "User")]
[ApiController]
public class CheckOutServiceController(ICheckOutCoordinator checkOutCoordinator) : ControllerBase
{
    [HttpPost("process")]
    public async Task<IActionResult> Checkout([FromBody] CheckOutRequestItem checkOutRequest)
    {
        try
        {
            var user = User.FindFirstValue(claimType: ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(user))
                return Unauthorized("Session is timed out. Please login again.");
            if (!TryParse(user, out var userId))
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error has occurred");
            
            var result = await checkOutCoordinator.ExecuteCheckOut(checkOutRequest.CartItems);
            return !result? StatusCode(StatusCodes.Status400BadRequest, "An error has occurred while processing the request"):
                Ok("Order is placed successfully");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "An error has occurred");
        }
    }
}