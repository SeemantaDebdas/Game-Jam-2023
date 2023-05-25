using DG.Tweening;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerRigController : MonoBehaviour
{
    [SerializeField] Rig playerRig;
    [SerializeField] RigBuilder rigBuilder;
    [SerializeField] MultiAimConstraint leftHandAim;
    [SerializeField] MultiAimConstraint rightHandAim;

    InputReader inputReader;
    ForceReceiver forceReceiver;

    private void Awake()
    {
        inputReader = GetComponent<InputReader>();
        forceReceiver = GetComponent<ForceReceiver>();

        inputReader.OnJumpHoldPerformed += () => RaiseArms();
        inputReader.OnJumpHoldCancelled += () => LowerArms();

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

    public void RaiseArms()
    {
        rigBuilder.enabled = true;
        rigBuilder.layers[0].active = true;

        DOVirtual.Float(0, 1, 0.5f, (val) => leftHandAim.weight = val);
        DOVirtual.Float(0, 1, 0.5f, (val) => rightHandAim.weight = val);
    }

    public void LowerArms()
    {
        if (leftHandAim.weight == 0) return;

        DOVirtual.Float(1, 0, 0.5f, (val) => leftHandAim.weight = val);
        DOVirtual.Float(1, 0, 0.5f, (val) => rightHandAim.weight = val);

        rigBuilder.enabled = false;
        rigBuilder.layers[0].active = false;
    }
}
