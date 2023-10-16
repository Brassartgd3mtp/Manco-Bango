using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using UnityEngine.UI;

public class CameraMouseLook : MonoBehaviour
{
    [SerializeField] public int sensX = 2;
    [SerializeField] public int sensY = 2;
    
    public Transform orientation;

    private float rotationX;
    private float rotationY;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //Bloque la souris au centre de l'écran
        Cursor.visible = false; //Cache la souris
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        rotationY += mouseX;

        rotationX -= mouseY * sensY;
        rotationX = Mathf.Clamp(rotationX, -90.0f, 90.0f); //Limite le degré maximal de rotation vertical

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0); //Rotation verticale
        orientation.rotation = Quaternion.Euler(0, rotationY, 0); //Rotation horizontale
    }
}
