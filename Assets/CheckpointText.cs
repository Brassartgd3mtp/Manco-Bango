using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckpointText : MonoBehaviour
{
    public float displayDuration = 3f; // Durée d'affichage du texte en secondes

    private TextMeshProUGUI textComponent;
    private bool isDisplaying;

    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        textComponent.enabled = false; // Désactivez le texte au démarrage
    }

    public void ShowCheckpointText(string message)
    {
        if (!isDisplaying)
        {
            textComponent.text = message;
            textComponent.enabled = true;
            isDisplaying = true;
            StartCoroutine(HideTextAfterDelay());
        }
    }

    IEnumerator HideTextAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        textComponent.enabled = false;
        isDisplaying = false;
    }
}
