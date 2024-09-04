namespace Hemera.Scene;

public class SceneManagerBuilder
{
    internal readonly List<Type> SceneTypes = [];
    private Type? firstSceneType = default;
    
    public SceneManagerBuilder AddScene<T>() where T : IScene
    {
        SceneTypes.Add(typeof(T));
        return this;
    }
    
    public SceneManagerBuilder SetFirstScene<T>() where T : IScene
    {
        firstSceneType = typeof(T);
        return this;
    }
    
    public SceneConfiguration Build()
    {
        if(firstSceneType == null)
        {
            throw new InvalidOperationException("First scene type is not set.");
        }
        
        
        return new SceneConfiguration(firstSceneType);
    }
}