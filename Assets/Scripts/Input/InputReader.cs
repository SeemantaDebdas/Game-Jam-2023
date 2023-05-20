using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    Controls controls;
    public Vector2 MoveInput { get; private set; }
    public event Action OnJumpEvent;

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
}
