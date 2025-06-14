using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    private IEventBus _eventBus;
    private Animator animator;
    private AnimatorStateInfo stateInfo;
    private bool rotateUpdate=false;

    private Quaternion targetRotation;
    private float rotationSpeed = 120f;
    private bool isFixingRotation = false;

    private void OnEnable()
    {
        _eventBus = EventBus.Instance;
        _eventBus.Subscribe<InputRequestEvent>(OnMoveRequest);
    }
    private void OnDisable()
    {
        _eventBus.UnSubscribe<InputRequestEvent>(OnMoveRequest);

    }

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);

    }

    // Update is called once per frame
    void Update()
    {
        if (stateInfo.IsName("Idle"))
        {
            rotateUpdate = false;
        }

        if (isFixingRotation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                transform.rotation = targetRotation;
                isFixingRotation = false;
            }
        }

    }


    private void OnMoveRequest(InputRequestEvent evt)
    {
        //if (!rotateUpdate)
        //    return;
        Action moveAction = evt.direction switch
        {
            RelativeDirection.Forward => () => StartCoroutine(WaitForWalk()),
            RelativeDirection.Left => TurnLeft,
            RelativeDirection.Right => TurnRight,
            RelativeDirection.Backward => Turn180,
            _ => () => Debug.LogWarning($"Unhandled direction: {evt.direction}")
        };

        moveAction();
    }

    private void TurnLeft()
    {
        Debug.Log("TurnLeft called");
        animator.SetTrigger("TurnLeft");

        if (stateInfo.IsName("TurnLeft") && stateInfo.normalizedTime > 1f && !rotateUpdate)
        {
            animator.SetTrigger("Done");
            rotateUpdate = true;
            Debug.Log("TurnLeft animation completed");
        }
    }

    private void TurnRight()
    {
        Debug.Log("TurnRight called");
        animator.SetTrigger("TurnRight");

        if (stateInfo.IsName("TurnRight") && stateInfo.normalizedTime > 1f && !rotateUpdate)
        {
            targetRotation = transform.rotation * Quaternion.Euler(0, -13.8f, 0);
            animator.SetTrigger("Done");
            isFixingRotation = true;
            rotateUpdate = true;
            Debug.Log("TurnRight animation completed");
        }
    }

    private void Turn180()
    {
        Debug.Log("Turn180 called");
        animator.SetTrigger("Turn180");

        if (stateInfo.IsName("Turn180") && stateInfo.normalizedTime > 1f && !rotateUpdate)
        {
            animator.SetTrigger("Done");
            rotateUpdate = true;
            Debug.Log("Turn180 animation completed");
        }
    }



    IEnumerator WaitForWalk()
    {
        animator.Play("Walk");
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("Done");
    }

}
