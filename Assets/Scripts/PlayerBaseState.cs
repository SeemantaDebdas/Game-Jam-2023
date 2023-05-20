using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStatemachine SM;

    protected int IdleAnim = Animator.StringToHash("Idle");
    protected int JogAnim = Animator.StringToHash("Jog");
    protected int JumpAnim = Animator.StringToHash("Jump");

    protected float FixedTransitionDuration = 0.1f;

    public PlayerBaseState(PlayerStatemachine SM)
    {
        this.SM = SM;
    }
    protected void Move(Vector3 motion)
    {
        Vector3 direction = motion + SM.ForceReceiver.Movement;
        SM.Controller.Move(Time.deltaTime * direction);
    }

    protected void Move()
    {
        Move(Vector3.zero);
    }


    const float rotationSmoothTime = 0.12f;
    float currentRotationVelocity;
    protected Vector3 RotateTowardsInput()
    {
        Vector3 moveInput = new Vector3(SM.InputReader.MoveInput.x, 0, SM.InputReader.MoveInput.y).normalized;

        float targetAngle = Mathf.Atan2(moveInput.x, moveInput.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;

        float rotation = Mathf.SmoothDampAngle(SM.transform.eulerAngles.y, targetAngle, ref currentRotationVelocity, rotationSmoothTime);
        SM.transform.rotation = Quaternion.Euler(0, rotation, 0);

        Vector3 targetDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
        
        return targetDirection;
    }

}
