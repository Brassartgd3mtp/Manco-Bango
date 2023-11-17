using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections; 

public class BossHealthBarUpdater : MonoBehaviour
{
    public Slider bossRedSlider;
    public Slider bossBlueSlider;
    public TextMeshProUGUI endText;
    public float fadeDuration = 1.0f; // Durée du fondu en secondes
    public float displayDuration = 3.0f; // Durée d'affichage du texte

    private int bossRedTotal;
    private int bossBlueTotal;

    private void Start()
    {
        // Initialiser les valeurs totales des boss
        bossRedTotal = CountObjectsWithTag("BossRed");
        bossBlueTotal = CountObjectsWithTag("BossBlue");
    }

    private void Update()
    {
        // Mettre à jour les barres de progression
        UpdateHealthBars();

        // Vérifier si les barres de vie sont vides et désactiver l'objet si c'est le cas
        CheckAndDisableHealthBars();
    }

    private void UpdateHealthBars()
    {
        // Calculer la progression des boss rouges et bleus
        float bossRedProgress = CalculateProgress("BossRed");
        float bossBlueProgress = CalculateProgress("BossBlue");

        // Mettre à jour les barres de progression
        bossRedSlider.value = bossRedProgress;
        bossBlueSlider.value = bossBlueProgress;
    }

    private int CountObjectsWithTag(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        return objects.Length;
    }

    private float CalculateProgress(string tag)
    {
        int bossCount = CountObjectsWithTag(tag);
        int bossTotal = (tag == "BossRed") ? bossRedTotal : bossBlueTotal;

        // Éviter une division par zéro
        if (bossTotal == 0)
        {
            return 0f;
        }

        return (float)bossCount / bossTotal;
    }

    private void CheckAndDisableHealthBars()
    {
        // Désactiver les barres de vie si elles sont vides
        if (bossRedSlider.value == 0f && bossBlueSlider.value == 0f)
        {
            bossRedSlider.gameObject.SetActive(false);
            bossBlueSlider.gameObject.SetActive(false);

            // Démarrer les coroutines pour le fondu du texte et le délai
            StartCoroutine(FadeInText());
        }
    }

    private IEnumerator FadeInText()
    {
        float startTime = Time.time;
        Color startColor = endText.color;

        // Progression du fondu
        while (Time.time - startTime < fadeDuration)
        {
            float t = (Time.time - startTime) / fadeDuration;
            endText.color = new Color(startColor.r, startColor.g, startColor.b, Mathf.Lerp(0.0f, 1.0f, t));
            yield return null;
        }

        // Assurez-vous que le texte est complètement opaque
        endText.color = new Color(startColor.r, startColor.g, startColor.b, 1.0f);

        // Attendre la durée d'affichage du texte
        yield return new WaitForSeconds(displayDuration);

        // Démarrer la coroutine pour le fondu sortant
        StartCoroutine(FadeOutText());
    }

    private IEnumerator FadeOutText()
    {
        float startTime = Time.time;
        Color startColor = endText.color;

        // Progression du fondu sortant
        while (Time.time - startTime < fadeDuration)
        {
            float t = (Time.time - startTime) / fadeDuration;
            endText.color = new Color(startColor.r, startColor.g, startColor.b, Mathf.Lerp(1.0f, 0.0f, t));
            yield return null;
        }

        // Assurez-vous que le texte est complètement transparent
        endText.color = new Color(startColor.r, startColor.g, startColor.b, 0.0f);
    }
}
