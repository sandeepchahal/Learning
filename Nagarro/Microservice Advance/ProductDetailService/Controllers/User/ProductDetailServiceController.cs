using Microsoft.AspNetCore.Mvc;
using ProductDetailService.Models;

namespace ProductDetailService.Controllers.User;

[Route("api/product/detail")]
[ApiController]
public class ProductDetailServiceController : ControllerBase
{
    private static readonly List<ProductDetail> ProductDetails = PredefinedProductDetail.GetProductDetails.ToList();
    
    [HttpGet("get-all")]
    public IActionResult GetAll()
    {
        try
        {
            return Ok(ProductDetails);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "An error has occurred");
        }
    }
    [HttpGet("get-all-by-product-id/{productId}")]
    public IActionResult GetProductDetails(int productId)
    {
        try
        {
            var productDetails = ProductDetails.Where(pd => pd.ProductId == productId).ToList();
            if (productDetails.Count == 0)
            {
                return NotFound();
            }

            return Ok(productDetails);
        } 
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "An error has occurred");
        }
    }
    
    [HttpPost("reserve")]
    public IActionResult ReserveIfAvailable([FromBody] ProductQuantityContext productQuantityContext)
    {
        try
        {
            var product = ProductDetails.FirstOrDefault(p =>  p.ProductDetailId == productQuantityContext.ProductDetailId);
            if (product == null)
            {
                return NotFound();
            }

            if (product.Quantity > productQuantityContext.Quantity)
                return StatusCode(StatusCodes.Status400BadRequest,
                    "Request cannot be processed. Quantity cannot be more than available quantity");
            product.Quantity -= productQuantityContext.Quantity;
            return StatusCode(StatusCodes.Status200OK, "Quantity is reserved successfully");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "An error has occurred");
        }
    }

    [HttpPost("update-quantity")]
    public IActionResult UpdateProductQuantity(ProductQuantityContext productQuantityContext)
    {
        var product = ProductDetails.FirstOrDefault(p => p.ProductDetailId == productQuantityContext.ProductDetailId);
        if (product == null)
        {
            return NotFound();
        }
        product.Quantity += productQuantityContext.Quantity;
        return StatusCode(StatusCodes.Status200OK, "Quantity is updated successfully");
    }

}