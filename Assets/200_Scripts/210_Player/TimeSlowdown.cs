using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeSlowdown : MonoBehaviour
{
    public float slowdownFactor = 0.05f; // Facteur de ralentissement du temps
    public float slowdownDuration = 2f; // Dur�e du ralentissement du temps
    public int maxSlowdowns = 3; // Nombre maximum de ralentissements autoris�s

    public TextMeshProUGUI slowdownText; // Faites r�f�rence � l'objet Text (UI Text) dans l'inspecteur
    public CameraMouseLook cameraMouseLook; // Faites r�f�rence au Transform de votre joueur FPS
    public int modifySensibilityX = 2; // Sensibilit� de la souris en mode ralenti
    public int modifySensibilityY = 2; // Sensibilit� de la souris en mode ralenti

    private bool isSlowingDown = false;
    [SerializeField] private int usedSlowdowns = 0;
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
        if (Input.GetButtonDown("Slowmotion") && !canvasToggle.isGamePaused ) 
        {
            if (!isSlowingDown && usedSlowdowns < maxSlowdowns)
            {
                StartCoroutine(SlowTime());
                usedSlowdowns++;
            }
            else if (usedSlowdowns <= maxSlowdowns)
            {
                ResetTime();
            }
        }

        // Mettez � jour le texte de l'UI avec le nombre de ralentissements restants
        slowdownText.text = $"Ralentissements restants : {(maxSlowdowns - usedSlowdowns)}";
    }

    public void AddSlowdowns(int amount)
    {
        usedSlowdowns -= amount;
        usedSlowdowns = Mathf.Clamp(usedSlowdowns, 0, maxSlowdowns);
    }

    private IEnumerator SlowTime()
    {
        isSlowingDown = true;

        // Ralentissez le temps en ajustant Time.timeScale (pour d'autres scripts)
        float originalTimeScale = Time.timeScale;
        Time.timeScale = slowdownFactor;

        // Ralentissez la sensibilit� de la souris
        cameraMouseLook.sensX *= modifySensibilityX; // R�glez la sensibilit� de la souris pour votre script FPS
        cameraMouseLook.sensY *= modifySensibilityY;

        yield return new WaitForSecondsRealtime(slowdownDuration);

        // R�tablissez les valeurs originales
        Time.timeScale = originalTimeScale;
        cameraMouseLook.sensX = originalMouseSensitivityX;
        cameraMouseLook.sensY = originalMouseSensitivityY;

        isSlowingDown = false;
    }

    private void ResetTime()
    {
        // R�tablissez les valeurs originales
        Time.timeScale = 1f;
        cameraMouseLook.sensX = originalMouseSensitivityX;
        cameraMouseLook.sensY = originalMouseSensitivityY;

        isSlowingDown = false;
    }
}
