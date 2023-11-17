using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    // Référence à la caméra à suivre (peut être définie dans l'inspecteur Unity)
    public Camera targetCamera;

    void Update()
    {
        // Assurez-vous qu'une caméra est définie
        if (targetCamera == null)
        {
            Debug.LogError("La caméra cible n'est pas définie. Veuillez assigner une caméra dans l'inspecteur Unity.");
            return;
        }

        // Faites en sorte que l'objet regarde toujours vers la caméra
        transform.LookAt(transform.position + targetCamera.transform.rotation * Vector3.forward,
            targetCamera.transform.rotation * Vector3.up);
    }
}
