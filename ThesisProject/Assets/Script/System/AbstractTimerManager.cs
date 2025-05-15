using System;
using System.Collections;
using UnityEngine;

public abstract class AbstractTimerManager : MonoBehaviour
{
    protected LevelLoader levelLoader;
    protected IEventBus _eventBus;


    protected virtual void Start()
    {
        levelLoader = LevelLoader.Instance;
    }

    protected Coroutine countdownCoroutine;


    protected virtual void OnEnable()
    {
        _eventBus = EventBus.Instance;
        _eventBus.Subscribe<RequestStartTimerEvent>(OnStartTimerRequested);
        _eventBus.Subscribe<RequestStopTimerEvent>(OnStopTimerRequested);
    }

    protected virtual void OnDisable()
    {
        _eventBus.UnSubscribe<RequestStartTimerEvent>(OnStartTimerRequested);
        _eventBus.UnSubscribe<RequestStopTimerEvent>(OnStopTimerRequested);
    }

    private void OnStartTimerRequested(RequestStartTimerEvent evt)
    {
        StartCountdown();
    }

    private void OnStopTimerRequested(RequestStopTimerEvent evt)
    {
        StopCountdown();
    }

    protected virtual void StartCountdown()
    {
        int seconds = levelLoader.levelData.totalTime;
        StopCountdown();
        countdownCoroutine = StartCoroutine(CountdownRoutine(seconds));
    }

    protected virtual void StopCountdown()
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
            countdownCoroutine = null;
        }
    }

    protected virtual IEnumerator CountdownRoutine(int totalTime)
    {
        _eventBus.Publish(new TimeUpEvent(false));

        for (int timer = totalTime; timer >= 0; timer--)
        {
            TimerEvents.Tick(timer);
            yield return new WaitForSeconds(1f);

            if (timer == 0)
            {
                _eventBus.Publish(new TimeUpEvent(true));
            }
        }
    }
}
