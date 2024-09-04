namespace Hemera.Resource;

public class FileStreamResourceResolver: IStreamResourceResolver<FileStreamResourceNode, FileStreamResource>
{
    public bool CanResolveNode(ResourceKey key)
    {
        return true;
    }

    public FileStreamResourceNode ResolveNode(ResourceKey path)
    {
        return new FileStreamResourceNode();
    }

    public bool CanResolveResource(ResourceKey key)
    {
        return true;
    }

    public FileStreamResource ResolveResource(ResourceKey key)
    {
        return new FileStreamResource();
    }
}

public class FileStreamResourceNode: IStreamResourceNode
{
    
}

public class FileStreamResource: IResource
{
    
}