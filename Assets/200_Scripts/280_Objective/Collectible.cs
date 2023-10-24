using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractableItem : MonoBehaviour
{
    public Canvas interactionCanvas;
    public TextMeshProUGUI interactionText;
    public bool buttonAlreadyPressed = false;
    private bool isInRange = false;
    [SerializeField] private GameObject collectible;

    void Start()
    {
        interactionCanvas.enabled = false;
        interactionText.text = "";
    }

    void Update()
    {
        if (isInRange)
        {
            interactionText.text = "Press E to interact";
            if (Input.GetKeyDown(KeyCode.E))
            {
                ToggleCanvas();
                if (!buttonAlreadyPressed)
                {
                    Time.timeScale = 0f;
                    buttonAlreadyPressed = true;
                }
                else
                {
                    Time.timeScale = 1f;
                    buttonAlreadyPressed = false;
                    Destroy(collectible.gameObject);
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            interactionText.text = ""; // Effacez le texte lorsque vous n'êtes plus à proximité de l'objet.
            interactionCanvas.enabled = false;
        }
    }

    void ToggleCanvas()
    {
        interactionText.text = "";
        if (interactionCanvas.enabled)
        {
            interactionCanvas.enabled = false;
        }
        else
        {
            interactionCanvas.enabled = true;
        }
    }
}
