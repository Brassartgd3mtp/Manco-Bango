using UnityEngine;
using TMPro;

public class ZoneScript : MonoBehaviour
{
    public TextMeshProUGUI playerText; // Référence au composant TextMeshProUGUI du joueur
    public string zoneText = "Votre texte ici";
    public KeyCode pauseKey = KeyCode.P;

    private bool isPaused = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPaused)
        {
            // Démarrer la coroutine pour afficher le texte progressivement
            StartCoroutine(DisplayText(zoneText));
        }
    }

    private System.Collections.IEnumerator DisplayText(string textToDisplay)
    {
        Time.timeScale = 0f;
        isPaused = true;

        if (playerText != null)
        {
            playerText.text = ""; // Assurez-vous que le texte soit vide au début

            // Afficher le texte progressivement
            for (int i = 0; i <= textToDisplay.Length; i++)
            {
                playerText.text = textToDisplay.Substring(0, i);
                yield return new WaitForSecondsRealtime(0.05f); // Ajustez la vitesse d'affichage selon vos préférences
            }

            // Attendre avant de permettre au joueur de reprendre le jeu
            yield return new WaitForSecondsRealtime(1.0f);

            // Effacer le texte progressivement
            for (int i = textToDisplay.Length; i >= 0; i--)
            {
                playerText.text = textToDisplay.Substring(0, i);
                yield return new WaitForSecondsRealtime(0.05f); // Ajustez la vitesse de disparition selon vos préférences
            }

            // Réactiver le jeu immédiatement après l'affichage et avant la disparition du texte
            Time.timeScale = 1f;
            isPaused = false;
        }
        else
        {
            Debug.LogError("La référence du TextMeshProUGUI du joueur n'est pas définie.");
        }
    }

    private void Update()
    {
        // Vérifier si la touche de reprise est pressée
        if (isPaused && Input.GetKeyDown(pauseKey))
        {
            // Arrêter la coroutine si la touche de reprise est pressée
            StopAllCoroutines();

            // Réactiver le jeu immédiatement
            Time.timeScale = 1f;
            isPaused = false;

            // Effacer le texte immédiatement
            playerText.text = "";
        }
    }
}
