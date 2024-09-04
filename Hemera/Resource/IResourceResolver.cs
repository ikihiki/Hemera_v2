namespace Hemera.Resource;

public interface IResourceResolver
{
    Type NodeType { get; }
    bool CanResolveNode(ResourceKey key);
    IResourceNode ResolveNode(ResourceKey key);
    Type ResourceType { get; }
    bool CanResolveResource(ResourceKey key);
    IResource ResolveResource(ResourceKey key);
}


public interface IResourceResolver<out TNode, out TResource> : IResourceResolver where TNode : IResourceNode where TResource : IResource
{
    Type IResourceResolver.NodeType => typeof(TNode);
    Type IResourceResolver.ResourceType => typeof(TResource);
    new TNode ResolveNode(ResourceKey key);
    new TResource ResolveResource(ResourceKey key);
    IResourceNode IResourceResolver.ResolveNode(ResourceKey key) => ResolveNode(key);
    IResource IResourceResolver.ResolveResource(ResourceKey key) => ResolveResource(key);
}
