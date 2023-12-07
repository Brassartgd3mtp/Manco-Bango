using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static Transform checkpoint;
    public static float SavedHealth;

    [SerializeField] private PlayerController playerController;
    [SerializeField] private HealthManager healthManager;
    private void Start()
    {
        checkpoint = transform; // Le point de départ initial devient le premier checkpoint
        playerController = GetComponent<PlayerController>();
        healthManager = GetComponent<HealthManager>();
    }

    // Fonction pour définir un nouveau checkpoint
    public void SetCheckpoint(Transform position, float savedHealth)
    {
        SavedHealth = savedHealth;
        checkpoint = position;
    }

    // Fonction pour retourner au dernier checkpoint
    public void ReturnToCheckpoint()
    {
        healthManager.health = SavedHealth;

        healthManager.isGameOver = false;
        healthManager.canPressEscape = true;

        //playerController.SetNewPosition(checkpoint);
    }
}
