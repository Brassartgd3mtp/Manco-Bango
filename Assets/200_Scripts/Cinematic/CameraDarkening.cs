using UnityEngine;

public class CameraDarkening : MonoBehaviour
{
    public Camera mainCamera;
    public float darkeningSpeed = 0.2f;
    public float lighteningSpeed = 0.2f;

    private bool isDarkening = false;
    private bool isLightening = false;

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    private void Update()
    {
        if (isDarkening)
        {
            mainCamera.backgroundColor = Color.Lerp(mainCamera.backgroundColor, Color.black, darkeningSpeed * Time.deltaTime);
        }
        else if (isLightening)
        {
            mainCamera.backgroundColor = Color.Lerp(mainCamera.backgroundColor, Color.clear, lighteningSpeed * Time.deltaTime);
        }
    }

    public void StartDarkening()
    {
        isDarkening = true;
        isLightening = false;
    }

    public void StartLightening()
    {
        isLightening = true;
        isDarkening = false;
    }
}