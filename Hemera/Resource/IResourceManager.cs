namespace Hemera.Resource;

public interface IResourceManager
{
     IResourceNode LoadResourceNode<T>(string dmain, string path) where T : IResourceNode;
     IResource LoadResource<T>(string domain, string path) where T : IResource;
     IStreamResourceNode LoadStreamResourceNode<T>(string domain, string path) where T : IStreamResourceNode;
}