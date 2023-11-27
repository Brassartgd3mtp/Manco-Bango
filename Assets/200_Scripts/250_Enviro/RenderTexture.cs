using UnityEngine;
using UnityEngine.UI;

public class SwitchRenderTexture : MonoBehaviour
{
    public Camera myCamera;
    public RenderTexture firstRenderTexture;
    public RenderTexture secondRenderTexture;
    public RawImage firstRawImage;
    public RawImage secondRawImage;

    void Start()
    {
        // Assurez-vous que la caméra utilise la première Render Texture au démarrage
        myCamera.targetTexture = firstRenderTexture;
        // Désactivez le deuxième RawImage au démarrage
        secondRawImage.gameObject.SetActive(false);
    }

    public void SwitchToFirstTexture()
    {
        // Changez la Render Texture de la caméra à la première
        myCamera.targetTexture = firstRenderTexture;
        // Désactivez le deuxième RawImage
        secondRawImage.gameObject.SetActive(false);
        // Activez le premier RawImage
        firstRawImage.gameObject.SetActive(true);
    }

    public void SwitchToSecondTexture()
    {
        // Changez la Render Texture de la caméra à la deuxième
        myCamera.targetTexture = secondRenderTexture;
        // Désactivez le premier RawImage
        firstRawImage.gameObject.SetActive(false);
        // Activez le deuxième RawImage
        secondRawImage.gameObject.SetActive(true);
    }
}
