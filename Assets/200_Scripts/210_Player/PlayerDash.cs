using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class PlayerDash : MonoBehaviour
{
    [Header("Dash")]
    [SerializeField] private float dashForce = 10.0f; // Force du dash
    [SerializeField] private float dashCooldown = 1.0f; // Temps de recharge entre les dashes
    [SerializeField] private float cooldownTimer;
    public float dashDuration = 0.5f; // Durée du dash
    private Vector3 direction;

    [Header("References")]
    [SerializeField] private Image DashBarImage;
    [SerializeField] private Transform orientation;
    [SerializeField] private Camera fovEffect;
    [SerializeField] private ParticleManager particleManager;

    private bool canDash = true; // Indicateur permettant de savoir si le joueur peut effectuer un dash
    private bool isDashing = false; // Ajoutez une variable pour savoir si le joueur est en train de dasher

    private void Start()
    {
        cooldownTimer = dashCooldown + dashDuration;
    }

    private void Update()
    {
        DashBarImage.fillAmount = cooldownTimer;

        if (cooldownTimer <= 0)
        {
            if (PlayerController.moveDirection != new Vector3(0, 0, 0))
            {
                if (canDash && Input.GetButtonDown("Dash")) // Changez la touche selon vos préférences
                {
                    direction = PlayerController.moveDirection;

                    StartCoroutine(Dash());
                    particleManager.Dash(dashDuration);

                    Invoke("ResetDash", dashCooldown + dashDuration); // Réactive le dash après le temps de recharge
                    cooldownTimer = dashCooldown + dashDuration;
                }
            }
        }
        else
            cooldownTimer -= Time.deltaTime;
    }


    private IEnumerator Dash()
    {
        if (!canDash || isDashing) yield break; // Vérifiez si le dash est possible et si le joueur n'est pas déjà en train de dasher

        isDashing = true; // Le joueur commence à dasher
        canDash = false;
        float dashTimer = 0f;

        while (dashTimer < dashDuration)
        {
            PlayerController.rb.velocity = new Vector3(PlayerController.rb.velocity.x, 0, PlayerController.rb.velocity.z);

            PlayerController.rb.AddForce(direction * dashForce, ForceMode.Force);
            dashTimer += Time.deltaTime;

            // Attendre la prochaine frame
            yield return null;
        }

        isDashing = false; // Le dash est terminé
    }

    // Ajoutez cette méthode pour réinitialiser le dash
    private void ResetDash()
    {
        canDash = true; // Réactive le dash
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Si le joueur entre en collision avec un objet pendant le dash, j'arrête le dash
        if (collision.gameObject.tag == "Wall")
        {
            canDash = true;
            PlayerController.rb.velocity = Vector3.zero;
        }
    }
}
