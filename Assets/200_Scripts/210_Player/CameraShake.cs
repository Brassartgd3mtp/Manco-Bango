using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Transform originalTransform;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private float shakeDuration = 0f;
    private float shakeMagnitude = 0.1f;

    private void Awake()
    {
        originalTransform = transform;
    }

    public void Shake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }

    private void Update()
    {
        if (shakeDuration > 0)
        {
            originalPosition = originalTransform.position;
            originalRotation = originalTransform.rotation;

            float xShake = Random.Range(-1f, 1f) * shakeMagnitude;
            float yShake = Random.Range(-1f, 1f) * shakeMagnitude;

            originalPosition.x += xShake;
            originalPosition.y += yShake;

            originalTransform.position = originalPosition;

            shakeDuration -= Time.deltaTime;
        }
        else
        {
            shakeDuration = 0f;
            originalTransform.position = originalPosition;
            originalTransform.rotation = originalRotation;
        }
    }
}
