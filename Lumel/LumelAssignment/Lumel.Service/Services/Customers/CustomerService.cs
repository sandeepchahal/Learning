using Lumel.Data;
using Lumel.Data.Entities;
using Lumel.Dto;

namespace Lumel.Service;

public class CustomerService:ICustomerService
{
    private readonly LumelDbContext dbContext;

    // Constructor to initialize the DbContext
    public CustomerService(LumelDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task AddOrUpdateAsync(List<CustomerDto> dto,bool isBackgroundExecuter = true)
    { 
        List<Customer> customers = new List<Customer>();
        foreach (var col in dto)
        {
            var customer = await GetByIdAsync(col.Id);
            if (customer == null)
            {
                customers.Add(new Customer()
                {
                    Id = col.Id,
                    Address = col.Address,
                    Email = col.Email,
                    Name = col.Name,
                    CreatedBy = isBackgroundExecuter? "System":"API",
                    CreatedOn = DateTime.Now,
                    DateOfBirth = col.DateOfBirth,
                    Gender = col.Gender
                });
            }
            else
            {
                customer.Address = col.Address;
                customer.DateOfBirth = col.DateOfBirth;
                customer.Name = col.Name;
                customer.Gender = col.Gender;
                customer.ModifiedBy = isBackgroundExecuter? "System":"API";
                customer.ModifiedOn = DateTime.Now;
            }
        }
        if (customers.Count != 0)
        {
            dbContext.Customers.AddRange(customers);
        }
        await dbContext.SaveChangesAsync();
    }

    public async Task<Customer?> GetByIdAsync(string customerId)
    {
        return await dbContext.Customers.FindAsync(customerId);
    }
}