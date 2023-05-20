using System.Collections;
using UnityEngine;

public class PlayerStatemachine : Statemachine
{
    public InputReader InputReader { get; private set; }
    public CharacterController Controller { get; private set; }
    public Animator Anim { get; private set; }
    public ForceReceiver ForceReceiver { get; private set; }


    private void Awake()
    {
        InputReader = GetComponent<InputReader>();
        Controller = GetComponent<CharacterController>();
        ForceReceiver = GetComponent<ForceReceiver>();

        if(Anim == null)
            Anim = GetComponent<Animator>();
    }

    private void Start()
    {
        SwitchState(new PlayerLocomotionState(this));
    }



}