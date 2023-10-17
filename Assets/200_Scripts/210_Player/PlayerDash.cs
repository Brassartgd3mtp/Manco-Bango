using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class PlayerDash : MonoBehaviour
{
    public float dashForce = 10.0f; // Force du dash
    //public float dashUpward = 0.0f; // Force du dash
    public float dashDuration = 0.2f; // Durée du dash
    public float dashCooldown = 1.0f; // Temps de recharge entre les dashes
    public float timer;
    public Image DashBarImage;
    [SerializeField] private Transform orientation;
    [SerializeField] private Camera fovEffect; // Référence à la caméra du joueur

    private bool canDash = true; // Indicateur permettant de savoir si le joueur peut effectuer un dash
    private Rigidbody rb; // Référence au Rigidbody du joueur
    private PlayerController playerController;

    private void Start()
    {
        timer = dashCooldown; 
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        DashBarImage.fillAmount = timer;
        if (timer <= 0)
        {
            if (canDash && Input.GetKeyDown(KeyCode.LeftShift)) // Changez la touche selon vos préférences
            {
                StartCoroutine(Dash());
                timer = 1;
            }
        }
        else timer -= Time.deltaTime;
    }

    private bool isDashing = false; // Ajoutez une variable pour savoir si le joueur est en train de dasher

    private IEnumerator Dash()
    {
        if (!canDash || isDashing) yield break; // Vérifiez si le dash est possible et si le joueur n'est pas déjà en train de dasher

        isDashing = true; // Le joueur commence à dasher
        float dashTimer = 0f;

        Vector3 initialVelocity = rb.velocity; // Vitesse actuelle du joueur

        while (dashTimer < dashDuration)
        {
            float t = dashTimer / dashDuration;
            // Interpolation linéaire entre la vitesse actuelle et la vitesse de dash
            Vector3 dashVelocity = Vector3.Lerp(initialVelocity * dashForce, playerController.moveDirection, t);

            rb.velocity = new Vector3(dashVelocity.x, rb.velocity.y, dashVelocity.z);
            dashTimer += Time.deltaTime;
            // Attendre la prochaine frame
            yield return null;
        }

        isDashing = false; // Le dash est terminé
        Invoke("ResetDash", dashCooldown); // Réactive le dash après le temps de recharge
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
            rb.velocity = Vector3.zero;
        }
    }
}
