using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    [SerializeField] private HealthManager healthManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckpointManager checkpointManager = FindAnyObjectByType<CheckpointManager>();
            // Met à jour le dernier checkpoint du joueur en utilisant la position de ce checkpoint
            checkpointManager.SetCheckpoint(transform.position, healthManager.health);
        }
    }
}
