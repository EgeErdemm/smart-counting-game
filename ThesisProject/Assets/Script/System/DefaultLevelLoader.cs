using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultLevelLoader : BaseEventBusAbstract, INextLevelLoader
{
    public void LoadNextLevel()
    {
        ClearScene();
        PlayerMoveEvent.getPlayer();

        levelLoader.LevelStarter();

        _eventBus.Publish(new ResetPlayerScoreEvent());

    }

    public void ReLoadLevel()
    {
        ClearScene();
        PlayerMoveEvent.getPlayer();

        levelLoader.LevelStarter();

        _eventBus.Publish(new ResetPlayerScoreEvent());

    }

    private void ClearScene()
    {
        if (levelLoader.player != null)
            GameObject.Destroy(levelLoader.player);

        if(levelLoader.gridParentRectTransform != null)
        {
            foreach (Transform child in levelLoader.gridParentRectTransform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

  
        foreach (Transform child in levelLoader.GridParent)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

}
