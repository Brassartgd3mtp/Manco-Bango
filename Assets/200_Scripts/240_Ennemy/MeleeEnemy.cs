using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : MonoBehaviour
{
    public float attackRange = 2f;
    public float attackCooldown = 2f;
    public int meleeDamage = 10;
    public float playerDetectionRange = 5f;
    public LayerMask playerLayer;

    private Transform player;
    private NavMeshAgent navMeshAgent;
    private float lastAttackTime;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        lastAttackTime = -attackCooldown; // Pour permettre la première attaque
    }

    void Update()
    {
        DetectPlayer();

        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= attackRange && Time.time - lastAttackTime >= attackCooldown)
            {
                AttackMelee();
            }

            navMeshAgent.SetDestination(player.position);
        }
    }

    void DetectPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, playerDetectionRange, playerLayer);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                player = hitCollider.transform;
                return; // Arrêtez de chercher dès que le joueur est trouvé
            }
        }

        // Si le joueur n'est pas détecté, effacez la référence au joueur
        player = null;
    }

    void AttackMelee()
    {
        // Gérer les dégâts au corps à corps ici
        lastAttackTime = Time.time;
    }
}
