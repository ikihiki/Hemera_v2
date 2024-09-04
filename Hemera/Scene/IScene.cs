namespace Hemera.Scene;

public interface IScene
{
    ValueTask Start(CancellationToken cancellationToken);
    ValueTask Stop(CancellationToken cancellationToken);

    event Action<IScene> OnEnd;
}