using UnityEngine;

public class ActivateCircleWithDelayedVFX : MonoBehaviour
{
    public GameObject circle; // Faites glisser votre cercle rouge désactivé depuis l'Inspector Unity.
    public GameObject vfxPrefab; // Le préfab du VFX à attacher en tant qu'enfant.
    public float minActivationTime = 2.0f; // Temps minimum d'attente avant l'activation.
    public float maxActivationTime = 5.0f; // Temps maximum d'attente avant l'activation.
    public float vfxDelay = 1.0f; // Délai avant l'apparition du VFX après l'activation du cercle rouge.
    public float vfxLifetime = 5.0f; // Durée de vie du VFX en secondes.

    private void Start()
    {
        // Assurez-vous que le cercle rouge est désactivé au début.
        if (circle != null)
        {
            circle.SetActive(false);
        }

        // Appelez la méthode ActivateRandomly à intervalles aléatoires.
        InvokeRepeating("ActivateRandomly", Random.Range(minActivationTime, maxActivationTime), Random.Range(minActivationTime, maxActivationTime));
    }

    private void ActivateRandomly()
    {
        if (circle != null)
        {
            // Activez le cercle rouge de manière aléatoire.
            circle.SetActive(!circle.activeSelf);

            if (circle.activeSelf)
            {
                // Si le cercle rouge est activé, appelez la méthode ActivateVFX avec un délai.
                Invoke("ActivateVFX", vfxDelay);
            }
        }
    }

    private void ActivateVFX()
    {
        if (vfxPrefab != null)
        {
            // Instanciez le VFX en tant qu'enfant du cercle.
            GameObject vfxInstance = Instantiate(vfxPrefab, circle.transform);
            vfxInstance.transform.localPosition = Vector3.zero; // Réinitialise la position locale du VFX par rapport au cercle.

            // Détruisez le VFX après la durée de vie spécifiée.
            Destroy(vfxInstance, vfxLifetime);
        }
    }
}
