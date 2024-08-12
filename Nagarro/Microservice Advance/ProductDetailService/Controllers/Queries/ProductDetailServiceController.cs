using Microsoft.AspNetCore.Mvc;
using ProductDetailService.Models;

namespace ProductDetailService.Controllers.Queries;

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
}