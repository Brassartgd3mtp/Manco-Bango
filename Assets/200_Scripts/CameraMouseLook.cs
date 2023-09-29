using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseLook : MonoBehaviour
{
    [SerializeField] private float sensitivity = 2.0f;
    [SerializeField] private Transform player;

    private float rotationX = 0.0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //Bloque la souris au centre de l'écran
        Cursor.visible = false; //Cache la souris
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        rotationX -= mouseY * sensitivity;
        rotationX = Mathf.Clamp(rotationX, -90.0f, 90.0f); //Limite le degré maximal de rotation vertical

        transform.localRotation = Quaternion.Euler(rotationX, 0, 0); //Rotation verticale
        player.transform.Rotate(Vector3.up * mouseX * sensitivity); //Rotation horizontale
    }
}
