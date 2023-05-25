using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    Controls controls;
    public Vector2 MoveInput { get; private set; }
    public event Action OnJumpEvent;

    public event Action OnJumpHoldPerformed;
    public event Action OnJumpHoldCancelled;

    private void Start()
    {
        controls = new();
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context){}

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        OnJumpEvent?.Invoke();
    }

    public void OnJumpHold(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnJumpHoldPerformed?.Invoke();
        if (context.canceled)
            OnJumpHoldCancelled?.Invoke();
    }
}
