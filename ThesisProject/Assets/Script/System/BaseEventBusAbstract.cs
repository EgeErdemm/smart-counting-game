

public abstract class BaseEventBusAbstract 
{
    protected IEventBus _eventBus;
    protected LevelLoader levelLoader;


    public BaseEventBusAbstract()
    {
        levelLoader = LevelLoader.Instance;
        _eventBus = EventBus.Instance;
    }


}
