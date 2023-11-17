using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ObjectCounter : MonoBehaviour
{
    public TextMeshProUGUI displayText;
    public float fadeDuration = 1.0f;
    public float displayDuration = 3.0f;
    public Image blueBar;
    public Image redBar;

    private int blueObjectCount = 0;
    private int redObjectCount = 0;
    private int maxBlueObjects = 10; // Remplacez par votre nombre maximum d'objets bleus
    private int maxRedObjects = 10; // Remplacez par votre nombre maximum d'objets rouges

    private void Start()
    {
        displayText.gameObject.SetActive(false);
    }

    private void Update()
    {
        CountObjectsByLayer();

        if (blueObjectCount == 0 && redObjectCount == 0)
        {
            displayText.gameObject.SetActive(true);
            StartCoroutine(FadeAndHideText());
        }

        UpdateProgressBars();
    }

    private void CountObjectsByLayer()
    {
        GameObject[] blueObjects = GameObject.FindGameObjectsWithTag("BossBlue");
        blueObjectCount = blueObjects.Length;

        GameObject[] redObjects = GameObject.FindGameObjectsWithTag("BossRed");
        redObjectCount = redObjects.Length;
    }


    private void UpdateProgressBars()
    {
        float blueProgress = (float)blueObjectCount / maxBlueObjects;
        float redProgress = (float)redObjectCount / maxRedObjects;

        blueBar.fillAmount = Mathf.Lerp(blueBar.fillAmount, blueProgress, Time.deltaTime * 5f);
        redBar.fillAmount = Mathf.Lerp(redBar.fillAmount, redProgress, Time.deltaTime * 5f);
    }

    private IEnumerator FadeAndHideText()
    {
        float startTime = Time.time;
        Color startColor = displayText.color;

        yield return new WaitForSeconds(displayDuration);

        while (Time.time - startTime < fadeDuration)
        {
            float t = (Time.time - startTime) / fadeDuration;
            displayText.color = new Color(startColor.r, startColor.g, startColor.b, Mathf.Lerp(1.0f, 0.0f, t));
            yield return null;
        }

        displayText.color = new Color(startColor.r, startColor.g, startColor.b, 0.0f);
        displayText.gameObject.SetActive(false);

    }
}
