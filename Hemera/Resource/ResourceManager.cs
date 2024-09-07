using System.Collections.Concurrent;
using System.Collections.Frozen;

namespace Hemera.Resource;

public class ResourceManager: IResourceManager
{
    private readonly FrozenDictionary<Type, IResourceResolver[]> nodeResolvers;
    private readonly FrozenDictionary<Type, IResourceResolver[]> resourceResolvers;
    private readonly FrozenDictionary<Type, IStreamResourceResolver[]> streamResourceResolvers;
    private readonly ConcurrentDictionary<ResourceKey, IResource> resourceCache = new();


    public ResourceManager(IEnumerable<IResourceResolver> resourceResolvers,
        IEnumerable<IStreamResourceResolver> streamResourceResolvers)
    {
        var nodeDict = new Dictionary<Type, List<IResourceResolver>>();
        var resolvers = resourceResolvers as IResourceResolver[] ?? resourceResolvers.ToArray();
        foreach (var resolver in resolvers)
        {
            if (!nodeDict.TryGetValue(resolver.NodeType, out var list))
            {
                list = new List<IResourceResolver>();
                nodeDict.Add(resolver.NodeType, list);
            }
            list.Add(resolver);
        }
        this.nodeResolvers = nodeDict.Select(kvp=> new KeyValuePair<Type,IResourceResolver[]>(kvp.Key, kvp.Value.ToArray())).ToFrozenDictionary();
        
        var resourceDict = new Dictionary<Type, List<IResourceResolver>>();
        foreach (var resolver in resolvers)
        {
            if (!resourceDict.TryGetValue(resolver.ResourceType, out var list))
            {
                list = new List<IResourceResolver>();
                resourceDict.Add(resolver.ResourceType, list);
            }
            list.Add(resolver);
        }
        this.resourceResolvers = resourceDict.Select(kvp=> new KeyValuePair<Type,IResourceResolver[]>(kvp.Key, kvp.Value.ToArray())).ToFrozenDictionary();
        
        var streamResourceDict = new Dictionary<Type, List<IStreamResourceResolver>>();
        var streamResolvers = streamResourceResolvers as IStreamResourceResolver[] ?? streamResourceResolvers.ToArray();
        foreach (var resolver in streamResolvers)
        {
            if (!streamResourceDict.TryGetValue(resolver.NodeType, out var list))
            {
                list = new List<IStreamResourceResolver>();
                streamResourceDict.Add(resolver.NodeType, list);
            }
            list.Add(resolver);
        }
        this.streamResourceResolvers = streamResourceDict.Select(kvp=> new KeyValuePair<Type,IStreamResourceResolver[]>(kvp.Key, kvp.Value.ToArray())).ToFrozenDictionary();
        
    }
    
    public  IResourceNodeHolder<T> LoadResourceNode<T>(string domain, string path) where T : IResourceNode
    {
        var type = typeof(T);
        var resolvers = nodeResolvers[typeof(T)];
        var key = new ResourceKey($"{type.FullName}://{domain}/{path}");
        foreach (var resolver in resolvers)
        {
            if (resolver.CanResolveNode(key))
            {
                return new ResourceNodeHolder<T>((T)resolver.ResolveNode(key));
            }
        }
        
        throw new Exception($"No resolver found for {key}");
    }
    
    public IResource LoadResource<T>(string domain, string path) where T : IResource
    {
        var type = typeof(T);
        var resolvers = resourceResolvers[typeof(T)];
        var key = new ResourceKey($"{type.FullName}://{domain}/{path}");
        if (resourceCache.TryGetValue(key, out var value))
        {
            return value;
        }
        
        
        foreach (var resolver in resolvers)
        {
            if (resolver.CanResolveResource(key))
            {
                var resource = resolver.ResolveResource(key);
                resourceCache.TryAdd(key, resource);
                return resource;
            }
        }
        
        throw new Exception($"No resolver found for {key}");
    }
    
    public IResourceNodeHolder<T> LoadStreamResourceNode<T>(string domain, string path) where T : IStreamResourceNode
    {
        var type = typeof(T);
        var resolvers = streamResourceResolvers[typeof(T)];
        var key = new ResourceKey($"{type.FullName}://{domain}/{path}");
        foreach (var resolver in resolvers)
        {
            if (resolver.CanResolveNode(key))
            {
                return new ResourceNodeHolder<T>((T) resolver.ResolveNode(key));
            }
        }
        
        throw new Exception($"No resolver found for {key}");
    }
}



