using Hemera.Resource;

namespace Hemera.Test.Resource;

public class ResoureceKeyTest
{

    [Test]
    public void Parse()
    {
        var key = "sss://arg.co/dba";
        var resourceKey = new ResourceKey(key);
        Assert.That(resourceKey.Scheme, Is.EqualTo( "sss"));
        Assert.That(resourceKey.Domain, Is.EqualTo("arg.co"));
        Assert.That(resourceKey.Path, Is.EqualTo("dba"));
    }
}