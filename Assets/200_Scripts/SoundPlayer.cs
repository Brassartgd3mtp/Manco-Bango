using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioSource audioSource; // Référence à l'AudioSource.

    private void Start()
    {
        // Assurez-vous que l'AudioSource est référencé dans l'inspecteur.
        if (audioSource == null)
        {
            Debug.LogError("Veuillez assigner l'AudioSource dans l'inspecteur.");
        }
    }

    // Fonction pour jouer le son.
    public void PlaySound()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
