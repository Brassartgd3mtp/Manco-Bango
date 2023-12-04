using System.Collections;
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
    public bool canPressEscape = true;
    public bool isGameOver = false;
    public int damageamount;
    public Canvas[] canvas;
    [SerializeField] private CheckpointManager checkpointManager;

    void Update()
    {
        if (!isGameOver)
        {
            healthBarImage.fillAmount = health / maxHealth;
            healthText.text = health + " / " + maxHealth;

            if (health <= 0)
            {
                foreach (Canvas canva in canvas)
                {
                    canva.gameObject.SetActive(false);
                }

                health = 0;

                Vector3 _deathPos = CheckpointManager.checkpoint.position;
                transform.position = _deathPos;

                gameOverPanel.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                canPressEscape = false;

                isGameOver = true; // Marquez le jeu comme �tant en cours de jeu termin�

                Rigidbody _rb = GetComponent<Rigidbody>();
                _rb.velocity = Vector3.zero;
                Time.timeScale = 0f;
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("FloorKill"))
        {
            ApplyDamage(100);
        }

        if (collision.gameObject.CompareTag("Heal"))
        {
            if (health != maxHealth)
            {
                ApplyHeal(10);
                Destroy(collision.gameObject);
            }
        }
    }

    public void ApplyDamage(int damageAmount)
    {
        if (!isGameOver)
        {
            health -= damageAmount;
            Debug.Log("Health reduced by " + damageAmount + ". Current health: " + health);
        }
    }

    public void ApplyHeal(int damageAmount)
    {
        if (!isGameOver)
        {
            health += damageAmount;
        }
    }
}
