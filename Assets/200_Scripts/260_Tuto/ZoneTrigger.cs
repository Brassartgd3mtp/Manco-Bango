using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ZoneTrigger : MonoBehaviour
{
    public TextMeshProUGUI messageText; // Faites glisser votre objet TextMeshPro dans l'Inspector ici.
    public string interactionMessage = "Appuyez sur E pour interagir";
    public KeyCode interactionKey = KeyCode.E;
    private bool inTriggerZone = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inTriggerZone = true;
            DisplayMessage(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inTriggerZone = false;
            DisplayMessage(false);
        }
    }

    private void Update()
    {
        if (inTriggerZone && Input.GetKeyDown(interactionKey))
        {
            // Vous pouvez ajouter ici le code de ce que vous voulez faire lorsque le joueur appuie sur la touche.

            // Dans cet exemple, nous allons simplement faire disparaître le message.
            DisplayMessage(false);
        }
    }

    private void DisplayMessage(bool show)
    {
        if (messageText != null)
        {
            messageText.text = show ? interactionMessage : string.Empty;
            messageText.gameObject.SetActive(show);
        }
    }
}
