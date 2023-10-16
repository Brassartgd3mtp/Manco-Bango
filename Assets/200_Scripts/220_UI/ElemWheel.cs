using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElemWheel : MonoBehaviour
{
    [SerializeField] private TimeManager tm;
    [SerializeField] private RectTransform selector;
    public float rotSpeed = 1f;
    // Update is called once per frame
    void Update()
    {
        selector.rotation = Quaternion.Euler(0, 0, -tm.GetHour()*(rotSpeed*90000)/360);
        //Debug.Log("Valeur de Z: " + selector.rotation.z);
    }
}
