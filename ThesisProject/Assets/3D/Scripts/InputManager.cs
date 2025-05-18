using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private IEventBus _eventBus;
    private bool isInputLocked = false;

    [SerializeField] private float inputCooldown = 1f; // saniye cinsinden

    private void OnEnable()
    {
        _eventBus = EventBus.Instance;
    }

    void Update()
    {
        if (isInputLocked) return;

        if (Input.GetKeyDown(KeyCode.W))
            SendInput(RelativeDirection.Forward);
        else if (Input.GetKeyDown(KeyCode.S))
            SendInput(RelativeDirection.Backward);
        else if (Input.GetKeyDown(KeyCode.A))
            SendInput(RelativeDirection.Left);
        else if (Input.GetKeyDown(KeyCode.D))
            SendInput(RelativeDirection.Right);
    }

    private void SendInput(RelativeDirection direction)
    {
        isInputLocked = true;
        _eventBus.Publish(new InputRequestEvent(direction));
        StartCoroutine(InputCooldownCoroutine());
    }

    private IEnumerator InputCooldownCoroutine()
    {
        yield return new WaitForSeconds(inputCooldown);
        isInputLocked = false;
    }
}
