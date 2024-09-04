using Hemera.Resource;
using Hemera.Session;
using Hemera.Scene;

namespace Hemera.Test;


public class TestScene(IResourceManager resourceManager) : IScene
{
    public ValueTask Start(CancellationToken cancellationToken)
    {
        Task.Run(async () =>
        {
            var resource =  resourceManager.LoadResource<StringResource>("contentRoot", "test.txt");
            
            await Task.Delay(TimeSpan.FromMilliseconds(20));
            OnEnd?.Invoke(this);
        });
        return new ValueTask();
    }

    public ValueTask Stop(CancellationToken cancellationToken)
    {
        OnEnd?.Invoke(this);
        return new ValueTask();
    }

    public event Action<IScene>? OnEnd;
}





public class HostSessionTest
{
    [Test]
    public async Task Test1()
    {
        var session = new HostSession(builder =>
        {
            builder.Services.AddScene(config =>
            {
                config.AddScene<TestScene>();
                config.SetFirstScene<TestScene>();
            });
            builder.Services.AddResource(config =>
            {
                config.AddStreamResourceResolver<FileStreamResourceResolver>();
                config.AddResourceResolver<StringResourceResolver>();
            });
        });
        
       await session.Start(default);

       await Task.Delay(TimeSpan.FromMilliseconds(100));
       await session.Stop(default);
    }
}