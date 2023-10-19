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
    public GameObject gameOverPanel; // Référence au panneau Game Over
    public bool Escape = false;
    private bool isGameOver = false;

    public int damageamount ;




    void Update()
    {

        if (!isGameOver)
        {
            healthBarImage.fillAmount = health / maxHealth;
            healthText.text = health + " / " + maxHealth;

            health = Mathf.Clamp(health, 0f, maxHealth);

            if (health <= 0)
            {
                // La santé est tombée à 0, désactivez la touche "Echap"
                DisableEscapeKey();
                gameOverPanel.SetActive(true);
                Escape = true;
                Time.timeScale = 0f;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            DamageButton(10); // Vous pouvez ajuster la valeur de dégâts comme nécessaire.
        }

      


    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Heal"))
        {
            if (health != maxHealth)
            {
                HealButton(10); // Vous pouvez ajuster la valeur de dégâts comme nécessaire.
                Destroy(collision.gameObject);

            }

        }
    }




    public void DamageButton(int damageAmount)
    {
        if (!isGameOver)
        {
            health -= damageAmount;
        }
    }

    public void HealButton(int damageAmount)
    {
        if (!isGameOver)
        {
            health += damageAmount;
        }
    }

    void DisableEscapeKey()
    {
        // Désactivez la touche "Echap" en désactivant le composant Input.GetKey(KeyCode.Escape)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            return;
        }
    }
}
