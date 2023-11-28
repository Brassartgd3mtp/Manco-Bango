using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public int damageAmount = 10; // Montant de d�g�ts � infliger au joueur

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // V�rifiez si l'objet entrant en collision est le joueur (utilisez un tag "Player" sur l'objet joueur).
            // Si c'est le joueur, infligez des d�g�ts au joueur en appelant une fonction du gestionnaire de sant� du joueur.
            HealthManager playerHealth = other.GetComponent<HealthManager>();
            if (playerHealth != null)
            {
                playerHealth.ApplyDamage(damageAmount);
            }

         
        }
    }
}
