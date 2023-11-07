using System.Collections;
using UnityEngine;
using TMPro;

public class TimeSlowdown : MonoBehaviour
{
    [Header("Slowdown")]
    [SerializeField] private float slowdownFactor = 0.05f; //Facteur de ralentissement du temps
    [SerializeField] private float slowdownDuration = 2f;
    [SerializeField] private int maxSlowdowns = 3;
    [SerializeField] private int usedSlowdowns = 0;

    [Header("Sensibility")]
    [SerializeField] private int modifySensibilityX = 2;
    [SerializeField] private int modifySensibilityY = 2;
    [HideInInspector] public bool isSlowingDown = false;
    private int originalMouseSensitivityX;
    private int originalMouseSensitivityY;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI slowdownText;
    [SerializeField] private CanvasToggle canvasToggle;
    [SerializeField] private InteractableItem interactableItem;

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
        originalMouseSensitivityX = CameraMouseLook.sensX;
        originalMouseSensitivityY = CameraMouseLook.sensY;

        isSlowingDown = true;

        // Ralentissez le temps en ajustant Time.timeScale (pour d'autres scripts)
        float originalTimeScale = Time.timeScale;
        Time.timeScale = slowdownFactor;

        // Ralentissez la sensibilit� de la souris
        CameraMouseLook.sensX *= modifySensibilityX; // R�glez la sensibilit� de la souris pour votre script FPS
        CameraMouseLook.sensY *= modifySensibilityY;

        yield return new WaitForSecondsRealtime(slowdownDuration);

        // R�tablissez les valeurs originales
        Time.timeScale = originalTimeScale;
        CameraMouseLook.sensX = originalMouseSensitivityX;
        CameraMouseLook.sensY = originalMouseSensitivityY;

        isSlowingDown = false;
    }

    private void ResetTime()
    {
        // R�tablissez les valeurs originales
        Time.timeScale = 1f;
        CameraMouseLook.sensX = originalMouseSensitivityX;
        CameraMouseLook.sensY = originalMouseSensitivityY;

        isSlowingDown = false;
    }
}
