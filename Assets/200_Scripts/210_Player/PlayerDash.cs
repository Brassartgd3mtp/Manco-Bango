using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class PlayerDash : MonoBehaviour
{
    public float dashForce = 10.0f; // Force du dash
    public float dashDuration = 0.2f; // Durée du dash
    public float dashCooldown = 1.0f; // Temps de recharge entre les dashes
    public float timer;
    private bool canDash = true; // Indicateur permettant de savoir si le joueur peut effectuer un dash
    private Rigidbody rb; // Référence au Rigidbody du joueur
    [SerializeField] private Camera playerCamera; // Référence à la caméra du joueur
    private PlayerController playerController;
    public Image DashBarImage;

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
            if (canDash)

            {
                if (Input.GetKeyDown(KeyCode.LeftShift)) // Changez la touche selon vos préférences
                {
                    StartCoroutine(Dash());
                    timer = 1; 
                }
            }
             
        }
        else timer -= Time.deltaTime;
    }

    private IEnumerator Dash()
    {
        canDash = false; //Je désactive le dash pendant la recharge

        Vector3 dashDirection = Vector3.zero;

        if (playerController.verticalInput > 0)
        {
            dashDirection = playerCamera.transform.forward;
        }
        else if (playerController.verticalInput < 0)
        {
            dashDirection = -playerCamera.transform.forward;
        }

        if (playerController.horizontalInput > 0)
        {
            dashDirection = playerCamera.transform.right;
        }
        else if (playerController.horizontalInput < 0)
        {
            dashDirection = -playerCamera.transform.right;
        }

        //Je réinitialise la valeur Y de la direction pour éviter de modifier la hauteur du dash
        dashDirection.y = 0;

        rb.AddForce(dashDirection.normalized * dashForce, ForceMode.Impulse);

        yield return new WaitForSeconds(dashDuration);

        //J'attends la période de recharge
        yield return new WaitForSeconds(dashCooldown);

        canDash = true; // Réactivez le dash
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Si le joueur entre en collision avec un objet pendant le dash, arrêtez le dash
        if (collision.gameObject.tag == "Wall")
        {
            canDash = true;
            rb.velocity = Vector3.zero; //Je stop le joueur
        }
    }
}
