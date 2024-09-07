namespace Hemera.Resource;

public interface IResourceNodeHolder<T> where T : IResourceNode<T>
{
    T Node { get; }
}



public interface IResourceNodeObserver<T> where T : IResourceNode<T>
{
    void OnNextNode(T node);
}


public class ResourceNodeHolder<T> : IResourceNodeHolder<T>, IResourceNodeObserver<T> where T : IResourceNode<T>
{
    public T Node { get; private set; }
    private IDisposable subscription;
    
    
    public ResourceNodeHolder(T node)
    {
        Node = node;
        subscription = node.Subscribe(this);
    }
    public void OnNextNode(T node)
    {
        subscription.Dispose();
        Node = node;
        subscription = node.Subscribe(this);
    }
}