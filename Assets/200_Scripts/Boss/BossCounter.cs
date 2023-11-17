using UnityEngine;

public class BossCounter : MonoBehaviour
{
    private int bossRedCount;
    private int bossBlueCount;

    private void Update()
    {
        // Actualiser les compteurs à chaque frame
        UpdateObjectCounters();
    }

    private void UpdateObjectCounters()
    {
        // Compter les objets avec le tag "BossRed"
        bossRedCount = CountObjectsWithTag("BossRed");

        // Compter les objets avec le tag "BossBlue"
        bossBlueCount = CountObjectsWithTag("BossBlue");

        // Afficher les données actualisées dans la console
        Debug.Log("Nombre d'objets avec le tag 'BossRed': " + bossRedCount);
        Debug.Log("Nombre d'objets avec le tag 'BossBlue': " + bossBlueCount);
    }

    private int CountObjectsWithTag(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        return objects.Length;
    }
}
