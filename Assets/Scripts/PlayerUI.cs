using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] PlayerInteract playerInteract;
    [SerializeField] GameObject interactUI;
    [SerializeField] TextMeshProUGUI interactUIText;
    void Update()
    {
        Vector3 dirToCamera = (Camera.main.transform.position - transform.position).normalized;
        transform.forward = -dirToCamera;

        IInteractable interactable = playerInteract.GetInteractable();
        if (interactable != null)
        {
            ShowUI(interactable.GetInteractText());
        }
        else
        {
            HideUI();
        }
    }

    private void HideUI()
    {
        interactUI.SetActive(false);
    }

    private void ShowUI(string text)
    {
        interactUI.SetActive(true);
        interactUIText.text = text;
    }
}
