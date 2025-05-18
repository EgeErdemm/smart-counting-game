using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private GameObject PlayerTilePrefab;// oyunun basinda yarat
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] public Transform gridParent;
    [SerializeField] private float cellSize = 110f;
    [SerializeField] private RectTransform gridParentRectTransform;
    [SerializeField] private GameObject BombUItutorialPanel;

    private int gridWidth;
    private int gridHeight;

    public static LevelLoader Instance { get; private set; }

    public LevelData levelData; // herkesin ulaşacağı verimiz
    [HideInInspector] public GameObject player;
    public GameObject[,] tileGrid;

    public string levelCount;

    [SerializeField] private GameObject BombTile;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Sahnedeki fazlalığı yok et
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadLevel(string levelName)
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

         
                GameObject tile = Instantiate(tilePrefab, gridParent);
                tile.GetComponent<RectTransform>().anchoredPosition = new Vector2(x * cellSize, -y * cellSize);

                TextMeshProUGUI text = tile.GetComponentInChildren<TextMeshProUGUI>();
                text.text = value.ToString();

                if(levelData.isBlind == true)
                {
                    isBlack(tile);
                }

                tileGrid[y, x] = tile;
                if (value == -1)
                {
                    GameObject bomb = Instantiate(BombTile, gridParent);
                    bomb.GetComponent<RectTransform>().anchoredPosition = new Vector2(x * cellSize, -y * cellSize);
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
        levelCount = PlayerPrefs.GetString("levelCount", "0");

        string level = "Level" + levelCount;
        LoadLevel(level);
        SceneOrder();
        Debug.Log(level);
        if (level == "Level10")
        {
            BombUItutorialPanel.transform.DOScale(new Vector3(1f, 1f, 1f), 1f);
        }
    }

    private void SceneOrder()
    {
        gridParentRectTransform.anchoredPosition = new Vector3(-(gridWidth - 1) * cellSize * 0.5f, (gridHeight - 1) * cellSize * 0.5f, 0f);

        player = Instantiate(PlayerTilePrefab, gridParent);
        int PlayerX = levelData.startX;
        float startPosX = PlayerX * cellSize;
        int PlayerY = levelData.startY;
        float startPosY = PlayerY * cellSize;
        player.GetComponent<RectTransform>().anchoredPosition = new Vector2(startPosX, startPosY);

    }


    private void MakeLevelData()
    {
        Debug.Log("Make data started");
        levelData = new LevelData();
        levelData.targetScore = Random.Range(45, 100);
        levelData.gridWidth= Random.Range(4, 10);
        levelData.gridHeight= Random.Range(4, 10);
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
