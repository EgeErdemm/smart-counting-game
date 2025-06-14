using UnityEngine;

public class BoundaryChecker: IBorders
{
    private int boundaryWidth=5, boundaryHeight=5;

    public BoundaryChecker()
    {
        EventBus.Instance.Subscribe<GridSizeEvent>(OnGridSizeEvent);
    }


    private void OnGridSizeEvent(GridSizeEvent obj)
    {
        boundaryWidth = obj.Width;
        boundaryHeight = obj.Height;
        Debug.Log(obj.Width);
    }

    public bool IsOutSideBorder(Vector3 targetPos)
    {
        float rightBoundary = boundaryWidth;
        float bottomBoundary = -boundaryHeight;  

        return targetPos.x <= -1 ||
               targetPos.x > rightBoundary ||
               targetPos.z > 0 ||
               targetPos.z < bottomBoundary;
    }

}



