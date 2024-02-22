using Consul;
using Microsoft.Extensions.Configuration;

namespace OrderAPI;

public class ServiceDiscoveryHostedService : IHostedService
{
    private readonly IConfiguration configuration;
    private readonly IConsulClient consulClient;
    private string registrationId= string.Empty;
    public ServiceDiscoveryHostedService(IConfiguration configuration)
    {
        this.configuration = configuration;

        consulClient = new ConsulClient(config =>
        {
            config.Address = configuration.GetValue<Uri>("ServiceConfig:ServiceDiscoveryAddress");
        });
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var serviceName = configuration.GetValue<string>("ServiceConfig:ServiceName");
        var serviceId = configuration.GetValue<string>("ServiceConfig:ServiceId");
        var serviceAddress = configuration.GetValue<Uri>("ServiceConfig:ServiceAddress");

        registrationId = $"{serviceName}-{serviceId}";

        var registration = new AgentServiceRegistration
        {
            ID = registrationId,
            Address = serviceAddress.Host,
            Name = serviceName,
            Port = serviceAddress.Port
        };

        //await consulClient.Agent.ServiceDeregister(registration.ID, cancellationToken);
        await consulClient.Agent.ServiceRegister(registration, cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await consulClient.Agent.ServiceDeregister(registrationId, cancellationToken);
    }
}
