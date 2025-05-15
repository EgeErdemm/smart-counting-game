using System;

public static class ScoreEvent
{
    public static event Action<int> OnPlayerScoreChanged;

    public static event Action OnResetPlayerScore;

 

    public static void ResetPlayerScore()
    {
        OnResetPlayerScore?.Invoke();
    }

    public static void PlayerScoreChanged(int score)
    {
        OnPlayerScoreChanged?.Invoke(score);
    }
}
