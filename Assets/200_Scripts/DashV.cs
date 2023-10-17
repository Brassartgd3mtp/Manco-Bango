using UnityEngine;
using System.Collections;

public class DashingPlayer : MonoBehaviour
{
    public float dashSpeed = 10.0f;
    public float dashDuration = 0.5f;
    private bool isDashing = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing) // Utilisation de la touche "Shift" pour le dash
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;

        // Récupérer la direction de déplacement actuelle du joueur
        Vector3 dashDirection = transform.forward;

        // Utiliser un Raycast pour détecter les collisions avant de commencer le dash
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dashDirection, out hit, 1.0f))
        {
            // Si une collision est détectée, ne pas effectuer le dash
            isDashing = false;
            yield break;
        }

        float startTime = Time.time;

        while (Time.time - startTime < dashDuration)
        {
            // Déplacer le joueur dans la direction du dash
            transform.position += dashDirection * dashSpeed * Time.deltaTime;

            // Utiliser un Raycast continu pour vérifier les collisions pendant le dash
            if (Physics.Raycast(transform.position, dashDirection, out hit, 1.0f))
            {
                // Si une collision est détectée, arrêter le dash
                isDashing = false;
                yield break;
            }

            yield return null;
        }

        isDashing = false;
    }
}
