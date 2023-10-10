using UnityEngine;
using TMPro;

public class ResultDisplay : MonoBehaviour
{
    public TimerController timerController; // Assurez-vous de faire référence au script TimerController
    private TextMeshProUGUI textMeshPro;

    private void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        textMeshPro.enabled = false; // Masquez le texte du résultat au démarrage
    }

    private void Update()
    {
        if (timerController != null)
        {
            if (!timerController.IsRunning())
            {
                // Affichez le résultat si le timer n'est pas en cours d'exécution
                textMeshPro.enabled = true;
                textMeshPro.text = "Résultat : " + timerController.GetTimer().ToString("F2");
            }
            else
            {
                // Masquez le résultat si le timer est en cours d'exécution
                textMeshPro.enabled = false;
            }
        }
    }
}
