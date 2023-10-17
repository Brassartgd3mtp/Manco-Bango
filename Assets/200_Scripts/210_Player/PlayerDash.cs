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
                Dash();
                timer = 1;
            }
        }
        else timer -= Time.deltaTime;
    }

    private void Dash()
    {
        canDash = false; //Je désactive le dash pendant la recharge

        //Vector3 forceToApply = orientation.forward * dashForce + orientation.up * dashUpward;

        playerController.moveDirection.y = 0;

        Vector3 velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        rb.AddForce(playerController.moveDirection * dashForce + velocity, ForceMode.Impulse);

        Invoke("ResetDash", dashCooldown + dashDuration);
    }

    void ResetDash()
    {
        canDash = true;
        //fovEffect.
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
