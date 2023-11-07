using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private static Vector3 checkpointPosition;
    public static float SavedHealth;

    [SerializeField] private HealthManager healthManager;
    private void Start()
    {
        checkpointPosition = transform.position; // Le point de départ initial devient le premier checkpoint
    }

    // Fonction pour définir un nouveau checkpoint
    public void SetCheckpoint(Vector3 position, float savedHealth)
    {
        SavedHealth = savedHealth;
        checkpointPosition = position;
    }

    // Fonction pour retourner au dernier checkpoint
    public void ReturnToCheckpoint(Transform _player)
    {
        _player.position = checkpointPosition;

        healthManager.health = SavedHealth;

        Debug.Log("TP");
    }
}
