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
    public GameObject gameOverPanel;
    public bool Escape = false;
    private bool isGameOver = false;
    public int damageamount;
    [SerializeField] private CheckpointManager checkpointManager;

    void Update()
    {
        if (!isGameOver)
        {
            healthBarImage.fillAmount = health / maxHealth;
            healthText.text = health + " / " + maxHealth;

            if (health <= 0)
            {
                // Sauvegardez la santé actuelle
                health = CheckpointManager.SavedHealth;
                // Téléportez le joueur au dernier checkpoint
                checkpointManager.ReturnToCheckpoint(gameObject.transform);
                // Désactivez d'autres fonctionnalités, par exemple le panneau de Game Over
                //gameOverPanel.SetActive(true);
                //Escape = true;
                //Time.timeScale = 0f;
                //Cursor.visible = true;
                //Cursor.lockState = CursorLockMode.None;
                // isGameOver = true; // Marquez le jeu comme étant en cours de jeu terminé
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(DamageDelay(10));
        }

        if (collision.gameObject.CompareTag("FloorKill"))
        {
            DamageButton(100);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("FloorKill"))
        {
            DamageButton(100);
        }

        if (collision.gameObject.CompareTag("Heal"))
        {
            if (health != maxHealth)
            {
                HealButton(10);
                Destroy(collision.gameObject);
            }
        }
    }

    IEnumerator DamageDelay(int damageAmount)
    {
        Debug.Log("Waiting for damage delay...");

        yield return new WaitForSeconds(1.5f); // Délai avant d'infliger des dégâts

        if (!isGameOver)
        {
            Debug.Log("Applying damage after delay...");
            DamageButton(damageAmount);
        }
    }

    public void DamageButton(int damageAmount)
    {
        if (!isGameOver)
        {
            health -= damageAmount;
            Debug.Log("Health reduced by " + damageAmount + ". Current health: " + health);
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            return;
        }
    }
}
