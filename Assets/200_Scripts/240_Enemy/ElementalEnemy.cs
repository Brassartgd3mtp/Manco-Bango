using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.DebugUI;

public class ElementalEnnemy : MonoBehaviour
{
    [SerializeField] private List<Target> targets;
    // Start is called before the first frame update
    void Start()
    {
        Target[] setupArray = gameObject.GetComponentsInChildren<Target>();

        foreach(Target target in setupArray)
        {
            targets.Add(target);
        }

        setupArray = new Target[0];
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] == null)
                targets.RemoveAt(i);
        }

        if (targets.Count <= 0)
        {
            Destroy(gameObject);
        }
    }
}
