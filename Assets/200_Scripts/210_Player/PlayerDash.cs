using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerDash : MonoBehaviour
{
    public float dashForce = 10.0f; // Force du dash
    public float dashDuration = 0.2f; // Durée du dash
    public float dashCooldown = 1.0f; // Temps de recharge entre les dashes
    public float timer;
    private bool canDash = true; // Indicateur permettant de savoir si le joueur peut effectuer un dash
    private Rigidbody rb; // Référence au Rigidbody du joueur
    [SerializeField] private Camera playerCamera; // Référence à la caméra du joueur
    public Image DashBarImage;

    private void Start()
    {
        timer = dashCooldown;
        rb = GetComponent<Rigidbody>();
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
        canDash = false; // Désactivez le dash pendant la recharge

        // Obtenez la direction de la caméra en convertissant l'angle de la caméra en direction
        Vector3 dashDirection = playerCamera.transform.forward;

        dashDirection.y = 0; // Ignorez la composante verticale (y)

        // Appliquez une force au Rigidbody pour le dash
        rb.AddForce(dashDirection.normalized * dashForce, ForceMode.Impulse);

        yield return new WaitForSeconds(dashDuration);

        // Attendez la période de recharge
        yield return new WaitForSeconds(dashCooldown);

        canDash = true; // Réactivez le dash
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Si le joueur entre en collision avec un objet pendant le dash, arrêtez le dash
        if (collision.gameObject.tag == "Wall")
        {
            canDash = true;
            rb.velocity = Vector3.zero; // Arrêtez le mouvement du joueur
        }
    }
}