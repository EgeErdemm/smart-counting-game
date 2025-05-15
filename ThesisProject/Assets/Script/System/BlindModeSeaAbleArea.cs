using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BlindModeSeaAbleArea : BaseEventBusAbstract, ISeaAbleArea
{
    private GameObject tile;


    public void SeaAble(int index)
    {
        int gridX = index % levelLoader.levelData.gridWidth;
        int gridY = index / levelLoader.levelData.gridWidth;

        if (levelLoader.levelData.isBlind == true)
        {
            for (int y = 0; y < levelLoader.levelData.gridHeight; y++)
            {
                for (int x = 0; x < levelLoader.levelData.gridWidth; x++)
                {
                    tile = levelLoader.tileGrid[y, x];
                    if (tile == null) continue;

                    if ((Mathf.Abs(gridX - x) == 1 && gridY == y) || (Mathf.Abs(gridY - y) == 1 && gridX == x))
                    {
                        tile.GetComponent<Image>().DOColor(Color.white, 0.35f);

                    }
                    else
                    {
                        tile.GetComponent<Image>().DOColor(Color.black, 0.5f);
                    }

                }
            }
        }
    }
}
