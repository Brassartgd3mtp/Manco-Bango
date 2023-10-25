using UnityEngine;
using UnityEngine.AI;

public class RangedEnemy : MonoBehaviour
{
    [Range(0f, 50f)] // Utilisation de [Range] pour rendre attackRange modifiable dans l'inspecteur Unity
    public float attackRange = 10f;
    public GameObject projectilePrefab;
    public Transform launchPoint;
    public float attackCooldown = 2f;

    private Transform player;
    private NavMeshAgent navMeshAgent;
    private float lastAttackTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        lastAttackTime = Time.time;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange && Time.time - lastAttackTime >= attackCooldown)
        {
            AttackRanged();
            lastAttackTime = Time.time;
        }

        navMeshAgent.SetDestination(player.position);
    }

    void AttackRanged()
    {
        if (projectilePrefab != null && launchPoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, Quaternion.identity);
            // Utilisez un script pour déplacer le projectile vers le joueur
        }
    }

    // Fonction pour dessiner un gizmo dans l'éditeur Unity
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
