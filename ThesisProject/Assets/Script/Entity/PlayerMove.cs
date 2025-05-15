using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    private LevelLoader levelLoader;

    private RectTransform PlayerTileTransform;
    [SerializeField] private Transform gridParent;
    [SerializeField] private float cellSize = 110f;

    private Tween moveTween;

    private int curIndex;


    private void OnEnable()
    {
        PlayerMoveEvent.OnGetPlayer += StartPlayerSpawn;

    }


    private void Start()
    {
        levelLoader = LevelLoader.Instance;
        StartCoroutine(getPlayer());

 

    }

    private void StartPlayerSpawn()
    {
        StartCoroutine(getPlayer());
    }

    IEnumerator getPlayer()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerTileTransform = levelLoader.player.GetComponent<RectTransform>();
        if (PlayerTileTransform == null)
        {
            Debug.LogError("NULL PLAYER RECT");
        }

        EventBus.Instance.Publish(new RequestStartTimerEvent());
        //TimerEvents.RequestStartTimer();
        if (levelLoader.levelData.isBlind)
        {
            CurrentIndex();
            PlayerMoveEvent.getIndex(curIndex);
        }
    }


    public void GoRight()
    {
        if (moveTween != null && moveTween.IsActive() && moveTween.IsPlaying())
            return;

        float RightBorder = ((levelLoader.levelData.gridWidth - 1) * cellSize)-1f;
        if (PlayerTileTransform.anchoredPosition.x >= RightBorder)
            return;

        moveTween = PlayerTileTransform.DOAnchorPosX(PlayerTileTransform.anchoredPosition.x + cellSize, 1f).OnComplete(() =>
         {
             moveTween = null;

             CurrentIndex();// After movement find the coordinat and send to event
             PlayerMoveEvent.PlayerMoved(curIndex);
         });
    }

    public void GoLeft()
    {
        if (moveTween != null && moveTween.IsActive() && moveTween.IsPlaying())
            return;

        if (PlayerTileTransform.anchoredPosition.x <= 5f)
            return;

        moveTween = PlayerTileTransform.DOAnchorPosX(PlayerTileTransform.anchoredPosition.x - cellSize, 1f).OnComplete(() =>
        {
            moveTween = null;

            CurrentIndex();// After movement find the coordinat and send to event
            PlayerMoveEvent.PlayerMoved(curIndex);

        });


    }

    public void GoUp()
    {
        if (moveTween != null && moveTween.IsActive() && moveTween.IsPlaying())
            return;

        if (PlayerTileTransform.anchoredPosition.y == 0f)
            return;
        moveTween = PlayerTileTransform.DOAnchorPosY(PlayerTileTransform.anchoredPosition.y + cellSize, 1f).OnComplete(() =>
        {
            moveTween = null;

            CurrentIndex();// After movement find the coordinat and send to event
            PlayerMoveEvent.PlayerMoved(curIndex);

        });
    }

    public void GoDown()
    {
        if (moveTween != null && moveTween.IsActive() && moveTween.IsPlaying())
            return;

        float BottomBorder = ((levelLoader.levelData.gridHeight - 1) * -cellSize) + 1;
        if (PlayerTileTransform.anchoredPosition.y <= BottomBorder)
            return;

        if (PlayerTileTransform.anchoredPosition.x <= -5f)
            return;

        moveTween = PlayerTileTransform.DOAnchorPosY(PlayerTileTransform.anchoredPosition.y - cellSize, 1f).OnComplete(() =>
        {
            moveTween = null;

            CurrentIndex();// After movement find the coordinat and send to event
            PlayerMoveEvent.PlayerMoved(curIndex);

        });
    }

    private void CurrentIndex()
    {
        int x = Mathf.RoundToInt(PlayerTileTransform.anchoredPosition.x / cellSize);
        int y = Mathf.Abs(Mathf.RoundToInt(PlayerTileTransform.anchoredPosition.y / cellSize));

        curIndex = (levelLoader.levelData.gridWidth * y) + x;

    }




}
