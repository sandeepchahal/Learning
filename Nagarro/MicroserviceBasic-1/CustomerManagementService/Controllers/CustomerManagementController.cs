using Microsoft.AspNetCore.Mvc;
using Models.CustomerModels;

namespace CustomerManagementService.Controllers;

[Route("api/customer")]
[ApiController]
public class CustomerManagementController(IHttpClientFactory clientFactory) : ControllerBase
{
    readonly HttpClient _client = clientFactory.CreateClient("AccountService");
    
    private static readonly List<Customer> Customers =
    [
        new Customer { Id = 1, Name = "John Doe", Email = "john@example.com", Phone = "1234567890" },
        new Customer { Id = 2, Name = "Jane Smith", Email = "jane@example.com", Phone = "9876543210" }
    ];

    [HttpPost("add")]
    public IActionResult AddCustomer(Customer customer)
    {
        customer.Id = Customers.Max(c => c.Id) + 1;
        Customers.Add(customer);
        return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
    }

    [HttpGet("get-all")]
    public IActionResult GetAllCustomers()
    {
        return Ok(Customers);
    }

    [HttpGet("get/{id}")]
    public IActionResult GetCustomer(int id)
    {
        var customer = Customers.FirstOrDefault(c => c.Id == id);
        if (customer == null)
            return NotFound();
        return Ok(customer);
    }

    [HttpPut("update/{id}")]
    public IActionResult UpdateCustomer(int id, Customer customer)
    {
        var existingCustomer = Customers.FirstOrDefault(c => c.Id == id);
        if (existingCustomer == null)
            return NotFound();

        existingCustomer.Name = customer.Name;
        existingCustomer.Email = customer.Email;
        existingCustomer.Phone = customer.Phone;

        return Ok(existingCustomer);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        var customer = Customers.FirstOrDefault(c => c.Id == id);
        if (customer == null)
            return NotFound();

        Customers.Remove(customer);

        // Call Account Management Service to delete the associated account
        if(await DeleteCustomerAccount(id))
            return Ok($"Customer  id- {id} is deleted successfully!");
        return BadRequest("An error has occurred");
    }

    private async Task<bool> DeleteCustomerAccount(int customerId)
    {
       var result = await _client.DeleteAsync($"delete-customer/{customerId}");
       return result.IsSuccessStatusCode;
    }
}