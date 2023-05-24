using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantObjects : MonoBehaviour
{
    void Start()
    {
        PersistantObjects[] persistantObjects = FindObjectsOfType<PersistantObjects>();
        foreach(PersistantObjects persistantObject in persistantObjects)
        {
            if (persistantObject != this)
                Destroy(persistantObject.gameObject);
        }

        DontDestroyOnLoad(this);   
    }
}
