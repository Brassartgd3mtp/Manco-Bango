using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public float attackRange = 2.0f; // Portée de l'attaque


    private void OnDrawGizmos()
    {
        // Dessinez une sphère gizmo pour visualiser la portée de l'attaque
        Gizmos.color = Color.red; // Couleur du gizmo
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            PerformMeleeAttack();
        }
    }

    void PerformMeleeAttack()
    {
        Debug.Log("on commence00"); 
        // Obtenez tous les colliders dans la portée de l'attaque
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);

        foreach (Collider hitCollider in hitColliders)
        {
            // Vérifiez si l'objet a le tag "Destroyable" et le layer "Enemy"
            if (hitCollider.CompareTag("Enemy") && hitCollider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                Debug.Log("ofcurse");
                // Détruisez immédiatement l'ennemi
                // Obtenez la référence à l'ennemi s'il est touché par le raycast
                EnemyHealth enemyHealth = hitCollider.GetComponent<EnemyHealth>();

                if (enemyHealth != null)
                {
                    Debug.Log("touchée"); 
                    // Appel de la fonction TakeDamage pour réduire les points de vie de l'ennemi
                    enemyHealth.TakeDamage(10);
                    Debug.Log("-10PV");
                }
            }
        }
    }
}
