using UnityEngine;

public class DisappearOnDestroy : MonoBehaviour
{
    public GameObject objectToDisappear; // L'objet que vous souhaitez faire disparaître
    public string destroyableTag = "Destroyable"; // Tag pour les objets détruisibles

    private void Update()
    {
        // Vérifie si tous les objets avec le tag "Destroyable" sont détruits
        if (AllObjectsDestroyedWithTag(destroyableTag))
        {
            // Fait disparaître l'objet spécifié
            if (objectToDisappear != null)
            {
                objectToDisappear.SetActive(false);
                

            }

            // Vous pouvez également détruire l'objet au lieu de le désactiver si vous le souhaitez
            // Destroy(objectToDisappear);
        }
    }

    private void PlaySound()
    {
        if (objectToDisappear == null)
        {
            DoorSound();

        }

    }

    // Fonction pour vérifier si tous les objets avec un tag spécifié sont détruits
    private bool AllObjectsDestroyedWithTag(string tag)
    {

       
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
        return objectsWithTag.Length == 0;
    }

    public void DoorSound(float volume = 1.0f)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;
        
        AudioManager.Instance.PlaySound(20, audioSource);
    }

}
