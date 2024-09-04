namespace Hemera.Resource;

public readonly struct ResourceKey : IEquatable<ResourceKey>
{
    private readonly string key;
    private readonly Range schemeRange;
    private readonly Range domainRange;
    private readonly Range pathRange;
    private readonly Range paramRange;

    public string Scheme => key[schemeRange];
    public string Domain => key[domainRange];
    public string Path => key[pathRange];
    
    public ResourceKey(string key)
    {
        this.key = key;
        (this.schemeRange, this.domainRange, this.pathRange, this.paramRange) = ParseKey(key);
    }

    private static (Range, Range, Range, Range) ParseKey(string key)
    {
        var schemeEnd = key.IndexOf(':');
        var schemeRange = new Range(0, schemeEnd);
        var domainStart = schemeEnd + 3;
        var domainEnd = key.IndexOf('/', domainStart);
        var pathStart = domainEnd + 1;
        var domainRange = new Range(domainStart, domainEnd);
        var paramStart = key.IndexOf('?', pathStart);
        var pathRange = paramStart == -1 
                ? new Range(pathStart, key.Length) 
                : new Range(pathStart, paramStart);
        
        var paramRange =  paramStart == -1 
            ? new Range(key.Length, key.Length) 
            : new Range(paramStart, key.Length);
        
        return (schemeRange, domainRange, pathRange, paramRange);
    }

    public bool Equals(ResourceKey other)
    {
        return key == other.key;
    }

    public override bool Equals(object? obj)
    {
        return obj is ResourceKey other && Equals(other);
    }

    public override int GetHashCode()
    {
        return key.GetHashCode();
    }
}