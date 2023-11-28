using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public int damageAmount = 10; // Montant de dégâts à infliger au joueur

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Vérifiez si l'objet entrant en collision est le joueur (utilisez un tag "Player" sur l'objet joueur).
            // Si c'est le joueur, infligez des dégâts au joueur en appelant une fonction du gestionnaire de santé du joueur.
            HealthManager playerHealth = other.GetComponent<HealthManager>();
            if (playerHealth != null)
            {
                playerHealth.ApplyDamage(damageAmount);
            }

         
        }
    }
}
