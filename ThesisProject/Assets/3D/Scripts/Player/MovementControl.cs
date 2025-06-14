using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControl : MonoBehaviour
{
    private IEventBus _eventBus;
    private bool isMoving;
    private IBorders _borders;

    private void OnEnable()
    {
        _eventBus = EventBus.Instance;
        _eventBus.Subscribe<InputRequestEvent>(OnMoveRequest);
    }
    private void OnDisable()
    {
        _eventBus.UnSubscribe<InputRequestEvent>(OnMoveRequest);
    }

    private void Awake()
    {
        _borders = new BoundaryChecker();
    }

    private void OnMoveRequest(InputRequestEvent evt)
    {
        if (isMoving)
            return;
            
        Vector3 dir = getWorldVector3(evt.direction);
        //Debug.Log(dir);
        StartCoroutine(Move(dir));

    }

    private Vector3 getWorldVector3(RelativeDirection dir)
    {
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        return dir switch
        {
            RelativeDirection.Forward => forward,
            RelativeDirection.Backward => -forward,
            RelativeDirection.Right => right,
            RelativeDirection.Left => -right,
            _ => Vector3.zero
        };

    }

    private IEnumerator Move(Vector3 dir)
    {
        isMoving = true;

        Vector3 start = transform.position;
        Vector3 target = start + dir.normalized; // 1 birim ileri
        if (_borders.IsOutSideBorder(target)) {
            isMoving = false;
            Debug.Log("out of borders");
            yield break;
        }
        _eventBus.Publish(new TargetDestEvent(target)); // send target pos info , listeners: tileEater
        float duration = 0.8f;
        float t = 0f;
        yield return new WaitForSeconds(0.2f); // animasyon gecisi bekle
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            transform.position = Vector3.Lerp(start, target, t);
            yield return null;
        }

        transform.position = target;
        isMoving = false;
    }


}
