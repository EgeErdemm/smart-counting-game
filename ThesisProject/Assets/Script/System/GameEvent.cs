using System;
public static class GameEvent
{
    public static event Action OnWin;

    public static event Action OnLose;

    public static event Action OnNextGame;

    public static event Action OnReGame;



    public static void Lose()
    {
        OnLose?.Invoke();
    }

    public static void Win()
    {
        OnWin?.Invoke();
    }


    public static void NextGame()
    {
        OnNextGame?.Invoke();
    }

    public static void ReGame()
    {
        OnReGame?.Invoke();
    }


}
