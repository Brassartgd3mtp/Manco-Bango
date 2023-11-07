using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class ScriptIntro : MonoBehaviour
{
    public GameObject Player;
    public GameObject PlayerCam;
    public GameObject Ennemi;
    public GameObject ui;
    public TextMeshProUGUI introText;
    public Image introImage;
    public Image Bulle;

    public float typingSpeed = 0.05f;
    public float fadeInDuration = 1.0f;
    public float fadeOutDuration = 1.0f;
    public float preFadeOutDelay = 2.0f; // Délai avant que l'image ne commence à disparaître

    private string fullText;
    private string currentText = "";

    // Start is called before the first frame update
    void Start()
    {
        Player.SetActive(false);
        PlayerCam.SetActive(false);
        Ennemi.SetActive(false);
        ui.SetActive(false);

        introImage.color = new Color(introImage.color.r, introImage.color.g, introImage.color.b, 0f);
        Bulle.color = new Color(Bulle.color.r, Bulle.color.g, Bulle.color.b, 0f);


        fullText = introText.text;
        introText.text = "";

        StartCoroutine(TypeText(fullText));
        StartCoroutine(FadeImage(true, fadeInDuration));
    }

    IEnumerator TypeText(string textToType)
    {
        foreach (char letter in textToType)
        {
            currentText += letter;
            introText.text = currentText;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(preFadeOutDelay); // Délai avant de faire disparaître l'image
        StartCoroutine(FadeImage(false, fadeOutDuration));
    }

    IEnumerator FadeImage(bool fadeIn, float duration)
    {
        float targetAlpha = fadeIn ? 1f : 0f;
        Color currentColor = introImage.color;
        Color ActualColor = Bulle.color;



        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            introImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, targetAlpha, t));
            Bulle.color = new Color(ActualColor.r, ActualColor.g, ActualColor.b, Mathf.Lerp(ActualColor.a, targetAlpha, t));

            yield return null;
        }
    }

    public void FinAnim()

    {
        introText.text = "";
        Player.SetActive(true);
        PlayerCam.SetActive(true);
        Ennemi.SetActive(true);
        ui.SetActive(true);
        introImage.gameObject.SetActive(false);
        introText.gameObject.SetActive(false);
        GetComponent<Camera>().depth = -1;
    }
}