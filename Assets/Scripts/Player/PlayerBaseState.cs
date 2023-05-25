using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStatemachine SM;

    protected const string JogAnimString = "Jog";
    protected const string PushAnimString = "Push";

    protected int IdleAnim = Animator.StringToHash("Idle");
    protected int JogAnim = Animator.StringToHash(JogAnimString);
    protected int JumpAnim = Animator.StringToHash("Jump");
    protected int PushAnim = Animator.StringToHash(PushAnimString);

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

        if (moveInput == Vector3.zero) return Vector3.zero;

        float targetAngle = Mathf.Atan2(moveInput.x, moveInput.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;

        float rotation = Mathf.SmoothDampAngle(SM.transform.eulerAngles.y, targetAngle, ref currentRotationVelocity, rotationSmoothTime);
        SM.transform.rotation = Quaternion.Euler(0, rotation, 0);

        Vector3 targetDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
        
        return targetDirection;
    }

    protected void PlayAnimation(string animString)
    {
        AnimatorClipInfo[] clips = SM.Anim.GetCurrentAnimatorClipInfo(0);
        if (SM.Anim.IsInTransition(0) || clips[0].clip.name == animString) return;

        int animHash = Animator.StringToHash(animString);

        SM.Anim.CrossFadeInFixedTime(animHash, FixedTransitionDuration);
    }

    protected bool IsOnPlatform(ref Transform platform)
    {
        Collider[] colliders = Physics.OverlapSphere(SM.transform.position, 1f);
        
        foreach(Collider collider in colliders)
        {
            if (collider.transform.CompareTag("ParentTrigger"))
            {
                platform = collider.transform;
                return true;
            }
        }

        return false;
    }

}
