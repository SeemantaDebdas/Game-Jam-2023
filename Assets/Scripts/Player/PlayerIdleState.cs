using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStatemachine SM) : base(SM)
    {
    }

    public override void Enter()
    {
        SM.Anim.CrossFadeInFixedTime(IdleAnim, FixedTransitionDuration);
        SM.InputReader.OnJumpEvent += InputReader_OnJumpEvent;
        SM.InputReader.OnAimPerformed += InputReader_OnAimPerformed;
    }

    public override void Exit()
    {
        SM.InputReader.OnJumpEvent -= InputReader_OnJumpEvent;
        SM.InputReader.OnAimPerformed -= InputReader_OnAimPerformed;
    }

    public override void Tick()
    {
        Move();

        if (SM.InputReader.MoveInput != Vector2.zero)
            SM.SwitchState(new PlayerLocomotionState(SM));
    }

    private void InputReader_OnJumpEvent()
    {
        if (SM.ForceReceiver.IsGrounded())
            SM.SwitchState(new PlayerJumpState(SM));
    }
    private void InputReader_OnAimPerformed()
    {
        SM.SwitchState(new PlayerAimState(SM));
    }
}
