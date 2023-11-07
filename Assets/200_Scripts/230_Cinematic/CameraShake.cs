using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance; // Instance unique du CameraShake

    public float shakeDuration = 0.2f;
    public float shakeMagnitude = 0.1f;

    private Vector3 originalPosition;

    void Awake()
    {
        if (instance == null)
        {
            instance = this; // Définir l'instance unique
        }
    }

    public void Shake()
    {
        originalPosition = transform.localPosition;
        StartCoroutine(DoShake());
    }

    IEnumerator DoShake()
    {
        float elapsedTime = 0f;
        while (elapsedTime < shakeDuration)
        {
            Vector3 randomPos = originalPosition + Random.insideUnitSphere * shakeMagnitude;
            transform.localPosition = randomPos;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
