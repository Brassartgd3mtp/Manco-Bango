using UnityEngine;

public class CameraMouseLook : MonoBehaviour
{
    [SerializeField] public static int sensX = 2;
    [SerializeField] public static int sensY = 2;
    
    public Transform orientation;

    private float rotationX;
    private float rotationY;

#if !UNITY_EDITOR
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
#endif

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        rotationY += mouseX;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90.0f, 90.0f); //Limite le degré maximal de rotation vertical

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0); //Rotation verticale
        orientation.rotation = Quaternion.Euler(0, rotationY, 0); //Rotation horizontale
    }
}
