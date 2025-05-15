using System;

public static class TimerEvents
{
    // ⏰ Her saniye tetiklenir
    public static event Action<int> OnTick;

    // ⌛ Süre bittiğinde tetiklenir
    public static event Action<bool> OnTimeUp;

    public static event Action OnRequestStartTimer;

    public static event Action OnRequestStopTimer;

    public static void RequestStopTimer()
    {
        OnRequestStopTimer?.Invoke();
    }

    public static void RequestStartTimer()
    {
        OnRequestStartTimer?.Invoke();
    }

    // ⏱ Bu metodlar diğer sınıflar tarafından çağrılır
    public static void Tick(int secondsLeft)
    {
        OnTick?.Invoke(secondsLeft);
    }

    public static void TimeUp(bool b)
    {
        OnTimeUp?.Invoke(b);
    }
}
