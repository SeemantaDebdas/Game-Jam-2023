using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ForceReceiver : MonoBehaviour
{
    CharacterController controller;
    float velocityY = 0;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float groundCheckDistance = 1.5f;
    public float JumpForce = 10f;

    public Vector3 Movement => Vector3.up * velocityY;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (velocityY < 0f && IsGrounded())
        {
            velocityY = Physics.gravity.y * Time.deltaTime;
        }
        else if(velocityY > 0f && !IsGrounded())
        {
            velocityY += Physics.gravity.y * 2f * Time.deltaTime;
        }
        else if(velocityY < 0f && !IsGrounded())
        {
            velocityY += Physics.gravity.y * 3f * Time.deltaTime;
        }
    }



    public void Jump(float jumpForce)
    {
        velocityY += jumpForce;
    }

    public void Reset()
    {
        velocityY = 0;
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
