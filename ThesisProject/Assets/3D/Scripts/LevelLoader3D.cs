using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader3D : BaseLevelLoader
{
    private IEventBus _eventbus;

    [SerializeField] private GameObject bombTile;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Transform gridParent;
    [SerializeField] private float cellSize;

    private void OnEnable()
    {
        _eventbus = EventBus.Instance;
    }


    protected override GameObject BombTile => bombTile;

    protected override GameObject TilePrefab => tilePrefab;

    protected override Transform GridParent => gridParent;

    protected override float CellSize => cellSize;

    protected override RectTransform gridParentRectTransform => throw new System.NotImplementedException(); // this is for 2d class. ovveride it

    protected override void SetTilePosition(GameObject tile, int x, int y)
    {
        tile.transform.localPosition = new Vector3(x * CellSize, 0f, y * -CellSize);
    }

    protected override void SceneOrder()
    {
        base.SceneOrder();
        _eventbus.Publish(new GridSizeEvent(levelData.gridWidth, levelData.gridHeight));
        Debug.Log("event publish");
    }

}
