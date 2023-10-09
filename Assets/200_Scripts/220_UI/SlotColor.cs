using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotColor : MonoBehaviour
{
    public List<Image> slots = new List<Image>(6);

    [SerializeField] private Barrel barrel;

    //J'utilise un dictionnaire pour stocker la correspondance entre les emplacements et les couleurs
    private Dictionary<Image, Color> slotColors = new Dictionary<Image, Color>();

    void Start()
    {
        foreach (Image slot in slots)
        {
            slot.color = Color.black;

            //J'ajoute l'emplacement et la couleur par défaut dans le dictionnaire
            slotColors[slot] = Color.black;
        }
    }

    void Update()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (barrel.barrelStock.Count > i && barrel.barrelStock[i] != null)
            {
                // Je met à jour la couleur de l'emplacement
                slotColors[slots[i]] = barrel.barrelStock[i];
            }
            else
            {
                // je check si l'index n'existe plus dans barrel.barrelStock, alors je le met en noir
                slotColors[slots[i]] = Color.black;
            }

            // J'applique la couleur à l'emplacement
            slots[i].color = slotColors[slots[i]];
        }
    }
}