using UnityEngine;

public class CanvasActivator : MonoBehaviour
{
    public Canvas canvasToShow;



    private void Start()
    {
        canvasToShow.gameObject.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Activer le Canvas lorsque le joueur entre dans le collider
            canvasToShow.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Désactiver le Canvas lorsque le joueur quitte le collider
            canvasToShow.gameObject.SetActive(false);
        }
    }
}
