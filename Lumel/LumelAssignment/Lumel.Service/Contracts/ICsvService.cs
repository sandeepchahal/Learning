using Lumel.Dto;

namespace Lumel.Service;

public interface ICsvService
{
    Task AddOrUpdateAsync(CsvDto csvDto);
}