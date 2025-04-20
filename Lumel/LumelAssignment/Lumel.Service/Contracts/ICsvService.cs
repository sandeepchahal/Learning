namespace Lumel.Service;

public interface ICsvService
{
    Task ProcessFile(bool isBackgroundExecutor = true);
}