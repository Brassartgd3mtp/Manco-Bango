using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public Color bulletColor;
    private Renderer instanceRenderer;

    // Start is called before the first frame update
    void Start()
    {
        instanceRenderer = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        instanceRenderer.material.color = bulletColor;
        if (bulletColor == Color.red)
        {

        }
    }
}
