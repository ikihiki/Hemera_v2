namespace Hemera.Resource;

public interface IResourceNode<T> where T : IResourceNode<T>
{
    IDisposable Subscribe(IResourceNodeObserver<T> observer);
}