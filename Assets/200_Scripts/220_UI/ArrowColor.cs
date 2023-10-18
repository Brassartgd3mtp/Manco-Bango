using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowColor : MonoBehaviour
{
    private Image arrow;

    [SerializeField] private Barrel barrel;


    // Start is called before the first frame update
    void Start()
    {
        arrow = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (barrel.barrelStock.Count > 0) arrow.color = barrel.barrelStock[0];
        else arrow.color = Color.black;
    }
}
