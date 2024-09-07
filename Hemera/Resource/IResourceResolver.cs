namespace Hemera.Resource;


public interface IResourceResolver<out TNode, out TResource>  where TNode : IResourceNode<TNode> where TResource : IResource
{
    Type NodeType => typeof(TNode);
    Type ResourceType => typeof(TResource);
    new TNode ResolveNode(ResourceKey key);
    new TResource ResolveResource(ResourceKey key);
}
