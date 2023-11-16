using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab de l'ennemi à faire apparaître
    public float spawnInterval = 5.0f; // Intervalle de temps entre chaque spawn
    public int maxEnemies = 10; // Nombre maximal d'ennemis à instancier
    public Collider spawnZone; // Collider délimitant la zone de spawn

    private void Start()
    {
        // Commencez à faire apparaître des ennemis à intervalles réguliers
        InvokeRepeating("SpawnEnemy", 0.0f, spawnInterval);
    }

    private void SpawnEnemy()
    {
        // Ne faites rien si le nombre maximal d'ennemis est atteint
        if (GameObject.FindGameObjectsWithTag("EnemyBoss").Length >= maxEnemies)
        {
            Debug.Log("Nombre maximal d'ennemis atteint. Pas de spawn.");
            return;
        }

        // Déterminez une position aléatoire à l'intérieur du collider de la zone
        Vector3 randomSpawnPoint = GetRandomPointInCollider(spawnZone);

        if (randomSpawnPoint != Vector3.zero)
        {
            // Faites apparaître l'ennemi à la position aléatoire
            Instantiate(enemyPrefab, randomSpawnPoint, Quaternion.identity);
            Debug.Log("Ennemi spawné à : " + randomSpawnPoint);
        }
        else
        {
            Debug.LogWarning("Erreur lors de la détermination du point de spawn. Assurez-vous que le collider de la zone est correct.");
        }
    }

    // Fonction pour obtenir un point aléatoire à l'intérieur d'un collider
    private Vector3 GetRandomPointInCollider(Collider collider)
    {
        Vector3 randomPoint = Vector3.zero;

        if (collider != null)
        {
            Vector3 bounds = collider.bounds.size;

            randomPoint = new Vector3(
                Random.Range(collider.bounds.min.x, collider.bounds.max.x),
                collider.transform.position.y,
                Random.Range(collider.bounds.min.z, collider.bounds.max.z)
            );
        }

        return randomPoint;
    }
}
