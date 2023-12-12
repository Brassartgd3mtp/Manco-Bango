using System.Collections;
using UnityEngine;

public class BarrelRotate : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private float rotSpeed = 12;
    [SerializeField] private int rotationState = 0;
    private static bool isRotating = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Rotate()
    {
        if (!isRotating)
        {
            StartCoroutine(RotateCoroutine());
        }
    }

    private IEnumerator RotateCoroutine()
    {
        isRotating = true;

        float targetAngle = 0f;

        switch (rotationState) //J'établis une liste de rotation Z en fonction d'un index
        {
            case 0: targetAngle = 62.7f; break;
            case 1: targetAngle = 119.2f; break;
            case 2: targetAngle = 181.1f; break;
            case 3: targetAngle = 242.1f; break;
            case 4: targetAngle = 301f; break;
            case 5: targetAngle = 350f; break;
        }

        while (rectTransform.rotation.eulerAngles.z < targetAngle) //J'effectue la rotation jusqu'à atteindre la targetAngle
        {
            rectTransform.rotation *= Quaternion.Euler(0, 0, Time.deltaTime * (rotSpeed * 50));
            yield return null;
        }

        //Je corrige la rotation Z après avoir fait mon while
        if (rotationState < 5)
            rectTransform.rotation = Quaternion.Euler(0, 0, targetAngle);
        else
            rectTransform.rotation = Quaternion.Euler(0, 0, 0);

        //J'incrémente rotationState de 1 et dès qu'il atteint 6, sa valeur repasse à 0
        rotationState = (rotationState + 1) % 6;

        isRotating = false;
    }
}
