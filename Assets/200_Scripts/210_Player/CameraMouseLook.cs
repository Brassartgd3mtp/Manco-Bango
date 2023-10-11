using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using UnityEngine.UI;

public class CameraMouseLook : MonoBehaviour
{
    [SerializeField] public int sensX = 2;
    [SerializeField] public int sensY = 2;

    public Slider slider1; // Référence au composant Slider
    public Slider slider2; // Référence au composant Slider

    public TextMeshProUGUI SensXtxt;
    public TextMeshProUGUI SensYtxt;

    
    public Transform orientation;

    private float rotationX;
    private float rotationY;

    private void Start()

    {
        sensX = 350;
        sensY = 350;

        slider1.value = sensX;
        slider2.value = sensY;

        slider1.onValueChanged.AddListener(UpdateValueX);
        slider2.onValueChanged.AddListener(UpdateValueY);
        Cursor.lockState = CursorLockMode.Locked; //Bloque la souris au centre de l'écran
        Cursor.visible = false; //Cache la souris
    }

    private void Update()
    {

        SensText(sensX, sensY);


        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        rotationY += mouseX;

        rotationX -= mouseY * sensY;
        rotationX = Mathf.Clamp(rotationX, -90.0f, 90.0f); //Limite le degré maximal de rotation vertical

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0); //Rotation verticale
        orientation.rotation = Quaternion.Euler(0, rotationY, 0); //Rotation horizontale
    }

    public void UpdateValueX(float newValue)
    {
                int intValue = Mathf.RoundToInt(newValue); // Convertir en un nombre entier

        sensX = (int)newValue;
        
    }

    public void UpdateValueY(float newValue)
    {
        int intValue = Mathf.RoundToInt(newValue); // Convertir en un nombre entier

        sensY = (int)newValue;

    }
    
    public void SensText(int _converterX, int _converterY)
    {
        _converterX = sensX;
        _converterY = sensY;
        _converterX /= 60;
        _converterY /= 60;
        SensXtxt.text = _converterX.ToString();
        SensYtxt.text = _converterY.ToString();
    }

}
