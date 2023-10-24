using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageController : MonoBehaviour
{
    public int damageAmount = 10; // Montant de dégâts infligés lors de la collision avec un ennemi
    private HealthManager healthManager; // Référence au script HealthManager

    void Start()
    {
        // Obtenez la référence au script HealthManager attaché au même GameObject
        healthManager = GetComponent<HealthManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Vérifiez si la collision a eu lieu avec un ennemi (vous pouvez définir des balises ou d'autres méthodes de détection)
        if (collision.gameObject.CompareTag("Destroyable") && collision.gameObject.layer == 10)
        {
            Debug.Log("OUCH");
            // Infligez des dégâts au joueur en utilisant le script HealthManager
            healthManager.DamageButton(damageAmount);
        }
    }

}
