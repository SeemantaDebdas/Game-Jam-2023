using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformState : PlayerBaseState
{
    Transform platform = null;
    readonly float speed = 5f;
    bool isMoving = false;
    public PlayerPlatformState(PlayerStatemachine SM, Transform platform) : base(SM)
    {
        this.platform = platform;
    }

    public override void Enter()
    {
        SM.transform.SetParent(platform);
        SM.InputReader.OnJumpEvent += InputReader_OnJumpEvent;
    }

    public override void Exit()
    {
        SM.transform.SetParent(null);
        SM.InputReader.OnJumpEvent -= InputReader_OnJumpEvent;
    }

    public override void Tick()
    {
        if(!IsOnPlatform(ref platform))
        {
            SM.SwitchState(new PlayerLocomotionState(SM));
            return;
        }

        Vector3 moveInput = new Vector3(SM.InputReader.MoveInput.x, 0, SM.InputReader.MoveInput.y).normalized;

        if (moveInput == Vector3.zero)
        {
            isMoving = false;
            SM.Anim.CrossFadeInFixedTime(IdleAnim, FixedTransitionDuration);
            return;
        }
        else if(moveInput != Vector3.zero && !isMoving)
        {
            isMoving = true;
            SM.Anim.CrossFadeInFixedTime(JogAnim, FixedTransitionDuration);
        }

        Vector3 targetDirection = RotateTowardsInput();

        //Move(speed * targetDirection);

        SM.Controller.Move(speed * Time.deltaTime * targetDirection);
    }
    private void InputReader_OnJumpEvent()
    {
        if (SM.ForceReceiver.IsGrounded())
            SM.SwitchState(new PlayerJumpState(SM));
    }
}
