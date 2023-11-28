using UnityEngine;

public class ActivateMouseCursor : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true; // Active le curseur de la souris
        Cursor.lockState = CursorLockMode.None; // Déverrouille le curseur
    }
}
