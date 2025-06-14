// TimerEvents.cs
// Bu dosya EventBus ile kullanılmak üzere Timer ile ilgili tüm event sınıflarını içerir.

using System;

/// <summary>
/// Her saniye tetiklenen event.
/// </summary>
public class TickEvent
{
    public int SecondsLeft;

    public TickEvent(int secondsLeft)
    {
        SecondsLeft = secondsLeft;
    }
}

/// <summary>
/// Süre bitince tetiklenen event. True ise zaman dolmuş demektir.
/// </summary>
public class TimeUpEvent
{
    public bool IsTimeUp;

    public TimeUpEvent(bool isTimeUp)
    {
        IsTimeUp = isTimeUp;
    }
}

/// <summary>
/// Timer başlatılması istendiğinde yayınlanır.
/// </summary>
public class RequestStartTimerEvent
{
    // Veri taşımaz, sadece sinyal amaçlı
}

/// <summary>
/// Timer durdurulması istendiğinde yayınlanır.
/// </summary>
public class RequestStopTimerEvent
{
    // Veri taşımaz, sadece sinyal amaçlı
}
