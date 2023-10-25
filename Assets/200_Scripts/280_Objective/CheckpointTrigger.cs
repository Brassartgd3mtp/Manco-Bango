using UnityEngine;
using System.Collections;
using TMPro;

public class CheckpointTrigger : MonoBehaviour
{
    public TextMeshProUGUI triggerText;
    public float displayDuration = 3f; // Durée d'affichage du texte en secondes

    private bool isDisplaying = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Met à jour le dernier checkpoint du joueur en utilisant la position de ce checkpoint
            CheckpointManager.SetCheckpoint(transform.position, HealthManager.health);

            // Affichez un message de checkpoint atteint
            ShowTriggerText("Save...");
        }
    }

    private void Update()
    {
        if (isDisplaying)
        {
            displayDuration -= Time.deltaTime;

            if (displayDuration <= 0)
            {
                // Désactivez le texte lorsque la durée est écoulée
                triggerText.text = "";
                triggerText.enabled = false;
                isDisplaying = false;
            }
        }
    }

    private void ShowTriggerText(string text)
    {
        // Affichez le texte
        triggerText.text = text;
        triggerText.enabled = true;
        isDisplaying = true;
        displayDuration = 3f; // Réinitialisez la durée d'affichage
    }
}
