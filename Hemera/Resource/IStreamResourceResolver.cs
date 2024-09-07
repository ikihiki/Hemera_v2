namespace Hemera.Resource;

public interface IStreamResourceResolver<out TNode, out TResource> :  IResourceResolver<TNode, TResource> where TNode : IStreamResourceNode<TNode> where TResource : IResource
{
    TNode ResolveNode(ResourceKey path);
    TNode IResourceResolver<TNode, TResource>.ResolveNode(ResourceKey path) => ResolveNode(path);
}
