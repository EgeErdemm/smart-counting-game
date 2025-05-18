using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class BaseLevelLoader : MonoBehaviour
{
    public GameObject[,] tileGrid;
    public LevelData levelData;

    private int gridWidth;
    private int gridHeight;

    protected abstract GameObject BombTile { get; }
    protected abstract GameObject TilePrefab { get; }
    protected abstract Transform GridParent { get; }
    protected abstract float CellSize { get; }
    protected abstract RectTransform gridParentRectTransform { get; }//
    protected abstract void SetTilePosition(GameObject tile, int x, int y);



    protected virtual void LoadLevel()
    {

        if (levelData.grid == null || levelData.gridHeight == null)
        {
            Debug.Log("level data eror");
        }

        gridWidth = levelData.gridWidth;
        gridHeight = levelData.gridHeight;

        tileGrid = new GameObject[levelData.gridHeight, levelData.gridWidth];


        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                int index = y * gridWidth + x;
                int value = levelData.grid[index];


                //GameObject tile = Instantiate(TilePrefab, GridParent);
                GameObject tile = Instantiate(TilePrefab); 
                tile.transform.SetParent(GridParent, false);
                //tile.GetComponent<RectTransform>().anchoredPosition = new Vector2(x * CellSize, -y * CellSize);
                SetTilePosition(tile, x, y);
                ITextProvider textProvider = new TextProvider(tile);
                textProvider.SetText(value.ToString());

                if (levelData.isBlind == true)
                {
                    //isBlack(tile);
                }

                tileGrid[y, x] = tile;
                if (value == -1)
                {
                    GameObject bomb = Instantiate(BombTile, GridParent);
                    bomb.GetComponent<RectTransform>().anchoredPosition = new Vector2(x * CellSize, -y * CellSize);
                    tileGrid[y, x] = bomb;

                }
            }
        }

        Debug.Log("Map created");
    }





    private void isBlack(GameObject tile)
    {
        tile.GetComponent<Image>().color = Color.black;
    }


    private void Start()
    {

        LevelStarter();

    }


    public void LevelStarter()
    {
        MakeLevelData();

        LoadLevel();
        Invoke(nameof(SceneOrder),0.2f);

    }

    protected virtual void SceneOrder()
    {
        //gridParentRectTransform.anchoredPosition = new Vector3(-(gridWidth - 1) * CellSize * 0.5f, (gridHeight - 1) * CellSize * 0.5f, 0f);

        ////player = Instantiate(PlayerTilePrefab, gridParent);
        //int PlayerX = levelData.startX;
        //float startPosX = PlayerX * cellSize;
        //int PlayerY = levelData.startY;
        //float startPosY = PlayerY * cellSize;
        //player.GetComponent<RectTransform>().anchoredPosition = new Vector2(startPosX, startPosY);

    }


    private void MakeLevelData()
    {
        Debug.Log("Make data started");
        levelData = new LevelData();
        levelData.targetScore = Random.Range(45, 100);
        levelData.gridWidth = Random.Range(4, 10);
        levelData.gridHeight = Random.Range(4, 10);
        levelData.startX = 0;
        levelData.startY = 0;
        levelData.totalTime = 60;
        levelData.isBlind = Random.Range(0, 2) == 0;
        //%50 change to blind mode
        int tileCount = levelData.gridHeight * levelData.gridWidth;
        levelData.grid = new int[tileCount];
        for (int i = 0; i < tileCount; i++)
        {
            levelData.grid[i] = Random.Range(1, 15);
        }
        Debug.Log("Make data Finished");

    }


}
