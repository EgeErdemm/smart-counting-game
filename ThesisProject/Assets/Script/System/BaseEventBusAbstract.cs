using UnityEngine;

public abstract class BaseEventBusAbstract 
{
    protected IEventBus _eventBus;
    protected BaseLevelLoader levelLoader;


    public BaseEventBusAbstract()
    {
        //levelLoader = LevelLoader.Instance;
        levelLoader = Object.FindObjectOfType<BaseLevelLoader>();
        _eventBus = EventBus.Instance;
    }


}
