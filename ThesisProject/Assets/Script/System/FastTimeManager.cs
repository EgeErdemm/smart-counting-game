using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastTimeManager : AbstractTimerManager
{
    [SerializeField] private BaseLevelLoader baseLevelLoader;

    protected override void Start()
    {
        if (levelLoader == null || levelLoader.levelData == null)
        {
            levelLoader = baseLevelLoader;
        }
        Invoke(nameof(LateAssignLevelLoader), 0.05f);
    }

    private void LateAssignLevelLoader()
    {
        if (levelLoader == null || levelLoader.levelData == null)
        {
            levelLoader.levelData = baseLevelLoader.levelData;
        }
        else { Debug.Log("LEVEL LOADER NOT NULL"); }
    }

    protected override IEnumerator CountdownRoutine(int totalTime)
    {
        _eventBus.Publish(new TimeUpEvent(false));

        for (int timer = totalTime; timer >= 0; timer--)
        {
            TimerEvents.Tick(timer);
            yield return new WaitForSeconds(0.5f);

            if (timer == 0)
            {
                _eventBus.Publish(new TimeUpEvent(true));
            }
        }
    }
}
