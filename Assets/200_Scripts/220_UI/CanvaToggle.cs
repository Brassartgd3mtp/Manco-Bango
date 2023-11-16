using UnityEngine;

public class CanvasToggle : MonoBehaviour
{
    public Canvas canvas; // Référence au Canvas que vous souhaitez activer/désactiver
    public bool isGamePaused = false;
    public HealthManager healthManager;
    public InteractableItem interactableItem;
    private void Start()
    {
        if (canvas != null)
        {
            // Au démarrage, désactivez le Canvas
            canvas.enabled = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !healthManager.Escape)
        {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (canvas != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !healthManager.Escape)
            {
                // Inversez l'état du Canvas (activé ou désactivé) lorsque la touche "Echap" est enfoncée
                canvas.enabled = !canvas.enabled;
            }
        }
    }
    private void PauseGame()
    {
        Time.timeScale = 0f; // Mettez le temps à zéro pour mettre le jeu en pause
        isGamePaused = true;

        // Affichez le curseur de la souris pour permettre l'interaction avec l'interface utilisateur
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void ResumeGame()
    {
        if(!interactableItem.buttonAlreadyPressed) Time.timeScale = 1f; // Rétablissez le temps normal pour reprendre le jeu
        isGamePaused = false;

        // Cachez à nouveau le curseur de la souris
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
