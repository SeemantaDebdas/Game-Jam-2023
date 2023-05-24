using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerBaseState
{
    const float rotationSmoothTime = 0.12f;
    float currentRotationVelocity;
    readonly float speed = 5f;
    public PlayerInAirState(PlayerStatemachine SM) : base(SM)
    {
    }

    public override void Enter()
    {
    }

    public override void Exit()
    { 
    }

    public override void Tick()
    {
        if (SM.ForceReceiver.IsGrounded())
        {
            SM.SwitchState(new PlayerLocomotionState(SM));
            return;
        }

        Vector3 moveInput = new Vector3(SM.InputReader.MoveInput.x, 0, SM.InputReader.MoveInput.y).normalized;

        if (moveInput == Vector3.zero)
        {
            Move();
            return;
        }

        float targetAngle = Mathf.Atan2(moveInput.x, moveInput.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;

        float rotation = Mathf.SmoothDampAngle(SM.transform.eulerAngles.y, targetAngle, ref currentRotationVelocity, rotationSmoothTime);
        SM.transform.rotation = Quaternion.Euler(0, rotation, 0);

        Vector3 targetDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

        Move(speed * targetDirection);
    }
}
