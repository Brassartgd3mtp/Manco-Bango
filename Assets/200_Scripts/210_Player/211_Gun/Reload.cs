using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Reload : MonoBehaviour
{
    [Header("Reload")]
    [SerializeField] private float reloadTimeSet = 0.15f;
    [SerializeField] private float reloadTimeCountdown;

    [Header("References")]
    [SerializeField] private RectTransform selector;
    [SerializeField] private CanvasToggle canvasToggle;

    private Barrel barrel;

    private void Start()
    {
        barrel = gameObject.GetComponent<Barrel>();
        reloadTimeCountdown = reloadTimeSet;
    }

    void Update()
    {
        if (!canvasToggle.isGamePaused)
        {
            if (reloadTimeCountdown > 0) reloadTimeCountdown -= Time.deltaTime;

            //Quand je recharge (et si le chargeur n'est pas déjà plein), je crée un balle, je lui change sa couleur et je l'ajoute au Stock
            if (Input.GetButtonDown("Reload") && barrel.barrelStock[Barrel.SelectedForReload] == Color.black && reloadTimeCountdown <= 0)
            {
                if (selector.rotation.eulerAngles.z <= 180 && selector.rotation.eulerAngles.z > 0)
                {
                    barrel.AddStock(Color.blue);
                }
                else /*if (selector.rotation.eulerAngles.z <= 0 && selector.rotation.eulerAngles.z > -180)*/
                {
                    barrel.AddStock(Color.red);
                }
                reloadTimeCountdown = reloadTimeSet;
            }
        }
    }
}
