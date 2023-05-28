using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerRigController : MonoBehaviour
{
    [SerializeField] Rig playerRig;
    [SerializeField] RigBuilder rigBuilder;
    [SerializeField] TwoBoneIKConstraint leftHandAim;
    [SerializeField] TwoBoneIKConstraint rightHandAim;
    [SerializeField] Transform rightHandTarget;
    [SerializeField] Transform rightHandTargetGlidePreset;
    [SerializeField] Transform rightHandTargetAimPreset;
    [SerializeField] GameObject handGun;

    InputReader inputReader;
    ForceReceiver forceReceiver;

    private void Awake()
    {
        inputReader = GetComponent<InputReader>();
        forceReceiver = GetComponent<ForceReceiver>();

        inputReader.OnJumpHoldPerformed += () => 
        {
            rightHandTarget.position = rightHandTargetGlidePreset.position;
            rightHandTarget.rotation = rightHandTargetGlidePreset.rotation;
            RaiseArms();
        };
        inputReader.OnJumpHoldCancelled += () => LowerArms();

        inputReader.OnAimPerformed += () =>
        {
            rightHandTarget.rotation = rightHandTargetAimPreset.rotation;
            RaiseRightArm();
        };
        
        inputReader.OnAimCancelled += () => LowerArms();


        forceReceiver.OnIsGroundedChanged += (condition) =>
        {
            if (condition)
                LowerArms();
        };
    }


    private void Start()
    {
        LowerArms();
    }

    private void Update()
    {
        if (!handGun.activeSelf) return;


        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        Vector3 targetPosition;
        if (Physics.Raycast(ray, out RaycastHit hit, 10))
        {
            targetPosition = hit.point;
        }
        else
        {
            Vector3 rayEndPosition = ray.GetPoint(10);
            targetPosition = rayEndPosition;
        }

        Vector3 dirToTarget = (targetPosition - handGun.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(dirToTarget, handGun.transform.up);

        handGun.transform.rotation = lookRotation;


        rightHandTarget.position = targetPosition; 
    }

    public void RaiseArms()
    {
        Debug.Log("Raise arms called");

        rigBuilder.enabled = true;
        rigBuilder.layers[0].active = true;

        DOVirtual.Float(0, 1, 0.5f, (val) => leftHandAim.weight = val);
        DOVirtual.Float(0, 1, 0.5f, (val) => rightHandAim.weight = val);
    }

    public void LowerArms()
    {
        if (rightHandAim.weight == 0) return;

        DOVirtual.Float(1, 0, 0.5f, (val) => leftHandAim.weight = val);
        DOVirtual.Float(1, 0, 0.5f, (val) => rightHandAim.weight = val);

        rigBuilder.enabled = false;
        rigBuilder.layers[0].active = false;

        if (handGun)
            handGun.SetActive(false);
    }
    private void RaiseRightArm()
    { 
        rigBuilder.enabled = true;
        rigBuilder.layers[0].active = true;
        DOVirtual.Float(0, 1, 0.2f, (val) => rightHandAim.weight = val);
        handGun.SetActive(true);
    }
}
