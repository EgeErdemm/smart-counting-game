using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class TileEater 
{
    private ITileGridProvider _tileGridProvider;
    private ILevelDataProvider _levelDataProvider;
    private IEventBus _eventBus;
    

    public TileEater(ITileGridProvider tileGridProvider, ILevelDataProvider levelData)
    {
        _tileGridProvider = tileGridProvider;
        _levelDataProvider = levelData;
        _eventBus = EventBus.Instance;
        Subscribe(); // levelloader create this class "ITileGridProvider"

    }

    public void Subscribe()
    {
        _eventBus.Subscribe<TargetDestEvent>(OnTargetReached);
        Debug.Log("TextEated");
    }

    private void OnTargetReached(TargetDestEvent obj)
    {
        int x = Mathf.RoundToInt(obj.target.x);
        int z = Mathf.RoundToInt(obj.target.z);
        EatTileAndChangeText(x, z);
        //EatTileAndChangeText((int)obj.target.x, (int)obj.target.z);
        DelayedEatData(obj.target.x, obj.target.z);
    }

    public void EatTileAndChangeText(int x, int z)
    {
        //Debug.Log(x+ " " + z);
        GameObject tile = _tileGridProvider.GetTile(x, -z);
        ITextProvider textProvider = new TextProvider(tile);
        textProvider.SetText("0");
      }

    private void EatData(float x, float z)
    {
        int i =(int)( x + -z * _levelDataProvider.levelData.gridWidth);
        _levelDataProvider.levelData.grid[i] = 0;
    }

    public async void DelayedEatData(float x, float z)
    {
        await Task.Delay(200); // 0.2 saniye gecikme
        EatData(x, z);
    }

}
