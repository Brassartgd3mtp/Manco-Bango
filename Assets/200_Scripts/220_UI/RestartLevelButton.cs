using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements.Experimental;

public class RestartLevelButton : MonoBehaviour
{
    private CheckpointManager checkpointManager;
    private HealthManager healthManager;
    private void Awake()
    {
        checkpointManager = FindObjectOfType<CheckpointManager>();
        healthManager = checkpointManager.GetComponent<HealthManager>();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void LastCheckpoint()
    {
        StartCoroutine(LastCheckPointCoroutine());
    }

    private IEnumerator LastCheckPointCoroutine()
    {
        Time.timeScale = 1.0f;

        foreach (Canvas canva in healthManager.canvas)
        {
            canva.gameObject.SetActive(true);
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //yield return new WaitForSeconds(.1f);
        checkpointManager.ReturnToCheckpoint();

        gameObject.SetActive(false);
        yield break;
    }
}
