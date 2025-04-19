using Lumel.Data;
using Lumel.Data.Entities;
using Lumel.Dto;

namespace Lumel.Service;

public class CustomerService(LumelDbContext dbContext):ICustomerService
{
    public async Task AddOrUpdateAsync(List<CustomerDto> dto)
    { 
        List<Customer> customers = new List<Customer>();
       var result =  dto.Select(async col =>
        {
            var customer = await GetByIdAsync(col.Id);
            if (customer == null)
            {
                return new Customer()
                {
                    Id = col.Id,
                    Address = col.Address,
                    Email = col.Email,
                    Name = col.Name,
                    CreatedBy = "System",
                    CreatedOn = DateTime.Now,
                    DateOfBirth = col.DateOfBirth,
                    Gender = col.Gender
                };
            }
            customer.Address = col.Address;
            customer.DateOfBirth = col.DateOfBirth;
            customer.Name = col.Name;
            customer.Gender = col.Gender;
            customer.ModifiedBy = "System";
            customer.ModifiedOn = DateTime.Now;
            return null;
                
        }).Where(col=>col!= null).ToList();

        var results = await Task.WhenAll(result);
        customers.AddRange(results.Where(c => c != null)!);
        if (customers.Count != 0)
        {
            dbContext.Customers.AddRange(customers);
        }
        await dbContext.SaveChangesAsync();
    }

    public async Task<Customer?> GetByIdAsync(int customerId)
    {
        return await dbContext.Customers.FindAsync(customerId);
    }
}