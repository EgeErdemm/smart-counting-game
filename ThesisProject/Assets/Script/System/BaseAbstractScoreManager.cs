using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public abstract class BaseAbstractScoreManager : IDisposable
{
    protected ILevelDataProvider levelLoader;
    protected int score;
    protected IEventBus _eventBus;


    public BaseAbstractScoreManager( ILevelDataProvider dataProvider)
    {
        levelLoader = dataProvider;
        _eventBus = EventBus.Instance;
        _eventBus.Subscribe<ResetPlayerScoreEvent>(OnResetScore);
        PlayerMoveEvent.OnPlayerMove += setScore;
        _eventBus.Subscribe<TargetDestEvent>(OnEventTargetDest);
    }

    private void OnEventTargetDest(TargetDestEvent obj)
    {
        int i = (int)(obj.target.x + -obj.target.z * levelLoader.levelData.gridWidth); // index of grid
        int value = levelLoader.levelData.grid[i];
        Debug.Log("value: " + value + " index: " + i);
        score += value * AddSubUI.AddSubMode;
        Debug.Log(score);
        ScoreEvent.PlayerScoreChanged(score); // Yay!

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

        ScoreEvent.PlayerScoreChanged(score); // Yay!
        Debug.Log(score);
    }


    public int GetScore()
    {
        return score;
    }

    public virtual void Dispose()
    {
        _eventBus.UnSubscribe<ResetPlayerScoreEvent>(OnResetScore);
        PlayerMoveEvent.OnPlayerMove -= setScore;
    }
}
