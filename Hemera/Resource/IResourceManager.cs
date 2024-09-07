namespace Hemera.Resource;

public interface IResourceManager
{
     IResourceNodeHolder<T> LoadResourceNode<T>(string dmain, string path) where T : IResourceNode;
     IResource LoadResource<T>(string domain, string path) where T : IResource;
     IResourceNodeHolder<T> LoadStreamResourceNode<T>(string domain, string path) where T : IStreamResourceNode;
}