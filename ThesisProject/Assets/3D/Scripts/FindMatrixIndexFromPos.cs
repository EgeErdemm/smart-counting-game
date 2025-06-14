using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindMatrixIndexFromPos : IFindMatrixIndex
{
    private int width;
    public float x, z;
    public float i;

    public FindMatrixIndexFromPos()
    {
        EventBus.Instance.Subscribe<GridSizeEvent>(OnGridSizeEvent);
    }

    private void OnGridSizeEvent(GridSizeEvent obj)
    {
        width = obj.Width;
        Debug.Log("FindmatrixIndex worked: " + width);
    }

    public int FindIndexFromPos(Vector3 pos)
    {
        x = pos.x;
        z = pos.z;
        i = x + width * z;
        return (int)i;
    }
}
