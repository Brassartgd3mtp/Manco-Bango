using UnityEngine;
using System.Collections; // Ajouter cette ligne pour résoudre l'erreur "IEnumerator introuvable"

public class FogController : MonoBehaviour
{
    [Header("Fog Settings")]
    public float targetFogDensity = 0.05f;
    public float transitionSpeed = 1.0f;

    private float initialFogDensity;

    private void Start()
    {
        initialFogDensity = RenderSettings.fogDensity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ChangeFogDensity(targetFogDensity));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ChangeFogDensity(initialFogDensity));
        }
    }

    private IEnumerator ChangeFogDensity(float targetDensity)
    {
        float currentDensity = RenderSettings.fogDensity;
        float elapsedTime = 0.0f;

        while (elapsedTime < transitionSpeed)
        {
            elapsedTime += Time.deltaTime;
            RenderSettings.fogDensity = Mathf.Lerp(currentDensity, targetDensity, elapsedTime / transitionSpeed);
            yield return null;
        }

        RenderSettings.fogDensity = targetDensity;
    }
}
