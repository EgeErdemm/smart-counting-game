using System;


public static class PlayerMoveEvent 
{
    public static event Action<int> OnPlayerMove;

    public static event Action OnGetPlayer;

    public static event Action<int> OnGetIndex;


    public static void PlayerMoved(int index)
    {
        OnPlayerMove?.Invoke(index);
    }

    public static void getPlayer()
    {
        OnGetPlayer?.Invoke();
    }

    public static void getIndex(int index)
    {
        OnGetIndex?.Invoke(index);
    }
}
