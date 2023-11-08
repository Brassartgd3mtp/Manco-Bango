using UnityEngine;
using System.Collections;
using TMPro;

public class CheckpointTrigger : MonoBehaviour
{
    public TextMeshProUGUI triggerText;
    public float displayDuration = 3f; // Dur�e d'affichage du texte en secondes
    public HealthManager healthManager;
    CheckpointManager checkpointManager;
    private bool isDisplaying = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckpointManager checkpointManager = FindAnyObjectByType<CheckpointManager>();
            // Met � jour le dernier checkpoint du joueur en utilisant la position de ce checkpoint
            checkpointManager.SetCheckpoint(transform.position, healthManager.health);

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
                // D�sactivez le texte lorsque la dur�e est �coul�e
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
        displayDuration = 3f; // R�initialisez la dur�e d'affichage
    }
}
