using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimState : PlayerBaseState
{
    bool isMoving = false;
    float speed = 3f;

    public PlayerAimState(PlayerStatemachine SM) : base(SM)
    {      
    }

    public override void Enter()
    {
        SM.InputReader.OnAimCancelled += InputReader_OnAimCancelled;
    }

    public override void Exit()
    {
        Debug.Log("Exitting Player Aim State");
        SM.InputReader.OnAimCancelled -= InputReader_OnAimCancelled;
    }


    const float rotationSmoothTime = 0.12f;
    float currentRotationVelocity;
    public override void Tick()
    {
        Vector3 moveInput = new Vector3(SM.InputReader.MoveInput.x, 0, SM.InputReader.MoveInput.y).normalized;

        float targetAngle = Camera.main.transform.eulerAngles.y;
        if(Mathf.Abs(targetAngle - SM.transform.eulerAngles.y) > 10f)
        {
            float rotation = Mathf.SmoothDampAngle(SM.transform.eulerAngles.y, targetAngle, ref currentRotationVelocity, rotationSmoothTime);
            SM.transform.rotation = Quaternion.Euler(0, rotation, 0);
        }


        if (moveInput == Vector3.zero)
        {
            isMoving = false;
            SM.Anim.CrossFadeInFixedTime(IdleAnim, FixedTransitionDuration);
            return;
        }
        else if (moveInput != Vector3.zero && !isMoving)
        {
            isMoving = true;
            SM.Anim.CrossFadeInFixedTime(JogAnim, FixedTransitionDuration);
        }

        Vector3 moveDirection = Camera.main.transform.forward * moveInput.z;
        moveDirection += Camera.main.transform.right * moveInput.x;

        Move(speed * moveDirection);
    }

    private void InputReader_OnAimCancelled()
    {
        SM.SwitchState(new PlayerLocomotionState(SM));
    }

}
