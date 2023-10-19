using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public int slowdownsToAdd = 1; // Nombre de slowdowns � ajouter

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) // Assurez-vous que l'objet qui entre en collision est le joueur
        {
            TimeSlowdown playerTimeSlowdown = other.gameObject.GetComponent<TimeSlowdown>();
            Debug.Log(playerTimeSlowdown);

            if (playerTimeSlowdown != null)
            {
                Debug.Log("Se rentre0");
                playerTimeSlowdown.AddSlowdowns(slowdownsToAdd);
                Destroy(gameObject); // D�truisez l'objet ramass�
            }
        }
    }
}