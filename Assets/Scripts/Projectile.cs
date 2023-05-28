using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileForce = 5f;

    Rigidbody rb;

    private void Awake()
    {
        Debug.Log("Called projectile");
        rb = GetComponent<Rigidbody>();
    }

    public void SetForce(Vector3 direction)
    {
        rb.velocity = direction * projectileForce;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    float timeSinceAwake = 0f;
    float destroyTime = 5f;
    private void Update()
    {
        timeSinceAwake += Time.deltaTime;

        if (timeSinceAwake > destroyTime)
            Destroy(gameObject);
    }
}
