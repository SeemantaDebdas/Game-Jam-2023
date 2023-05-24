using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] float interactRange = 2f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            IInteractable interactable = GetInteractable();
            if (interactable != null)
                interactable.Interact();
        }
    }

    public IInteractable GetInteractable()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactRange);
        if (colliders.Length < 0) return null;

        foreach(var collider in colliders)
        {
            if (collider.TryGetComponent(out IInteractable interactable)) 
                return interactable;
        }

        return null;
    }
}
