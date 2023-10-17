using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator; // Variable publique pour l'Animator
    private PlayerController playerController; // Assurez-vous que le script PlayerController est correctement attaché

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (animator == null)
        {
            Debug.LogError("L'Animator n'a pas été assigné.");
            return;
        }

        // Vérifie si le joueur est en mouvement
        bool isMoving = playerController.horizontalInput != 0 || playerController.verticalInput != 0;

        // Active ou désactive l'animation en fonction de l'état de mouvement
        animator.SetBool("IsMoving", isMoving);
    }
}
