using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Reload : MonoBehaviour
{
    [SerializeField] private RectTransform selector;
    public BulletManager bulletManager;

    void Update()
    {
        if (Input.GetButtonDown("Reload"))
        {
            if (selector.rotation.eulerAngles.z <= 180 && selector.rotation.eulerAngles.z > 0) bulletManager.bulletColor = Color.blue;
            else /*if (selector.rotation.eulerAngles.z <= 0 && selector.rotation.eulerAngles.z > -180)*/ bulletManager.bulletColor = Color.red;
        }
    }
}
