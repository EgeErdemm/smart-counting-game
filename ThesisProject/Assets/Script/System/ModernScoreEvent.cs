
public class ResetPlayerScoreEvent{ };

public class PlayerScoreChangedEvent
{
    public int Score;

    public PlayerScoreChangedEvent(int score)
    {
        Score = score;
    }

};