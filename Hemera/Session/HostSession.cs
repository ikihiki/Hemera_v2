using Microsoft.Extensions.Hosting;

namespace Hemera.Session;

public class HostSession(Action<HostApplicationBuilder> builder): ISession
{
    private IHost? currentHost = default;
    
    public async ValueTask Start(CancellationToken cancellationToken)
    {
        if (currentHost != null)
        {
            throw new InvalidOperationException("Host is already started");
        }
        
        
        var hostApplicationBuilder = new HostApplicationBuilder();
        builder(hostApplicationBuilder);
        var host = hostApplicationBuilder.Build();
        currentHost = host;
        await host.StartAsync(cancellationToken);
    }

    public async ValueTask Stop(CancellationToken cancellationToken)
    {
        if(currentHost == null)
        {
            throw new InvalidOperationException("Host is not started");
        }
        
        await currentHost.StopAsync(cancellationToken);
    }
}