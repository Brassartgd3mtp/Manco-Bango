using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public float interactRange = 10f;
    [SerializeField] private Camera fpCam;
    [SerializeField] private Barrel barrel;
    public static List<GameObject> RedDoors = new List<GameObject>();
    public static List<GameObject> BlueDoors = new List<GameObject>();

    private void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Le joueur tire
        {
            Ray ray = fpCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if(barrel.barrelStock.Count > 0)
            {
                if (Physics.Raycast(ray, out hit, interactRange))
                {
                    if (barrel.barrelStock[0] == Color.blue)
                    {
                        if (hit.collider.CompareTag("BlueButton"))
                        {
                            // Le joueur a tiré sur un objet bleu, désactivez les portes bleues et réactiver les portes rouges
                            foreach (GameObject door in RedDoors)
                            {
                                door.SetActive(true);
                            }
                            foreach (GameObject door in BlueDoors)
                            {
                                door.SetActive(false);
                            }
                        }
                        else if (hit.collider.CompareTag("Destroyable") && hit.collider.gameObject.layer == 8)
                        {
                            Destroy(hit.collider.gameObject);
                        }
                    }
                    else if (barrel.barrelStock[0] == Color.red)
                    {
                        if (hit.collider.CompareTag("RedButton"))
                        {
                            // Le joueur a tiré sur un objet rouge, désactivez les portes rouges et réactiver les portes bleues
                            foreach (GameObject door in RedDoors)
                            {
                                door.SetActive(false);
                            }
                            foreach (GameObject door in BlueDoors)
                            {
                                door.SetActive(true);
                            }
                        }
                        else if (hit.collider.CompareTag("Destroyable") && hit.collider.gameObject.layer == 9)
                        {
                            Destroy(hit.collider.gameObject);
                        }
                    }
                }
            }
        }
    }

    private void OnDestroy()
    {
        RedDoors.Clear();
        BlueDoors.Clear();
    }
}
