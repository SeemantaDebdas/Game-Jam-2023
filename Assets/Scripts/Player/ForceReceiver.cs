using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ForceReceiver : MonoBehaviour
{
    float velocityY = 0;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask pushableLayer;
    [SerializeField] float groundCheckDistance = 1.5f;
    [SerializeField] float pushForce = 2f;
    public float JumpForce = 10f;
    public Vector3 Movement => Vector3.up * velocityY;

    private void Start()
    {
        velocityY = -1;
        Reset();
    }

    private void Update()
    {
        if (velocityY < 0f && IsGrounded())
        {
            velocityY = -1f;
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

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!IsGrounded()) return;
        if (!hit.transform.CompareTag("Pushable"))
        { 
            return;
        }

        Debug.Log(hit.transform.name);

        Rigidbody rb = hit.collider.attachedRigidbody;
        if (rb != null && !rb.isKinematic)
        {
            rb.velocity = hit.moveDirection * pushForce;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = IsGrounded() ? Color.green : Color.red;
        Gizmos.DrawLine(transform.position + transform.up * 0.1f, transform.position  + -transform.up * groundCheckDistance);

    }

}