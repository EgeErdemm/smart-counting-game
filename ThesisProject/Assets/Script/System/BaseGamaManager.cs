using UnityEngine;

public class BaseGamaManager : MonoBehaviour
{
    protected int Score;
    protected bool timeIsUp;
    protected IEventBus _eventBus;
    protected IGameWinCheck _winCheck;
    protected INextLevelLoader _nextLevelLoader;
    protected ISeaAbleArea _seaAbleArea;

    protected virtual void Awake()
    {
        _winCheck = new ScoreBasedWinChecker();
        _nextLevelLoader = new DefaultLevelLoader();
        _seaAbleArea = new BlindModeSeaAbleArea();
    }

    protected virtual void Start()
    {
        timeIsUp = false;
    }



}
