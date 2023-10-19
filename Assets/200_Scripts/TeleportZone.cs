using UnityEngine;

public class TeleportZone : MonoBehaviour
{
    public Transform destination; // Référence à la zone de destination
    private TimerController timerController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assurez-vous que le joueur entre dans la zone
        {
            // Téléportez le joueur à la position de la zone de destination
            other.transform.position = destination.position;
            timerController.timer = 0.0f;
        }
    }
}
