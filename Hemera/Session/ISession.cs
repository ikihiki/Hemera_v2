namespace Hemera.Session;

public interface ISession
{
    ValueTask Start(CancellationToken cancellationToken);
    ValueTask Stop(CancellationToken cancellationToken);
}