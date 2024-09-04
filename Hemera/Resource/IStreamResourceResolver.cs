namespace Hemera.Resource;

public interface IStreamResourceResolver : IResourceResolver
{
    new IStreamResourceNode ResolveNode(ResourceKey path);
    IResourceNode IResourceResolver.ResolveNode(ResourceKey key) => ResolveNode(key);
}

public interface IStreamResourceResolver<out TNode, out TResource> : IStreamResourceResolver, IResourceResolver<TNode, TResource> where TNode : IStreamResourceNode where TResource : IResource
{
    new TNode ResolveNode(ResourceKey path);
    IResourceNode IResourceResolver.ResolveNode(ResourceKey key) => ResolveNode(key);
    IStreamResourceNode IStreamResourceResolver.ResolveNode(ResourceKey path) => ResolveNode(path);
    TNode IResourceResolver<TNode, TResource>.ResolveNode(ResourceKey path) => ResolveNode(path);
}
