using System.Collections;
using UnityEngine;

public class PlayerStatemachine : Statemachine
{
    public InputReader InputReader;
    public CharacterController Controller;
    public Animator Anim;

    private void Awake()
    {
        InputReader = GetComponent<InputReader>();
        Controller = GetComponent<CharacterController>();

        if(Anim == null)
            Anim = GetComponent<Animator>();
    }

    private void Start()
    {
        SwitchState(new PlayerLocomotionState(this));
    }
}