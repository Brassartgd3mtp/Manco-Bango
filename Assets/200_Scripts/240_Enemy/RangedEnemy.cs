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
    private float lastAttackTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
    }

    void AttackRanged()
    {
        if (projectilePrefab != null && launchPoint != null)
            Instantiate(projectilePrefab, launchPoint.position, Quaternion.identity);
    }
}
