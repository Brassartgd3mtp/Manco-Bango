using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDash : MonoBehaviour
{
    #region Variables
    [Header("Dash")]
    public float dashForce = 10.0f;
    public float dashCooldown = 1.0f;
    [SerializeField] private float cooldownTimer;
    public float dashDuration = 0.5f;
    private Vector3 direction;

    [Header("References")]
    [SerializeField] private Image DashBarImage;
    [SerializeField] private Transform orientation;
    [SerializeField] private Camera fovEffect;
    [SerializeField] private ParticleManager particleManager;
    [SerializeField] private GameObject dashParticle;

    private bool canDash = true;
    [HideInInspector] public bool isDashing = false;
    #endregion

    private void Start()
    {
        cooldownTimer = dashCooldown + dashDuration;
    }

    private void Update()
    {
        DashBarImage.fillAmount = cooldownTimer;

        if (cooldownTimer <= 0)
        {
            if (PlayerController.moveDirection != Vector3.zero && canDash && !PlayerSlide.Sliding && Input.GetButtonDown("Dash"))
            {
                DashSound();

                direction = PlayerController.moveDirection;

                StartCoroutine(Dash());
                particleManager.Dash(dashDuration);

                Invoke("ResetDash", dashCooldown + dashDuration);
                cooldownTimer = dashCooldown + dashDuration;
            }
        }
        else
            cooldownTimer -= Time.deltaTime;
    }

    private IEnumerator Dash()
    {
        //Je vérifie si le joueur peut dasher et s'il n'est pas déjà en train de dasher
        if (!canDash || isDashing) yield break;

        isDashing = true;
        canDash = false;
        float dashTimer = 0f;

        Vector3 velocityLock = direction * dashForce;

        //Je positionne mon effet de particule en fonction des inputs de déplacement de mon joueur
        DashEffectDirection();

        //J'effectue mon dash
        while (dashTimer < dashDuration)
        {
            PlayerController.rb.velocity = new Vector3(PlayerController.rb.velocity.x, 0, PlayerController.rb.velocity.z);

            PlayerController.rb.AddForce(velocityLock, ForceMode.Force);
            dashTimer += Time.deltaTime;

            yield return null;
        }

        isDashing = false;
    }

    private void ResetDash()
    {
        canDash = true;
    }

    /// <summary>
    /// J'ai une classe qui contient des Propriétés nommées de type Vector3.
    /// Ce sont des positions et des rotations.
    /// 
    /// Je fais un switch prenant tous les cas de figure des  déplacements de mon joueur afin de positionner le ParticleSystem du dash.
    /// </summary>
    private void DashEffectDirection()
    {
        switch (PlayerController.verticalInput)
        {
            case 1:
                switch (PlayerController.horizontalInput)
                {
                    case 1:
                        dashParticle.transform.localPosition = DashEffectPos.FrontRightPos;
                        dashParticle.transform.localRotation = Quaternion.Euler(DashEffectPos.FrontRightRot);
                        particleManager.DashRate(15);
                        break;

                    case -1:
                        dashParticle.transform.localPosition = DashEffectPos.FrontLeftPos;
                        dashParticle.transform.localRotation = Quaternion.Euler(DashEffectPos.FrontLeftRot);
                        particleManager.DashRate(15);
                        break;

                    default:
                        dashParticle.transform.localPosition = DashEffectPos.FrontPos;
                        dashParticle.transform.localRotation = Quaternion.Euler(DashEffectPos.FrontRot);
                        particleManager.DashRate(15);
                        break;
                }
                break;

            case -1:
                switch (PlayerController.horizontalInput)
                {
                    case 1:
                        dashParticle.transform.localPosition = DashEffectPos.BackRightPos;
                        dashParticle.transform.localRotation = Quaternion.Euler(DashEffectPos.BackRightRot);
                        particleManager.DashRate(30);
                        break;

                    case -1:
                        dashParticle.transform.localPosition = DashEffectPos.BackLeftPos;
                        dashParticle.transform.localRotation = Quaternion.Euler(DashEffectPos.BackLeftRot);
                        particleManager.DashRate(30);
                        break;

                    default:
                        dashParticle.transform.localPosition = DashEffectPos.BackPos;
                        dashParticle.transform.localRotation = Quaternion.Euler(DashEffectPos.BackRot);
                        particleManager.DashRate(30);
                        break;
                }
                break;

            default:
                switch (PlayerController.horizontalInput)
                {
                    case 1:
                        dashParticle.transform.localPosition = DashEffectPos.RightPos;
                        dashParticle.transform.localRotation = Quaternion.Euler(DashEffectPos.RightRot);
                        particleManager.DashRate(30);
                        break;

                    case -1:
                        dashParticle.transform.localPosition = DashEffectPos.LeftPos;
                        dashParticle.transform.localRotation = Quaternion.Euler(DashEffectPos.LeftRot);
                        particleManager.DashRate(30);
                        break;

                    default:
                        Debug.Log("Player is not moving !");
                        particleManager.DashRate(15);
                        break;
                }
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Si le joueur entre en collision avec un mu pendant le dash, j'arrête le dash
        if (collision.gameObject.tag == "Wall")
        {
            canDash = true;
            PlayerController.rb.velocity = Vector3.zero;
        }
    }
    public void DashSound()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioManager.Instance.PlaySound(0, audioSource);
    }


}
