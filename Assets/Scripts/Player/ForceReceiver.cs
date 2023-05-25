using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ForceReceiver : MonoBehaviour
{
    public event Action<bool> OnIsGroundedChanged;

    float velocityY = 0;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float groundCheckDistance = 1.5f;
    [SerializeField] bool canGlide = false;

    public float JumpForce = 10f;
    public Vector3 Movement => Vector3.up * velocityY;
    public bool CanGlide => canGlide;

    float velocityDescentMultiplier = 3f;
    const float velocityDescentMultiplierDefault = 3f;
    const float velocityDescentMultiplierGlide = 0.5f;

    InputReader inputReader = null;

    bool wasGroundedLastFrame = true;

    private void Awake()
    {
        inputReader = GetComponent<InputReader>();

        if (!canGlide) return;
        inputReader.OnJumpHoldPerformed += () => velocityDescentMultiplier = velocityDescentMultiplierGlide;
        inputReader.OnJumpHoldCancelled += () => velocityDescentMultiplier = velocityDescentMultiplierDefault;
    }

    private void Start()
    {
        Reset();
    }

    private void Update()
    {
        bool isGrounded = IsGrounded();

        if (velocityY < 0f && isGrounded)
        {
            velocityY = -1f;
        }
        else if(velocityY > 0f && !isGrounded)
        {
            velocityY += Physics.gravity.y * 2f * Time.deltaTime;
        }
        else if(velocityY < 0f && !isGrounded)
        {
            velocityY += Physics.gravity.y * velocityDescentMultiplier * Time.deltaTime;
        }

        if(isGrounded != wasGroundedLastFrame)
        {
            OnIsGroundedChanged?.Invoke(isGrounded);
        }

        wasGroundedLastFrame = isGrounded;
    }


    public void Jump(float jumpForce) 
    {
        velocityY += jumpForce;
    }
     
    public void Reset()
    {
        velocityY = -1;
    }

    public bool IsGrounded()
    {
        if (Physics.Raycast(transform.position + transform.up * 0.1f, -transform.up, groundCheckDistance, groundLayer))
            return true;
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = IsGrounded() ? Color.green : Color.red;
        Gizmos.DrawLine(transform.position + transform.up * 0.1f, transform.position  + -transform.up * groundCheckDistance);
    }

}
