using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartLevelButton : MonoBehaviour
{
    public string sceneToLoad;

    private Scene sceneToRestart;
    HealthManager healthManager;
    private void Awake()
    {
        Time.timeScale = 1.0f;
    }

    void Start()
    {

       sceneToRestart = SceneManager.GetActiveScene();
        Debug.Log(sceneToRestart.ToString());
    }

    public void RestartLevel()
    {
        // Chargez à nouveau la scène actuelle
        SceneManager.LoadScene(sceneToLoad);
    }
}
