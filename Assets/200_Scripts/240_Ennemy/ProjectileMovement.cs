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
        target = player; // Initialisez la position cible

        // Détruisez le projectile après la durée de vie spécifiée
        Invoke("DestroyProjectile", lifetime);
    }

    // Appelé à chaque frame
    void Update()
    {
        // Mettez à jour la position cible à chaque frame en fonction de la position actuelle du joueur
        target = player;

        // Déplacez le projectile vers la position cible (le joueur)
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    // Appelé lorsqu'une collision se produit
    void OnTriggerEnter(Collider other)
    {
        // Vérifiez si l'objet avec lequel le projectile a collision est sur la couche "Enemies"
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            // Ignorez la collision avec cet ennemi (même couche)
            return;
        }

        // Vérifiez si l'objet avec lequel le projectile a collision est le joueur
        if (other.CompareTag("Player"))
        {
            // Réduisez la santé du joueur en utilisant la méthode DamageButton
            HealthManager healthManager = other.GetComponent<HealthManager>();
            if (healthManager != null)
            {
                healthManager.DamageButton(damageAmount);
            }
        }

        // Détruisez le projectile lorsqu'il entre en collision avec n'importe quel autre objet
        DestroyProjectile();
    }

    // Fonction pour détruire le projectile
    void DestroyProjectile()
    {
        CancelInvoke(); // Annule l'appel à la destruction si elle est déclenchée manuellement
        Destroy(gameObject);
    }
}
