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
    }

    public override void Exit()
    {
        
    }

    public override void Tick()
    {
        if (SM.InputReader.MoveInput != Vector2.zero)
            SM.SwitchState(new PlayerLocomotionState(SM));
    }
}
