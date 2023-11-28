using UnityEngine;
using TMPro;

public class TimerDisplay : MonoBehaviour
{
    public TimerController timerController; // Assurez-vous de faire référence au script TimerController
    private TextMeshProUGUI textMeshPro;

    private void Start()
    {
        // Obtenez la référence au composant TextMeshProUGUI
        textMeshPro = GetComponent<TextMeshProUGUI>();

        // Masquez le texte du timer au démarrage
        textMeshPro.enabled = false;
    }

    private void Update()
    {
        if (timerController != null)
        {
            // Vérifiez si le timer est en cours d'exécution
            if (timerController.IsRunning())
            {
                // Affichez le texte avec le temps écoulé du TimerController
                textMeshPro.enabled = true;
                textMeshPro.text = "Temps : " + timerController.GetTimer().ToString("F2");
            }
            else
            {
                // Masquez le texte si le timer n'est pas en cours d'exécution
                textMeshPro.enabled = false;
            }
        }
    }
}
