using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private Vector3 checkpointPosition;
    public float SavedHealth; 
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
    public void ReturnToCheckpoint()
    {
        Debug.Log("TP");
        // Déplacez le joueur à la position du dernier checkpoint
        transform.position = checkpointPosition;
    }
}
