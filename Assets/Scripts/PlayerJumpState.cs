using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    float speed = 5f;
    Vector3 momentum;

    public PlayerJumpState(PlayerStatemachine SM) : base(SM){}

    public override void Enter()
    {
        SM.ForceReceiver.Jump(SM.ForceReceiver.JumpForce);

        momentum = SM.Controller.velocity;
        momentum.y = 0f;
        SM.Anim.CrossFadeInFixedTime(JumpAnim, FixedTransitionDuration);
    }

    public override void Exit()
    {
        
    }

    public override void Tick()
    {
        if (SM.ForceReceiver.Movement.y < 0f && SM.ForceReceiver.IsGrounded())
        {
            SM.SwitchState(new PlayerLocomotionState(SM));
            return;
        }

        Vector3 moveInput = new Vector3(SM.InputReader.MoveInput.x, 0, SM.InputReader.MoveInput.y).normalized;

        if (moveInput == Vector3.zero)
        {
            Move(momentum);
            return;
        }

        Vector3 targetDirection = RotateTowardsInput();
        Move(speed * targetDirection);
    }
}
