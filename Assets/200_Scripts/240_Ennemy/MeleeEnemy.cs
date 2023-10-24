using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : MonoBehaviour
{
    public float attackRange = 2f;
    public float attackCooldown = 2f;
    public int meleeDamage = 10;

    private Transform player;
    private NavMeshAgent navMeshAgent;
    private float lastAttackTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange && Time.time - lastAttackTime >= attackCooldown)
        {
            AttackMelee();
        }

        navMeshAgent.SetDestination(player.position);
    }

    void AttackMelee()
    {
        // Gérer les dégâts au corps à corps ici
        Debug.Log("Melee enemy attack!");
        lastAttackTime = Time.time;
    }
}
