using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStatemachine SM;

    protected int IdleAnim = Animator.StringToHash("Idle");
    protected int JogAnim = Animator.StringToHash("Jog");

    protected float FixedTransitionDuration = 0.1f;

    public PlayerBaseState(PlayerStatemachine SM)
    {
        this.SM = SM;
    }
}
