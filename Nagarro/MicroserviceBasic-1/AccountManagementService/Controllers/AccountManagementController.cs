using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using AccountManagementService.Models;

namespace AccountManagementService.Controllers;

[Route("api/account")]
[ApiController]
public class AccountManagementController(IHttpClientFactory clientFactory) : ControllerBase
{
    readonly HttpClient _client = clientFactory.CreateClient("CustomerService");

    private static readonly List<Account> Accounts =
    [
        new Account { Id = 1, CustomerId = 1, Balance = 1000 },
        new Account { Id = 2, CustomerId = 2, Balance = 2000 }
    ];


    [HttpPost("addMoney/{customerId}")]
    public async Task<IActionResult> AddMoney(int customerId, decimal amount)
    {
        if (!await IsValidCustomerAsync(customerId))
            return BadRequest("Invalid customer");

        var account = Accounts.FirstOrDefault(a => a.CustomerId == customerId);
        if (account == null)
            return NotFound("Account not found");

        account.Balance += amount;
        return Ok(account);
    }

    [HttpPost("withdrawMoney")]
    public async Task<IActionResult> WithdrawMoney(int customerId, decimal amount)
    {
        if (!await IsValidCustomerAsync(customerId))
            return BadRequest("Invalid customer");

        var account = Accounts.FirstOrDefault(a => a.CustomerId == customerId);
        if (account == null)
            return NotFound("Account not found");

        if (account.Balance < amount)
            return BadRequest("Insufficient funds");

        account.Balance -= amount;
        return Ok(account);
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetAccountDetails(int id)
    {
        var account = Accounts.FirstOrDefault(a => a.Id == id);
        if (account == null)
            return NotFound();

        var customerDetails = await GetCustomerDetailsAsync(account.CustomerId);
        var result = new
        {
            Account = account,
            Customer = customerDetails
        };

        return Ok(result);
    }

    [HttpDelete("delete-customer/{customerId}")]
    public IActionResult DeleteAccount(int customerId)
    {
        var account = Accounts.FirstOrDefault(a => a.CustomerId == customerId);
        if (account == null)
            return NotFound();

        Accounts.Remove(account);
        return StatusCode(StatusCodes.Status200OK);
    }

    private async Task<bool> IsValidCustomerAsync(int customerId)
    {
        var response = await _client.GetAsync($"get/{customerId}");
        return response.IsSuccessStatusCode;
    }

    private async Task<object?> GetCustomerDetailsAsync(int customerId)
    {
        var response = await _client.GetAsync($"get/{customerId}");
        if (!response.IsSuccessStatusCode) return null;
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<object>(content);
    }
}