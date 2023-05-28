using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotionState : PlayerBaseState
{
    readonly float speed = 5f;

    public PlayerLocomotionState(PlayerStatemachine SM) : base(SM) { }

    public override void Enter()
    {
        SM.Anim.CrossFadeInFixedTime(JogAnim, FixedTransitionDuration);

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
        Transform platform = null;
        if (IsOnPlatform(ref platform))
        {
            Debug.Log("Entering");
            SM.SwitchState(new PlayerPlatformState(SM, platform));
            return;
        }

        Vector3 moveInput = new Vector3(SM.InputReader.MoveInput.x, 0, SM.InputReader.MoveInput.y).normalized;

        if (moveInput == Vector3.zero)
        {
            SM.SwitchState(new PlayerIdleState(SM));
            return;
        }

        Vector3 targetDirection = RotateTowardsInput();

        Move(speed * targetDirection);
    }

    private void InputReader_OnJumpEvent()
    {
        if(SM.ForceReceiver.IsGrounded())
            SM.SwitchState(new PlayerJumpState(SM));
    }
    private void InputReader_OnAimPerformed()
    {
        SM.SwitchState(new PlayerAimState(SM)); 
    }
}
