using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductService.Models;

namespace ProductService.Controllers;

[Route("api/product")]
[ApiController]
public class ProductServiceController(IHttpClientFactory httpClientFactory) : ControllerBase
{
    private readonly List<Product> _products = PredefinedProduct.Products;
    readonly HttpClient _client = httpClientFactory.CreateClient("ProductDetailServiceClient");

    [HttpPost("add")]
    [Authorize(Roles = "Admin")]
    public IActionResult AddProduct([FromBody] Product product)
    {
        try
        {
            product.ProductId = _products.Count + 1;
            _products.Add(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "An error has occurred");
        }
    }

    [HttpDelete("delete/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RemoveProduct(int id)
    {
        try
        {
            var product = _products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            // remove from product details as well
            var response = await _client.GetAsync($"delete-by-product-id/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Unexpected error occurred while deleting the product");
            }

            _products.Remove(product);
            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "An error has occurred");
        }
    }

    [HttpGet("get-all")]
    public IActionResult GetAllProducts()
    {
        try
        {
            return Ok(_products);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "An error has occurred");
        }
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        try
        {
            var product = _products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            // Fetch product details from ProductDetails service
            var productDetails = await GetProductDetailsAsync(product.ProductId);
            var productWithDetails = new
            {
                product.ProductId,
                product.Name,
                product.Description,
                ProductDetails = productDetails
            };
            return Ok(productWithDetails);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "An error has occurred");
        }
    }

    private async Task<List<ProductDetailDto>> GetProductDetailsAsync(int productId)
    {
        // Move the client to constructor level and address should point to api gateway
        try
        {
            var response = await _client.GetAsync($"get-all-by-product-id/{productId}"); // Adjust the URL as needed
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ProductDetailDto>>(jsonString);
            return result ?? [];
        }
        catch (Exception)
        {
            return [];
        }
    }
}