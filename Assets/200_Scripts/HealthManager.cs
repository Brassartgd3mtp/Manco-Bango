using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthManager : MonoBehaviour

    
{
    public float health = 75f;

    public float maxHealth = 100f;

    public Image healthBarImage;

    public TextMeshProUGUI healthText; 


    // Update is called once per frame
    void Update()
    {
        healthBarImage.fillAmount = health / maxHealth;
        healthText.text = health + " / " + maxHealth; 

        health = Mathf.Clamp(health, 0f, maxHealth);

    }

    public void DamageButton(int damageamount) //permet de faire des degats
    {
        health -= damageamount;

    }

    public void HealButton(int damageamount) // permet de donné des PV
    {
        health += damageamount;
    }
}
