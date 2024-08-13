using System.Security.Claims;
using CartService.Implementation;
using CartService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Int32;

namespace CartService.Controllers.Commands;

[Route("api/cart")]
[Authorize(Roles = "User")]
[ApiController]
public class CartServiceController(ICartService cartService):ControllerBase
{
    [HttpPost("add")]
    public async Task<IActionResult> AddToCart(ProductReservation productReservation)
    {
        try
        {
            var user = User.FindFirstValue(claimType: ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(user))
                return Unauthorized("Session is timed out. Please login again.");
            if (!TryParse(user, out var userId))
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error has occurred");
            var result = await cartService.AddToCart(userId, productReservation);
            return result.Item1 ? StatusCode(StatusCodes.Status201Created, result.Item2) : BadRequest(result.Item2);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "An error has occurred");
        }
    }

    [HttpPost("remove/{productId}/{productDetailId}")]
    public IActionResult RemoveAnItem(int productId, int productDetailId)
    {
        try
        {
            var user = User.FindFirstValue(claimType: ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(user))
                return Unauthorized("Session is timed out. Please login again.");
            if (!TryParse(user, out var userId))
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error has occurred");
            var result = cartService.RemoveItem(userId, productId, productDetailId);
            return result.Item1 ? StatusCode(StatusCodes.Status200OK, result.Item2) : BadRequest(result.Item2);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "An error has occurred");
        }
    }
    
    [HttpPost("add-quantity/{productId}/{productDetailId}/{quantity}")]
    public IActionResult AddQuantity(int productId, int productDetailId, int quantity)
    {
        try
        {
            var user = User.FindFirstValue(claimType: ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(user))
                return Unauthorized("Session is timed out. Please login again.");
            if (!TryParse(user, out var userId))
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error has occurred");
            var result = cartService.AddQuantity(userId, productId, productDetailId, quantity);
            return result.Item1 ? StatusCode(StatusCodes.Status200OK, result.Item2) : BadRequest(result.Item2);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "An error has occurred");
        }
    }
    
    [HttpPost("delete-quantity/{productId}/{productDetailId}/{quantity}")]
    public IActionResult DeleteQuantity(int productId, int productDetailId, int quantity)
    {
        try
        {
            var user = User.FindFirstValue(claimType: ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(user))
                return Unauthorized("Session is timed out. Please login again.");
            if (!TryParse(user, out var userId))
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error has occurred");
            var result = cartService.DeleteQuantity(userId, productId, productDetailId, quantity);
            return result.Item1 ? StatusCode(StatusCodes.Status200OK, result.Item2) : BadRequest(result.Item2);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "An error has occurred");
        }
    }
   
}