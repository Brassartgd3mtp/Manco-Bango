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

    private bool isGameOver = false;

    void Update()
    {
        if (!isGameOver)
        {
            healthBarImage.fillAmount = health / maxHealth;
            healthText.text = health + " / " + maxHealth;

            health = Mathf.Clamp(health, 0f, maxHealth);

            if (health <= 0)
            {
                // La santé est tombée à 0, mettez le jeu en pause et affichez le panneau Game Over
                PauseGame();
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

    void PauseGame()
    {
        Time.timeScale = 0f; // Mettez le temps à zéro pour mettre le jeu en pause
        isGameOver = true;

        // Affichez le panneau Game Over
        gameOverPanel.SetActive(true);
    }

    // Vous pouvez ajouter une méthode pour reprendre le jeu si nécessaire
    public void ResumeGame()
    {
        Time.timeScale = 1f; // Rétablissez le temps normal pour reprendre le jeu
        isGameOver = false;

        // Désactivez le panneau Game Over
        gameOverPanel.SetActive(false);
    }
}