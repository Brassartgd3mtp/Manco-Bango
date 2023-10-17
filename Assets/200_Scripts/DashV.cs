using UnityEngine;
using System.Collections;

public class DashingPlayer : MonoBehaviour
{
    public float dashSpeed = 10.0f;
    public float dashDuration = 0.5f;
    private bool isDashing = false;
    public PlayerController playerController;

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

        // Utiliser le script du contrôleur du joueur pour obtenir la direction de déplacement
        Vector3 dashDirection = playerController.moveDirection;
        Debug.Log(playerController.moveDirection);
        float startTime = Time.time;

        while (Time.time - startTime < dashDuration)
        {
            // Déplacer le joueur dans la direction du dash
            transform.position += dashDirection * dashSpeed * Time.deltaTime;

            yield return null;
        }

        isDashing = false;
    }
}
