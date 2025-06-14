using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManuelAnim : MonoBehaviour
{
    private IEventBus _eventBus;
    private Animator animator;
    private AnimatorStateInfo stateInfo;


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
        stateInfo = animator.GetCurrentAnimatorStateInfo(1);

    }


    private void OnMoveRequest(InputRequestEvent evt)
    {

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
        animator.SetTrigger("Walk");
        StartCoroutine(SmoothRotate(-90f, 0.5f));
    }

    private void TurnRight()
    {
        animator.SetTrigger("Walk");
        StartCoroutine(SmoothRotate(90f, 0.5f));
    }

    private void Turn180()
    {
        animator.SetTrigger("Walk");
        StartCoroutine(SmoothRotate(180f, 0.5f));
    }



    IEnumerator WaitForWalk()
    {
        animator.Play("Walk");
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("Done");
    }

    private IEnumerator SmoothRotate(float angle, float duration)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, angle, 0);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
            yield return null;
        }

        transform.rotation = endRotation; // tam hizalama
        animator.SetTrigger("Done");

    }

}
