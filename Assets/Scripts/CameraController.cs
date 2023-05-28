using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    InputReader inputReader;
    [SerializeField] GameObject freeLookCam;
    [SerializeField] GameObject followCam;
    [SerializeField] Transform followCamTarget;
    private void Awake()
    {
        inputReader = GetComponent<InputReader>();

        inputReader.OnAimPerformed += () =>
        {
            freeLookCam.SetActive(false);
            followCam.SetActive(true);
        };

        inputReader.OnAimCancelled += () =>
        {
            freeLookCam.SetActive(true);
            followCam.SetActive(false);
        };
    }

    private void Update()
    {
        if (!followCam.activeSelf) return;

        Vector3 lookInput = inputReader.LookInput;

        followCamTarget.transform.eulerAngles += 10f * lookInput.x * Time.deltaTime * Vector3.up;
        followCamTarget.transform.eulerAngles += 5f * lookInput.y * Time.deltaTime * Vector3.right;
    }
}
