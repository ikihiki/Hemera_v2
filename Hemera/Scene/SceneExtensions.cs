using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hemera.Scene;

public static class SceneExtensions
{
    public static IServiceCollection AddScene(this IServiceCollection services, Action<SceneManagerBuilder> config)
    {
        var builder = new SceneManagerBuilder();
        config(builder);
        services.AddSingleton<SceneConfiguration>(builder.Build());
        services.AddSingleton<ISceneManager, SceneManager>();
        services.AddSingleton<IHostedService, SceneManager>();
        foreach(var sceneType in builder.SceneTypes)
        {
            services.AddTransient(sceneType);
        }
        return services;
    }
}