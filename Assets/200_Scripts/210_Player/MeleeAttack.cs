using UnityEngine;
using UnityEngine.UI;

public class MeleeAttack : MonoBehaviour
{
    public float attackRange = 2.0f; // Portée de l'attaque
    public GameObject particlePrefab; // Préfab du système de particules
    public float particleDuration = 1.0f; // Durée d'affichage des particules en secondes
    public float attackCooldown = 2.0f; // Temps d'attente entre chaque attaque en secondes
    public Image cooldownImage; // Image utilisée pour afficher le cooldown

    private bool canAttack = true;

    private void OnDrawGizmos()
    {
        // Dessinez une sphère gizmo pour visualiser la portée de l'attaque
        Gizmos.color = Color.red; // Couleur du gizmo
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void Update()
    {
        // Vérifiez si le joueur peut attaquer et si la touche est pressée
        if (canAttack && Input.GetKeyDown(KeyCode.V))
        {
            // Démarrez la coroutine d'attaque
            StartCoroutine(PerformMeleeAttack());
        }
    }

    private System.Collections.IEnumerator PerformMeleeAttack()
    {
        // Désactivez la possibilité d'attaquer pendant le temps de recharge
        canAttack = false;

        // Mettez à jour l'image de cooldown pour indiquer le temps de recharge
        if (cooldownImage != null)
        {
            cooldownImage.fillAmount = 1.0f; // Remplit complètement l'image au début du cooldown
        }

        // Attendez le temps de recharge
        float cooldownTimer = attackCooldown;
        while (cooldownTimer > 0f)
        {
            yield return null;
            cooldownTimer -= Time.deltaTime;

            // Mettez à jour l'image de cooldown pendant l'attente
            if (cooldownImage != null)
            {
                cooldownImage.fillAmount = cooldownTimer / attackCooldown;
            }
        }

        // Réactivez la possibilité d'attaquer
        canAttack = true;

        // Obtenez tous les colliders dans la portée de l'attaque
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);

        foreach (Collider hitCollider in hitColliders)
        {
            // Vérifiez si l'objet a le tag "Enemy" et le layer "Enemy"
            if (hitCollider.CompareTag("Enemy") && hitCollider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                // Détruisez immédiatement l'ennemi
                // Obtenez la référence à l'ennemi s'il est touché par le raycast
                EnemyHealth enemyHealth = hitCollider.GetComponent<EnemyHealth>();

                if (enemyHealth != null)
                {
                    // Appel de la fonction TakeDamage pour réduire les points de vie de l'ennemi
                    enemyHealth.TakeDamage(10);
                    Debug.Log("-10PV");

                    // Instanciez les particules
                    if (particlePrefab != null)
                    {
                        GameObject particleInstance = Instantiate(particlePrefab, hitCollider.transform.position, Quaternion.identity);

                        // Désactivez les particules après la durée spécifiée
                        Destroy(particleInstance, particleDuration);
                    }
                }
            }
        }

        // Réinitialisez l'image de cooldown à la fin de l'attaque
        if (cooldownImage != null)
        {
            cooldownImage.fillAmount = 0.0f;
        }
    }
}
