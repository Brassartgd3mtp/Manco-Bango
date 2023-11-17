using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BarrelRotate : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private float rotSpeed = 12;
    [SerializeField] private int rotationType = 0;
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

        switch (rotationType)
        {
            case 0: targetAngle = 62.7f; break;
            case 1: targetAngle = 119.2f; break;
            case 2: targetAngle = 181.1f; break;
            case 3: targetAngle = 242.1f; break;
            case 4: targetAngle = 301f; break;
            case 5: targetAngle = 350f; break;
        }

        while (rectTransform.rotation.eulerAngles.z < targetAngle)
        {
            rectTransform.rotation *= Quaternion.Euler(0, 0, Time.deltaTime * (rotSpeed * 50));
            yield return null;
        }

        // Correction pour s'assurer que la rotation atteint exactement l'angle cible
        if (rotationType < 5)
            rectTransform.rotation = Quaternion.Euler(0, 0, targetAngle);
        else
            rectTransform.rotation = Quaternion.Euler(0, 0, 0);

        isRotating = false;

        // Incrémentez la rotationType uniquement si elle est inférieure à 5
        rotationType = (rotationType + 1) % 6;
    }
}
