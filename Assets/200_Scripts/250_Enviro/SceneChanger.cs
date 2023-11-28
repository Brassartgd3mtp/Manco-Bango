using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public SceneFader sceneFader; // Référence au script de fondu
    public string sceneToLoad;
    public Canvas[] canvasesToDisable; // Liste des Canvas à désactiver

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Désactiver tous les Canvas de la liste
            foreach (Canvas canvas in canvasesToDisable)
            {
                canvas.enabled = false;
            }

            // Appel de la transition avec fondu
            sceneFader.FadeToScene(sceneToLoad);
        }
    }
}
