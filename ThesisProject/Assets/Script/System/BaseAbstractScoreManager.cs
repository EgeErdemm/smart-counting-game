using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public abstract class BaseAbstractScoreManager : MonoBehaviour
{
    protected LevelLoader levelLoader;
    protected int score;
    protected IEventBus _eventBus;

    protected virtual void Start()
    {
        levelLoader = LevelLoader.Instance;
    }

    protected virtual void OnEnable()
    {
        _eventBus = EventBus.Instance;
        _eventBus.Subscribe<ResetPlayerScoreEvent>(OnResetScore);
        PlayerMoveEvent.OnPlayerMove += setScore;
    }

    protected virtual void OnDisable()
    {
        _eventBus.UnSubscribe<ResetPlayerScoreEvent>(OnResetScore);
        PlayerMoveEvent.OnPlayerMove -= setScore;

    }


    protected virtual void OnResetScore(ResetPlayerScoreEvent evnt)
    {
        score = 0;
        _eventBus.Publish(new PlayerScoreChangedEvent(score));
    }

    protected virtual void setScore(int index)
    {
        int value = levelLoader.levelData.grid[index];
        levelLoader.levelData.grid[index] = 0;
        score += value * AddSubUI.AddSubMode;

        //
        TextMeshProUGUI playerText = levelLoader.player.GetComponentInChildren<TextMeshProUGUI>();
        playerText.text = score.ToString();
        //
        ScoreEvent.PlayerScoreChanged(score); // Yay!
    }


    public int GetScore()
    {
        return score;
    }
}
