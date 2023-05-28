using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] Transform handGunTip;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float fireRate = 0.5f;

    bool canShoot = false;
    float fireRateCounter = 0f;

    InputReader inputReader = null;
    CinemachineImpulseSource impulseSource = null;

    private void Start()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
        inputReader = GetComponent<InputReader>();

        inputReader.OnAimPerformed += () => canShoot = true;
        inputReader.OnAimCancelled += () => canShoot = false;

        inputReader.OnAttackEvent += InputReader_OnAttackEvent;
    }

    private void OnDisable()
    {
        inputReader.OnAttackEvent -= InputReader_OnAttackEvent;
    }

    private void Update()
    {
        fireRateCounter += Time.deltaTime;
    }

    private void InputReader_OnAttackEvent()
    {
        if (!canShoot) return;
        if (fireRateCounter < fireRate) return;

        fireRateCounter = 0;

        Projectile projectileSpawn = Instantiate(projectilePrefab, handGunTip.position, handGunTip.rotation).GetComponent<Projectile>();

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));

        Vector3 projectileDirection;
        if(Physics.Raycast(ray, out RaycastHit hit, 10))
        {
            projectileDirection = (hit.point - handGunTip.position).normalized; 
        }
        else
        {
            Vector3 rayEndPosition = ray.GetPoint(10);
            projectileDirection = (rayEndPosition - handGunTip.position).normalized;
        }

        projectileSpawn.SetForce(projectileDirection);
        impulseSource.GenerateImpulse();

        Debug.LogError("STOP");
    }

}
