using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public float countdownDuration = 60.0f; // Durée du compte à rebours en secondes
    public TextMeshProUGUI countdownText; // Référence au composant TextMesh Pro de l'UI
    private float timeRemaining;
    private bool isPlayerInsideZone = false;

    private void Start()
    {
        // Désactivez le composant TextMesh Pro au début
        countdownText.enabled = false;
        timeRemaining = countdownDuration;
    }

    private void Update()
    {
        // Vérifiez si le joueur est dans la zone
        if (isPlayerInsideZone)
        {
            if (timeRemaining > 0)
            {
                // Décrémentez le temps restant
                timeRemaining -= Time.deltaTime;

                // Activez le composant TextMesh Pro s'il ne l'est pas déjà
                countdownText.enabled = true;

                // Mettez à jour le composant TextMesh Pro avec le temps restant formaté
                UpdateCountdownText();
            }
            else
            {
                // Le compte à rebours est terminé, effectuez l'action souhaitée ici.
                Debug.Log("Compte à rebours terminé, faites quelque chose !");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Le joueur est entré dans la zone
            isPlayerInsideZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Le joueur est sorti de la zone
            isPlayerInsideZone = false;

            // Désactivez le composant TextMesh Pro
            countdownText.enabled = false;
        }
    }

    void UpdateCountdownText()
    {
        // Formattez le temps restant en minutes et secondes
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        // Mettez à jour le texte de l'UI
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
