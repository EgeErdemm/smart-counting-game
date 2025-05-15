using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastTimeManager : AbstractTimerManager
{
 

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
