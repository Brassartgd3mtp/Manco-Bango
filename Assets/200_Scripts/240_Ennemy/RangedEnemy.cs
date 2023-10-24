using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemy : MonoBehaviour
{
    public float attackRange = 10f;
    public float attackCooldown = 2f;
    public Transform throwPoint;
    public GameObject throwableObjectPrefab;
    public float throwForce = 10f;
    public float distanceToMaintain = 5f; // Distance à maintenir entre l'ennemi et le joueur

    private Transform player;
    private NavMeshAgent navMeshAgent;
    private float lastAttackTime;
    private Vector3 initialPosition; // Position initiale de l'ennemi

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        initialPosition = transform.position;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange && Time.time - lastAttackTime >= attackCooldown)
        {
            AttackRanged();

            if (distanceToPlayer <= distanceToMaintain)
            {
                // Si le joueur s'approche trop, retournez à la position initiale
                navMeshAgent.SetDestination(initialPosition);
            }
        }
        else
        {
            // Si l'ennemi est hors de portée, revenez à la position initiale
            navMeshAgent.SetDestination(initialPosition);
        }
    }

    void AttackRanged()
    {
        if (throwableObjectPrefab != null && throwPoint != null)
        {
            GameObject throwableObject = Instantiate(throwableObjectPrefab, throwPoint.position, Quaternion.identity);
            Rigidbody rb = throwableObject.GetComponent<Rigidbody>();

            Vector3 throwDirection = (player.position - throwPoint.position).normalized;
            rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);

            lastAttackTime = Time.time;
        }
    }
}
