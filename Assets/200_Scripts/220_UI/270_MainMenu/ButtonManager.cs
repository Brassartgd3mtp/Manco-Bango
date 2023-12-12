using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ButtonManager : MonoBehaviour
{
    public Toggle fullscreenToggle;

    private void Start()
    {
        // Assurez-vous que le Toggle est initialisé et qu'il a une fonction de rappel associée.
        if (fullscreenToggle != null)
        {
            // Associez la fonction ToggleFullscreen à l'événement "onValueChanged" du Toggle.
            fullscreenToggle.onValueChanged.AddListener(ToggleFullscreenMode);
        }
    }

    public string Level_1 = "Level_1";


    // Fonction pour charger un niveau en utilisant son nom
    public void LoadLevel(string Level_1)
    {
        SceneManager.LoadScene(Level_1);
    }

    // Fonction pour quitter le jeu (utilisez Application.Quit)
    public void QuitGame()
    {
        Application.Quit();
    }

    // GameObject du panneau que vous souhaitez ouvrir/fermer
    public GameObject panel;

    // Fonction pour ouvrir ou fermer le panneau
    public void TogglePanel()
    {
        if (panel != null)
        {
            panel.SetActive(!panel.activeSelf); // Inverse l'état actuel du panneau
     
        }

    }

    public void ToggleFullscreenMode(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        Debug.Log("FullScreen");
    }
    public void ChargerLeNiveau()
    {
        // Charger le niveau spécifié
        SceneManager.LoadScene(Level_1);
    }
}
