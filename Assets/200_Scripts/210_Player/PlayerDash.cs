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
    [SerializeField] private Camera fovEffect; // Référence à la caméra du joueur
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

        playerController.moveDirection.y = 0;

        rb.AddForce(playerController.moveDirection * dashForce, ForceMode.Impulse);

        yield return new WaitForSeconds(dashDuration);

        //J'attends la période de recharge
        yield return new WaitForSeconds(dashCooldown);

        canDash = true; //Je réactive le dash
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
