using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator; // Variable publique pour l'Animator

    private void Update()
    {
        if (animator == null)
        {
            Debug.LogError("L'Animator n'a pas été assigné.");
            return;
        }

        // Vérifie si le joueur est en mouvement
        bool isMoving = PlayerController.horizontalInput != 0 || PlayerController.verticalInput != 0;

        // Active ou désactive l'animation en fonction de l'état de mouvement
        animator.SetBool("IsMoving", isMoving);
    }
}
