using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public int slowdownsToAdd = 1; // Nombre de slowdowns à ajouter

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assurez-vous que l'objet qui entre en collision est le joueur
        {
            TimeSlowdown playerTimeSlowdown = other.GetComponent<TimeSlowdown>();

            if (playerTimeSlowdown != null)
            {
                Debug.Log("Se rentre0");
                playerTimeSlowdown.AddSlowdowns(slowdownsToAdd);
                Destroy(gameObject); // Détruisez l'objet ramassé
            }
        }
    }
}
