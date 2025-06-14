using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class BaseLevelLoader : MonoBehaviour ,ITileGridProvider , ILevelDataProvider
{
    public GameObject[,] tileGrid;
    public LevelData levelData;

    [HideInInspector] public GameObject player;

    protected int gridWidth;
    protected int gridHeight;

    protected abstract GameObject BombTile { get; }
    protected abstract GameObject TilePrefab { get; }
    public abstract Transform GridParent { get; }
    protected abstract float CellSize { get; }
    public abstract RectTransform gridParentRectTransform { get; }//

    protected IEventBus _eventbus;
    LevelData ILevelDataProvider.levelData => levelData;
    protected TileEater _tileEater;


    protected abstract void SetTilePosition(GameObject tile, int x, int y);

    public GameObject GetTile(int x, int y)
    {
        return tileGrid[y, x]; // dikkat: [y, x]
    }

    protected virtual void OnEnable()
    {
        _eventbus = EventBus.Instance;
        _tileEater = new TileEater(this,this); // tile and leveldata provider send
    }

    protected void PublishTargetScore()
    {
        _eventbus.Publish(new TargetScoreEvent(levelData.targetScore));// listeners ui manager
        _eventbus.Publish(new RequestStartTimerEvent());
    }

    protected virtual void LoadLevel()
    {

        gridWidth = levelData.gridWidth;
        gridHeight = levelData.gridHeight;

        tileGrid = new GameObject[levelData.gridHeight, levelData.gridWidth];

        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                int index = y * gridWidth + x;
                int value = levelData.grid[index];


                GameObject tile = Instantiate(TilePrefab); 
                tile.transform.SetParent(GridParent, false);
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


    protected virtual void isBlack(GameObject tile)
    {
        tile.GetComponent<Image>().color = Color.black;
    }


    protected void Start()
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
        Invoke(nameof(PublishTargetScore), 0.1f);
    }


    protected virtual void MakeLevelData()
    {
        levelData = new LevelData();
        levelData.targetScore = Random.Range(45, 100);
        levelData.gridWidth = 6;//Random.Range(4, 10);
        levelData.gridHeight = 6;//Random.Range(4, 10);
        levelData.startX = -1;
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

    }


}
