using UnityEngine;

public class ObjectSpawnerTrigger : MonoBehaviour
{
    public ObjectSpawner objectSpawner; // Référence au script qui contient la liste d'objets

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject obj in objectSpawner.objectsToSpawn)
            {
                obj.SetActive(true); // Activez chaque objet de la liste
            }
        }
    }
}
