using System.Resources;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hemera.Resource;

public static class ResourceExtensions
{
    public static IServiceCollection AddResource(this IServiceCollection services, Action<ResourceManagerBuilder> config)
    {
        var builder = new ResourceManagerBuilder();
        config(builder);
        services.AddSingleton<ResourceConfiguration>(builder.Build());
        services.AddSingleton<IResourceManager, ResourceManager>();
        foreach (var resourceResolver in builder.ResourceResolvers)
        {
            services.AddSingleton(typeof(IResourceResolver), resourceResolver);
        }
        foreach (var streamResourceResolver in builder.StreamResourceResolvers)
        {
            services.AddSingleton(typeof(IStreamResourceResolver), streamResourceResolver);
        }
        return services;
    }
    
}

public record ResourceConfiguration
{
}

public class ResourceManagerBuilder
{
    internal readonly List<Type> ResourceResolvers = new();
    internal readonly List<Type> StreamResourceResolvers = new();
    public ResourceManagerBuilder AddResourceResolver<T>() where T : IResourceResolver
    {
        ResourceResolvers.Add(typeof(T));
        return this;
    }
    public ResourceManagerBuilder AddStreamResourceResolver<T>() where T : IResourceResolver
    {
        StreamResourceResolvers.Add(typeof(T));
        return this;
    }
    
    public ResourceConfiguration Build()
    {
        return new ResourceConfiguration();
    }
}