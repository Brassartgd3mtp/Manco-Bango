using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Monster Stats")]
    [SerializeField] private int meleeDamage = 10;
    [SerializeField] private float attackCooldown = 2f;
    private float lastAttackTime;

    [Space]
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float playerDetectionRange = 12f;
    
    [Header("Charge Stats")]
    [SerializeField] private float chargeAttackCooldown = 2f;
    [SerializeField] private int chargeDuration = 5;
    [SerializeField] private int chargeSpeed = 5;
    [SerializeField] private float chargeDetectionRange;
    [SerializeField] private bool canChargeAttack = true;

    [Space]
    [SerializeField] private LayerMask playerLayer;

    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private HealthManager playerHealth;

    private Vector3 startPos;
    private Vector3 targetPos;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        playerHealth = FindAnyObjectByType<HealthManager>();

        chargeDetectionRange = playerDetectionRange / 1.5f;

        lastAttackTime = -attackCooldown;
    }

    void Update()
    {
        DetectPlayer();

        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer > chargeDetectionRange)
                navMeshAgent.SetDestination(player.position);

            if (distanceToPlayer <= attackRange)
            {
                if (Time.time - lastAttackTime >= attackCooldown) 
                    AttackMelee();
            }
            else if (canChargeAttack && distanceToPlayer <= chargeDetectionRange)
            {
                startPos = transform.position;
                targetPos = player.position;
                StartCoroutine(ChargeToPlayer());
            }
        }
    }

    private void DetectPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, playerDetectionRange, playerLayer);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                player = hitCollider.transform;
                return;
            }
        }
        player = null;
    }

    private void AttackMelee()
    {
        lastAttackTime = Time.time;
        playerHealth.ApplyDamage(meleeDamage);
    }

    private IEnumerator ChargeToPlayer()
    {
        canChargeAttack = false;

        float elapsedTime = 0f;

        while (elapsedTime < chargeDuration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / chargeDuration);
            elapsedTime += Time.deltaTime * chargeSpeed;
            yield return null;
        }

        yield return new WaitForSeconds(chargeAttackCooldown);
        canChargeAttack = true;

        yield break;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerDetectionRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chargeDetectionRange);
    }
}
