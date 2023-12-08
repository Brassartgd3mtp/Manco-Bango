using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class BarrelFader : MonoBehaviour
{
    public CanvasGroup canvaGroup;
    public float MaxDelay = 1;
    [Tooltip("Don't modify in runtime !")]
    public float Delay;
    [Range(1, 10)]
    public float FadeSpeed = 2;
    // Start is called before the first frame update
    void Start()
    {
        canvaGroup = GetComponent<CanvasGroup>();
        Delay = MaxDelay;
        if (FadeSpeed < 1) FadeSpeed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Delay <= 0 && canvaGroup.alpha >= 0)
        {
            Delay = 0;
            canvaGroup.alpha -= Time.deltaTime * FadeSpeed;
        }
        else
            Delay -= Time.deltaTime;
    }
}
