using System.Collections;
using UnityEngine;

public class DisappearAndAppear : MonoBehaviour
{
    public GameObject objectToToggle; // L'objet que vous voulez faire disparaître et apparaître.
    public float toggleInterval = 2.0f; // L'intervalle en secondes entre les changements d'état.

    private bool isObjectActive = true;

    private void Start()
    {
        // Démarrer la coroutine pour effectuer les changements d'état à intervalles réguliers.
        StartCoroutine(ToggleObject());
    }

    private IEnumerator ToggleObject()
    {
        while (true)
        {
            // Inverser l'état de l'objet actif.
            isObjectActive = !isObjectActive;

            // Activer ou désactiver l'objet en fonction de l'état.
            if (objectToToggle != null) objectToToggle.SetActive(isObjectActive);
            else Destroy(this);

            // Attendre pendant l'intervalle spécifié avant de continuer.
            yield return new WaitForSeconds(toggleInterval);
        }
    }
}
