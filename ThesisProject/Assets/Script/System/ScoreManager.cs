using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private LevelLoader levelLoader;
    private int score;
    private IEventBus _eventBus;

    private void Start()
    {
        levelLoader = LevelLoader.Instance;
    }

    private void OnEnable()
    {
        _eventBus = EventBus.Instance;
        _eventBus.Subscribe<ResetPlayerScoreEvent>(OnResetScore);
        PlayerMoveEvent.OnPlayerMove += setScore;
    }

    private void OnDisable()
    {
        _eventBus.UnSubscribe<ResetPlayerScoreEvent>(OnResetScore);
        PlayerMoveEvent.OnPlayerMove -= setScore;

    }


    private void OnResetScore(ResetPlayerScoreEvent evnt)
    {
        score = 0;
        _eventBus.Publish(new PlayerScoreChangedEvent(score));
    }

    private void setScore(int index)
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
