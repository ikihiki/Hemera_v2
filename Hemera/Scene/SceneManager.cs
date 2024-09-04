using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Hemera.Scene;

public class SceneManager(SceneConfiguration config,  IServiceScopeFactory serviceScopeFactory, ILogger<SceneManager> logger) : ISceneManager, IHostedService
{
    ConcurrentDictionary<IScene, SceneContext> scenes = new();
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting scene manager");
        await StartScene(config.FirstSceneType, cancellationToken);
        logger.LogInformation("Scene manager started");
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Stopping scene manager");
        var tasks = new List<Task>();
        foreach (var scene in scenes.Keys)
        {
            tasks.Add(scene.Stop(cancellationToken).AsTask());
        }

        await Task.WhenAll(tasks);
        logger.LogInformation("Scene manager stopped");
    }
    
    
    public async ValueTask StartScene(Type sceneType, CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting scene {SceneType}", sceneType);
        var scope = serviceScopeFactory.CreateScope();
        var scene = (IScene)scope.ServiceProvider.GetRequiredService(sceneType);
        scenes.TryAdd(scene, new SceneContext(scene, scope));
        scene.OnEnd += OnEndScene;
        await scene.Start(cancellationToken);
        logger.LogInformation("Scene {SceneType} started", sceneType);
    }

    private void OnEndScene(IScene scene)
    {
        logger.LogInformation("Scene {SceneType} ended", scene.GetType());
        scene.OnEnd -= OnEndScene;
        if (scenes.TryRemove(scene, out var context))
        {
            context.Scope.Dispose();
        }
    }
}

public class SceneContext(IScene scene, IServiceScope scope)
{
    internal IScene Scene { get; } = scene;
    internal IServiceScope Scope { get; } = scope;
}