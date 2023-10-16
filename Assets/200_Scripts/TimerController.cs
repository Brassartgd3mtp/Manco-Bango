using System.Collections;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    private bool isTimerRunning = false;
    private float timer = 0.0f;

    private void Start()
    {
        // Désactivez le timer au démarrage
        isTimerRunning = false;
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            // Incrémente le timer
            timer += Time.deltaTime;
            //Debug.Log("Temps écoulé : " + timer.ToString("F2")); // Affiche le temps écoulé

            // Ici, vous pouvez ajouter d'autres actions à effectuer pendant le timer
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StartZone"))
        {
            // Le joueur entre dans la zone de départ, démarre le timer
            isTimerRunning = true;
        }
        else if (other.CompareTag("StopZone"))
        {
            // Le joueur entre dans la zone d'arrêt, arrête le timer
            isTimerRunning = false;
            //Debug.Log("Temps total : " + timer.ToString("F2")); // Affiche le temps total
        }
    }
    public bool IsRunning()
    {
        return isTimerRunning;
    }
    public float GetTimer()
    {
        return timer;
    }
}
