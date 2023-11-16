using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class SceneFader : MonoBehaviour
{
    public Image fadeImage;
    public float fadeSpeed = 1.0f;
    public Canvas endCanva;

    private bool isFading = false;

    public void FadeToScene(string sceneName)
    {
        if (!isFading)
        {
            StartCoroutine(FadeOut(sceneName));
        }
    }

    public void FuncFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        fadeImage.canvasRenderer.SetAlpha(1.0f);
        fadeImage.CrossFadeAlpha(0, fadeSpeed, false);
        yield return new WaitForSeconds(fadeSpeed);
        endCanva.gameObject.SetActive(false);
        yield break;
    }

    IEnumerator FadeOut(string sceneName)
    {
        endCanva.gameObject.SetActive(true);
        fadeImage.canvasRenderer.SetAlpha(0.0f);
        fadeImage.CrossFadeAlpha(1, fadeSpeed, false);
        yield return new WaitForSeconds(fadeSpeed);
        SceneManager.LoadScene(sceneName);
    }
}
