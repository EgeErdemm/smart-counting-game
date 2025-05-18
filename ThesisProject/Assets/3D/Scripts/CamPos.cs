using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPos : MonoBehaviour
{
    [SerializeField] private Transform camTransform;
    private IEventBus _eventbus;


    private void OnEnable()
    {
        _eventbus = EventBus.Instance;
        _eventbus.Subscribe<GridSizeEvent>(OnGridSize);
    }

    private void OnGridSize(GridSizeEvent obj)
    {
        setCamPos(obj.Width, obj.Height);
    }

    private void setCamPos(int width,int height)
    {
        float x= width*0.5f;
        float z = -height;
        Vector3 pos = new Vector3(x, 7, z);
        camTransform.position = pos;
        Debug.Log(pos);
        Debug.Log(x);
    }


}
