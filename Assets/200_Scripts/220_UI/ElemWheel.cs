using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElemWheel : MonoBehaviour
{
    [SerializeField] private TimeManager tm;
    [SerializeField] private RectTransform selector;
    [SerializeField] private float rotSpeed = 1f;
    // Update is called once per frame
    void Update()
    {
        selector.rotation = Quaternion.Euler(0, 0, -tm.GetHour()*360/rotSpeed);
        //Debug.Log("Valeur de Z: " + selector.rotation.z);
    }
}
