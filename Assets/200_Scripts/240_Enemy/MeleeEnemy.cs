using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : MonoBehaviour
{
    #region Variables
    [Header("Monster Stats")]
    [SerializeField] private int meleeDamage = 10;
    [SerializeField] private float maxAttackCooldown = 2f;
    [SerializeField] private float attackCooldown = 0f;

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
    [SerializeField] private ParticleSystem preChargeParticle;

    private Vector3 startPos;
    private Vector3 targetPos;

    private float distanceToPlayer;
    #endregion

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerHealth = FindAnyObjectByType<HealthManager>();
        preChargeParticle = gameObject.GetComponentInChildren<ParticleSystem>();

        chargeDetectionRange = playerDetectionRange / 1.5f;

        attackCooldown = -1f;
    }

    /// <summary>
    /// J'effectue la méthode DetectPlayer pour trouver un joueur.
    /// Je compare la distance entre le joueur et l'ennemi, si elle est supérieur à la range de charge alors je déplace l'ennemi vers le joueur.
    /// Si mon joueur est en range d'attaque, j'effectue la méthode AttackMelee.
    /// Si mon joueur est en range de charge, je récupère la position de départ de l'ennemi et celle du joueur puis j'effectue la coroutine ChargeToPlayer.
    /// </summary>
    void Update()
    {
        DetectPlayer();

        if (player != null)
        {
            distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer > chargeDetectionRange)
                navMeshAgent.SetDestination(player.position);

            if (distanceToPlayer <= attackRange)
            {
                if (attackCooldown <= 0)
                    AttackMelee();
                else
                    attackCooldown -= Time.deltaTime;
            }
            else if (canChargeAttack && distanceToPlayer <= chargeDetectionRange)
            {
                startPos = transform.position;
                targetPos = player.position;

                StartCoroutine(ChargeToPlayer());
            }
        }
    }

    /// <summary>
    /// Je crée une sphère de détection qui a pour limite de diamètre la variable playerDetectionRange.
    /// Si une entité ayant le component PlayerController entre dans ma sphère, je set la variable player à son GameObject.
    /// </summary>
    private void DetectPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, playerDetectionRange, playerLayer);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out PlayerController _playerController))
            {
                player = _playerController.transform;
               
                return;
            }
        }
        player = null;
    }

    //J'appelle la méthode ApplyDamage du component HealthManager et je reset le cooldown.
    private void AttackMelee()
    {
        attackCooldown = maxAttackCooldown;
        playerHealth.ApplyDamage(meleeDamage);
    }

    /// <summary>
    /// Je crée une variable qui a pour durée la variable duration du ParticleSystem preChargeParticle.
    /// Je joue la particule et j'attend le délai preChargeParticle avant d'effectuer la suite.
    /// Je fais un Lerp de la position de départ de l'ennemi jusqu'à la position de départ du joueur avec comme ratio le temps passé sur la durée totale d'une charge.
    /// Si jamais mon player sort de la range de détection de l'ennemi, j'annule la charge.
    /// Pour finir, j'attend un coolodwn pour pouvoir effectuer de nouveau la charge.
    /// </summary>
    private IEnumerator ChargeToPlayer()
    {
        float preChargeTimer = preChargeParticle.main.duration;

        preChargeParticle.Play();
        yield return new WaitForSeconds(preChargeTimer);

        canChargeAttack = false;

        float elapsedTime = 0f;

        while (elapsedTime < chargeDuration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / chargeDuration);

            if (distanceToPlayer > playerDetectionRange)
            {
                preChargeParticle.Stop();
                canChargeAttack = true;
                yield break;
            }

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

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
