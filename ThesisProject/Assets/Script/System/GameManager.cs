
public class GameManager :BaseGamaManager
{

    private void OnEnable()
    {
        _eventBus = EventBus.Instance;
        PlayerMoveEvent.OnPlayerMove += SeaAbleArea;
        ScoreEvent.OnPlayerScoreChanged += UpdateScore;
        _eventBus.Subscribe<TimeUpEvent>(OnTimeUp);
        GameEvent.OnNextGame += NextLevel;
        GameEvent.OnReGame += ReGame;
        PlayerMoveEvent.OnGetIndex += GetPlayerIndex;
    }

    private void UpdateScore(int score)
    {
        Score = score;
        _winCheck.CheckWin(Score, timeIsUp);
    }


    private void OnTimeUp(TimeUpEvent evnt)
    {
        if (evnt.IsTimeUp)
        {
            GameEvent.Lose();
        }
    }
    
    private void GetPlayerIndex(int index)
    {
        SeaAbleArea(index);
    }

    private void SeaAbleArea(int index)
    {
        _seaAbleArea.SeaAble(index);
    }

    public void NextLevel()
    {
        _nextLevelLoader.LoadNextLevel();
    }

    public void ReGame()
    {
        _nextLevelLoader.ReLoadLevel();
    }

}
