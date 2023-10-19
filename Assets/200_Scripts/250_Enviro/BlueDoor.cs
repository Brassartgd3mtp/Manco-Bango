using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueDoor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ObjectInteraction.BlueDoors.Add(gameObject);
    }
    private void OnDestroy()
    {
        ObjectInteraction.BlueDoors.Remove(gameObject);

    }

}
