using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDoor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ObjectInteraction.RedDoors.Add(gameObject);

    }

    private void OnDestroy()
    {
        ObjectInteraction.RedDoors.Remove(gameObject);

    }
}
