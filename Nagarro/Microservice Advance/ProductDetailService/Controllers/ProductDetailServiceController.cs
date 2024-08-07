using Microsoft.AspNetCore.Mvc;
using ProductDetailService.Models;

namespace ProductDetailService.Controllers;

[Route("api/product/detail")]
[ApiController]
public class ProductDetailServiceController : ControllerBase
{
    private static readonly List<ProductDetail> ProductDetails = new();

    [HttpPost("add/{productId}")]
    public IActionResult AddProductDetail(int productId, [FromBody] ProductDetail productDetail)
    {
        try
        {
            if (ProductDetails.FirstOrDefault(col => col.ProductId == productId) == null)
                return NotFound($"Product Id {productId} is not found");

            productDetail.ProductDetailId = ProductDetails.Count + 1;
            ProductDetails.Add(productDetail);
            return CreatedAtAction(nameof(GetProductDetails), new { productId = productDetail.ProductId },
                productDetail);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "An error has occurred");
        }
    }

    [HttpDelete("delete/{id}")]
    public IActionResult RemoveProductDetail(int id)
    {
        try
        {
            var productDetail = ProductDetails.FirstOrDefault(pd => pd.ProductDetailId == id);
            if (productDetail == null)
            {
                return NotFound();
            }

            ProductDetails.Remove(productDetail);
            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "An error has occurred");
        }
    }
    
    [HttpDelete("delete-by-product-id/{id}")]
    public IActionResult RemoveProductRangeDetail(int id)
    {
        try
        {
            var productDetail = ProductDetails.FirstOrDefault(pd => pd.ProductDetailId == id);
            if (productDetail == null)
            {
                return NotFound();
            }

            ProductDetails.RemoveAll(col => col.ProductId == id);
            return NoContent();
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