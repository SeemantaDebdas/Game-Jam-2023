using System;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    Controls controls;
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }

    public event Action OnJumpEvent;

    public event Action OnAttackEvent;

    public event Action OnJumpHoldPerformed;
    public event Action OnJumpHoldCancelled;

    public event Action OnAimPerformed;
    public event Action OnAimCancelled;

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

    public void OnLook(InputAction.CallbackContext context)
    {
        LookInput = context.ReadValue<Vector2>();   
    }

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

    public void OnAim(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnAimPerformed?.Invoke();
        if (context.canceled)
            OnAimCancelled?.Invoke();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        OnAttackEvent?.Invoke();
    }
}
