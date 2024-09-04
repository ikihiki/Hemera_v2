using System.Collections.Concurrent;

namespace Hemera.Resource;

public class StringResourceResolver: IResourceResolver<IStringResourceNode, StringResource>
{
    ConcurrentDictionary<ResourceKey, IStringResourceNode> nodes = new();
    
    
    
    
    
    
    
    public bool CanResolveNode(ResourceKey key)
    {
        return true;
    }

    public IStringResourceNode ResolveNode(ResourceKey key)
    {
        throw new NotImplementedException();
    }



    public bool CanResolveResource(ResourceKey key)
    {
        return true;
    }
    
    public StringResource ResolveResource(ResourceKey key)
    {
             throw new NotImplementedException();
    }
}


public interface IStringResourceNode : IResourceNode
{
    string Value { get; }
}



public class LoadingStringResourceNode: IStringResourceNode
{
   public  string Value => throw new InvalidOperationException("Resource not loaded");
}

public class LoadedStringResourceNode(string value) : IStringResourceNode
{
    public string Value { get; } = value;
}

public class StringResource(IStringResourceNode node) : IResource
{
    public string Value => node.Value;
}