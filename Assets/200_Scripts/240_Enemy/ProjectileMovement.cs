using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5.0f;
    public int damageAmount = 10;

    private Transform player; // Référence au joueur

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Trouvez le joueur

        //if (player == null)
        //{
        //    Debug.LogError("Le joueur n'a pas été trouvé. Assurez-vous qu'un objet avec le tag 'Player' est présent.");
        //    return;
        //}
        //else
        //{
        //    Debug.Log("Joueur trouvé avec succès.");
        //}

        // Détruisez le projectile après la durée de vie spécifiée
        Destroy(gameObject, lifetime);
    }

    // Appelé à chaque frame
    void Update()
    {
        // Déplacez le projectile vers la position cible (le joueur)
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    // Appelé lorsqu'une collision se produit
    void OnTriggerEnter(Collider other)
    {
        // Vérifiez si l'objet avec lequel le projectile a collision est le joueur
        if (other.CompareTag("Player"))
        {
            // Réduisez la santé du joueur en utilisant la méthode ApplyDamage
            HealthManager healthManager = other.GetComponent<HealthManager>();

            if (healthManager != null)
            {
                healthManager.ApplyDamage(damageAmount);
            }

            Destroy(gameObject);
        }
    }
}