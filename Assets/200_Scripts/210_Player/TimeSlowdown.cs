using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeSlowdown : MonoBehaviour
{
    public float slowdownFactor = 0.05f; // Facteur de ralentissement du temps
    public float slowdownDuration = 2f; // Durée du ralentissement du temps
    public int maxSlowdowns = 3; // Nombre maximum de ralentissements autorisés

    public TextMeshProUGUI slowdownText; // Faites référence à l'objet Text (UI Text) dans l'inspecteur
    public CameraMouseLook cameraMouseLook; // Faites référence au Transform de votre joueur FPS
    public int modifySensibilityX = 2; // Sensibilité de la souris en mode ralenti
    public int modifySensibilityY = 2; // Sensibilité de la souris en mode ralenti

    private bool isSlowingDown = false;
    [SerializeField] private int currentSlowdowns = 0;
    private int originalMouseSensitivityX;
    private int originalMouseSensitivityY;
    public CanvasToggle canvasToggle;
    public InteractableItem interactableItem;

    private void Start()
    {
        originalMouseSensitivityX = cameraMouseLook.sensX;
        originalMouseSensitivityY = cameraMouseLook.sensY;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && !canvasToggle.isGamePaused && !interactableItem.buttonAlreadyPressed) 
        {
            if (!isSlowingDown && currentSlowdowns < maxSlowdowns)
            {
                StartCoroutine(SlowTime());
                currentSlowdowns++;
            }
            else if (currentSlowdowns <= maxSlowdowns)
            {
                ResetTime();
            }
        }

        // Mettez à jour le texte de l'UI avec le nombre de ralentissements restants
        slowdownText.text = $"Ralentissements restants : {(maxSlowdowns - currentSlowdowns)}";
    }

    public void AddSlowdowns(int amount)
    {
        currentSlowdowns -= amount;
        currentSlowdowns = Mathf.Clamp(currentSlowdowns, 0, maxSlowdowns);
    }

    private IEnumerator SlowTime()
    {
        isSlowingDown = true;

        // Ralentissez le temps en ajustant Time.timeScale (pour d'autres scripts)
        float originalTimeScale = Time.timeScale;
        Time.timeScale = slowdownFactor;

        // Ralentissez la sensibilité de la souris
        cameraMouseLook.sensX *= modifySensibilityX; // Réglez la sensibilité de la souris pour votre script FPS
        cameraMouseLook.sensY *= modifySensibilityY;

        yield return new WaitForSecondsRealtime(slowdownDuration);

        // Rétablissez les valeurs originales
        Time.timeScale = originalTimeScale;
        cameraMouseLook.sensX = originalMouseSensitivityX;
        cameraMouseLook.sensY = originalMouseSensitivityY;

        isSlowingDown = false;
    }

    private void ResetTime()
    {
        // Rétablissez les valeurs originales
        Time.timeScale = 1f;
        cameraMouseLook.sensX = originalMouseSensitivityX;
        cameraMouseLook.sensY = originalMouseSensitivityY;

        isSlowingDown = false;
    }
}
