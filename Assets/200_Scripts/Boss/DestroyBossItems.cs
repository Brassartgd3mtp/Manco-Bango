using UnityEngine;

public class DestroyBossItems : MonoBehaviour
{
    public GameObject particleEffectPrefab; // Préfab de l'effet de particules

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boss"))
        {
            // L'objet touché a le tag "boss"
            Destroy(other.gameObject); // Détruit l'objet boss

            // Créez un effet de particules à l'emplacement de la collision
            if (particleEffectPrefab != null)
            {
                Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
