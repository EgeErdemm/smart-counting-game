using System.Collections;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    private LevelLoader levelLoader;
    private Coroutine countdownCoroutine;


    private void Start()
    {
        levelLoader = LevelLoader.Instance;

    }

    private void OnEnable()
    {
        TimerEvents.OnRequestStartTimer += StartCountdown;
        TimerEvents.OnRequestStopTimer += StopCountdown;
    }

    private void OnDisable()
    {
        TimerEvents.OnRequestStartTimer -= StartCountdown;
        TimerEvents.OnRequestStopTimer -= StopCountdown;
    }



    public void StartCountdown()
    {
        int totalTimeInSeconds = levelLoader.levelData.totalTime;
        StopCountdown(); // Eski varsa iptal et
        countdownCoroutine = StartCoroutine(CountdownRoutine(totalTimeInSeconds));
    }

    public void StopCountdown()
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
            countdownCoroutine = null;
        }
    }

    private IEnumerator CountdownRoutine(int totalTime)
    {
        TimerEvents.TimeUp(false);

        for (int timer = totalTime; timer >= 0; timer--)
        {
            TimerEvents.Tick(timer);
            yield return new WaitForSeconds(1f);

            if (timer == 0)
            {
                Debug.Log("TimeUp");
                TimerEvents.TimeUp(true);
            }
        }
    }

}
