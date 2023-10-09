using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Reload : MonoBehaviour
{
    [SerializeField] private RectTransform selector;

    [SerializeField] private float reloadTimeSet = 0.15f;
    [SerializeField] private float reloadTimeCountdown;

    [SerializeField] private ParticleManager particleManager;

    private Barrel barrel;

    private void Start()
    {
        particleManager = gameObject.GetComponent<PlayerGun>().particleManager;
        barrel = gameObject.GetComponent<Barrel>();
        reloadTimeCountdown = reloadTimeSet;
    }

    void Update()
    {
        if (reloadTimeCountdown > 0) reloadTimeCountdown -= Time.deltaTime;

        if (Input.GetButtonDown("Reload") && barrel.barrelStock.Count < 6 && reloadTimeCountdown <=0) //Quand je recharge (et si le chargeur n'est pas déjà plein), je crée un balle, je lui change sa couleur et je l'ajoute au Stock
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
        particleManager.CanonColor.Play();
    }
}
