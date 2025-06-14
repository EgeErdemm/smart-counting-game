using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class LevelLoader : BaseLevelLoader
{
    [SerializeField] private GameObject PlayerTilePrefab;// oyunun basinda yarat
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] public Transform gridParent;
    [SerializeField] private float cellSize = 110f;
    [SerializeField] private RectTransform GridParentRectTransform;
    [SerializeField] private GameObject BombUItutorialPanel;
    public string levelCount;
    [SerializeField] private GameObject bombTile;


    public static LevelLoader Instance { get; private set; }
    protected override GameObject BombTile => bombTile;
    protected override GameObject TilePrefab => tilePrefab;
    public override Transform GridParent => GridParentRectTransform;
    protected override float CellSize => cellSize;
    public override RectTransform gridParentRectTransform => GridParentRectTransform;

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

    protected override void LoadLevel()
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

    protected override void SceneOrder()
    {
        gridParentRectTransform.anchoredPosition = new Vector3(-(gridWidth - 1) * cellSize * 0.5f, (gridHeight - 1) * cellSize * 0.5f, 0f);

        player = Instantiate(PlayerTilePrefab, gridParent);
        int PlayerX = levelData.startX;
        float startPosX = PlayerX * cellSize;
        int PlayerY = levelData.startY;
        float startPosY = PlayerY * cellSize;
        player.GetComponent<RectTransform>().anchoredPosition = new Vector2(startPosX, startPosY);

    }

    protected override void SetTilePosition(GameObject tile, int x, int y)
    {
        throw new System.NotImplementedException();
    }
}
