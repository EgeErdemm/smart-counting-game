using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;


public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    [SerializeField] private TextMeshProUGUI TargetScoreTxt;

    [SerializeField] private TimerManager timerManager;

    [SerializeField] private GameObject WinPanel, LosePanel;


    private LevelLoader levelLoader;


    private void Start()
    {
        levelLoader = LevelLoader.Instance;
        TargetsTextUpdate();
    }



    private void OnEnable()
    {
        TimerEvents.OnTick += UpdateTimerText;
       // TimerEvents.OnTimeUp += ShowLosePanel;
        PlayerMoveEvent.OnPlayerMove += RefreshEatenTile;
        PlayerMoveEvent.OnPlayerMove += (x) => TargetsTextUpdate();
        GameEvent.OnLose += ShowLosePanel;
        GameEvent.OnWin += ShowWinPanel;
        GameEvent.OnNextGame += CloseWinPanel;
        GameEvent.OnReGame += CloseLosePanel;

        GameEvent.OnNextGame += TargetsTextUpdate;
        GameEvent.OnReGame += TargetsTextUpdate;
    }


    private void OnDisable()
    {
        TimerEvents.OnTick -= UpdateTimerText;
        //TimerEvents.OnTimeUp -= ShowLosePanel;
        PlayerMoveEvent.OnPlayerMove -= RefreshEatenTile;

    }

    private void UpdateTimerText(int secondsLeft)
    {
        int minutes = secondsLeft / 60;
        int seconds = secondsLeft % 60;
        timerText.text = $"{minutes}:{seconds:D2}";
    }

    private void ShowLosePanel()
    {
        LosePanel.transform.DOScale(new Vector3(1, 1, 1), 1f);
    }


    private void ShowWinPanel()
    {
        WinPanel.transform.DOScale(new Vector3(1, 1, 1), 1f);

    }

    private void CloseWinPanel()
    {
        WinPanel.transform.DOScale(new Vector3(0f, 0f, 0f), 1f);

    }

    private void CloseLosePanel()
    {
        LosePanel.transform.DOScale(new Vector3(0f, 0f, 0f), 1f);

    }

    private void RefreshEatenTile(int index)
    {
        int gridX = index % levelLoader.levelData.gridWidth;
        int gridY = index / levelLoader.levelData.gridWidth;

        TextMeshProUGUI text = levelLoader.tileGrid[gridY, gridX].GetComponentInChildren<TextMeshProUGUI>();
        if (text == null)
        {
            Debug.LogError("text is null");
        }
        if (levelLoader.tileGrid[gridY, gridX] == null)
        {
            Debug.LogError("tilegrid is null");
        }
        text.text = "0";
    }



    private void TargetsTextUpdate()
    {
        int targetScore = levelLoader.levelData.targetScore;

        targetScore = levelLoader.levelData.targetScore;
        TargetScoreTxt.text = "Target Score: " + targetScore;

    }

    public void NextGame()
    {
        GameEvent.NextGame();
    }

    public void ReGame()
    {
        GameEvent.ReGame();
    }

}
