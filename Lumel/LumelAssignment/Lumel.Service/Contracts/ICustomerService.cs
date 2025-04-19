using Lumel.Data.Entities;
using Lumel.Dto;

namespace Lumel.Service;

public interface ICustomerService
{
    Task AddOrUpdateAsync(List<CustomerDto> dto);
    Task<Customer?> GetByIdAsync(int customerId);
}