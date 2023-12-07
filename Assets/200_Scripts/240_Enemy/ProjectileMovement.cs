using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f; // Vitesse de déplacement du projectile
    public float lifetime = 5.0f; // Durée de vie du projectile (en secondes)
    public int damageAmount = 10; // Montant de dégâts infligés par le projectile au joueur

    private Transform player; // Référence au joueur
    private Transform target; // Position cible actuelle

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Trouvez le joueur

        if (player == null)
        {
            Debug.LogError("Le joueur n'a pas été trouvé. Assurez-vous qu'un objet avec le tag 'Player' est présent.");
            return;
        }
        else
        {
            Debug.Log("Joueur trouvé avec succès.");
        }

        target = player; // Initialisez la position cible

        // Détruisez le projectile après la durée de vie spécifiée
        Invoke("DestroyProjectile", lifetime);
    }

    // Appelé à chaque frame
    void Update()
    {
        // Déplacez le projectile vers la position cible (le joueur)
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    // Appelé lorsqu'une collision se produit
    void OnTriggerEnter(Collider other)
    {
        // Vérifiez si l'objet avec lequel le projectile a collision est le joueur
        if (other.CompareTag("Player"))
        {
            Debug.Log("Projectile en collision avec le joueur. Infliger des dégâts.");
            // Réduisez la santé du joueur en utilisant la méthode ApplyDamage
            HealthManager healthManager = other.GetComponent<HealthManager>();
            if (healthManager != null)
            {
                healthManager.ApplyDamage(damageAmount);
            }

            // Annulez l'Invoke avant de détruire le projectile
            Debug.Log("Destruction du projectile après la collision avec le joueur.");
            CancelInvoke();
            Destroy(gameObject);
        }
        else
        {
            // Si la collision n'est pas avec le joueur, ne faites rien (ou effectuez d'autres actions si nécessaire)
        }
    }

}