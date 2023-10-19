using UnityEngine;

public class EnemyAttraction : MonoBehaviour
{
    public float attractionSpeed = 5.0f; // Vitesse d'attraction de l'ennemi.
    public float detectionRadius = 5.0f; // Rayon de détection.
    public LayerMask playerLayer; // Couche du joueur.
    private Transform player; // Référence au transform du joueur.
    private bool playerDetected = false; // Indique si le joueur a été détecté.

    private void Start()
    {
        // Trouvez le joueur par son tag "Player".
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Vérifiez si le joueur est dans la zone de détection.
        playerDetected = Physics.CheckSphere(transform.position, detectionRadius, playerLayer);

        // Si le joueur est détecté, attirez l'ennemi vers le joueur.
        if (playerDetected)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            transform.Translate(directionToPlayer * attractionSpeed * Time.deltaTime);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Dessinez une gizmo sphérique pour la zone de détection.
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
