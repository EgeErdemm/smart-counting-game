using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBasedWinChecker : BaseEventBusAbstract , IGameWinCheck
{
    public void CheckWin(int currentScore, bool isTimeUp)
    {
        Debug.Log("check win");

        if (currentScore == levelLoader.levelData.targetScore && !isTimeUp)
        {
            Debug.Log("win");
            //
            GameEvent.Win();
            _eventBus.Publish(new ResetPlayerScoreEvent());
            _eventBus.Publish(new RequestStopTimerEvent());
        }
        else if (currentScore > levelLoader.levelData.targetScore)
        {
            Debug.Log("Lose");
            //
            GameEvent.Lose();
            _eventBus.Publish(new ResetPlayerScoreEvent());
            _eventBus.Publish(new RequestStopTimerEvent());

        }
    }

 
}
