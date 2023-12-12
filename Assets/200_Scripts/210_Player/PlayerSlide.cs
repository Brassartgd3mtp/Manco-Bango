using System.Collections;
using UnityEngine;

public class PlayerSlide : MonoBehaviour
{
    #region Variables
    [Header("Slide")]
    public float maxSlideTime;
    public float slideForce;
    public float slideYScale;
    private float slideTimer;
    private float startYScale;
    public static bool Sliding;

    [Header("FOV")]
    public int fovModifier;
    [SerializeField] private float maxFOVTimer;
    [SerializeField] private float FOVTimer;
    [SerializeField] private Camera fovEffect;
    private float baseFOV;

    [Header("Slope Slide")]
    [SerializeField] private float maxSlopeAngle;

    private PlayerDash dash;
    #endregion

    private void Start()
    {
        startYScale = gameObject.transform.localScale.y;

        maxFOVTimer = maxSlideTime / 2f;
        FOVTimer = maxFOVTimer;

        baseFOV = fovEffect.fieldOfView;

        dash = GetComponent<PlayerDash>();
    }

    void Update()
    {
        if (PlayerController.moveDirection != new Vector3(0, 0, 0) && !dash.isDashing)
        {
            if (Input.GetButtonDown("Slide") && !Sliding)
                StartSlide();

            if (OnSlope())
            {
                if (!Input.GetButtonDown("Jump"))
                    PlayerController.rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }

        }
    }

    private void FixedUpdate()
    {
        if (Sliding)
            SlidingLogic();
    }


    /// <summary>
    /// Fonctionnement du Slide
    /// 
    /// StartSlide :
    /// Je stock dans une variable la direction dans laquelle va le joueur.
    /// Je récupère son GameObject et je baisse sa taille.
    /// Je set le timer à sa durée max.
    /// </summary>
    Vector3 _direction;
    private void StartSlide()
    {
        SlideSound(volume: 1.0f);

        _direction = new Vector3(PlayerController.moveDirection.x, 0, PlayerController.moveDirection.z);
        Sliding = true;

        Transform playerObj = gameObject.transform;
        playerObj.localScale = new Vector3(playerObj.localScale.x, slideYScale, playerObj.localScale.z);
        PlayerController.rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

        slideTimer = maxSlideTime;
    }

    /// <summary>
    /// Fonctionnement du Slide
    /// 
    /// Sliding :
    /// Il fonctionne en update.
    /// Je regarde si je suis sur une slope (dans ce cas je pousse mon joueur vers le bas)
    /// Je réduit la durée du slide, dès qu'elle atteint 0 (et si je ne suis pas sur une slope), j'arrête mon slide.
    /// J'augmente mon FOV pour donner un effet de vitesse.
    /// Le timer du FOV sert à limiter la valeur max du FOV quand elle augmente.
    /// </summary>
    private void SlidingLogic()
    {
        if (!OnSlope())
            PlayerController.rb.AddForce(_direction * slideForce, ForceMode.Force);
        else
        {
            _direction = new Vector3(_direction.x, -1, _direction.z);
            PlayerController.rb.AddForce(_direction * slideForce, ForceMode.Force);
        }

        slideTimer -= Time.deltaTime;

        if (slideTimer <= 0 && !OnSlope())
            StopSlide();

        if (FOVTimer > 0)
        {
            FOVTimer -= Time.deltaTime;
            fovEffect.fieldOfView += fovModifier * Time.deltaTime;
        }
    }

    /// <summary>
    /// Fonctionnement du Slide
    /// 
    /// StopSlide :
    /// Je reset la taille de mon personnage.
    /// Je réduit le FOV pour le remettre à sa valeur d'origine.
    /// Je reset le timer du FOV.
    /// </summary>
    private void StopSlide()
    {
        SlideSound(0.0f);
        float baseSlideForce = slideForce;

        Sliding = false;

        Transform playerObj = gameObject.transform;
        playerObj.localScale = new Vector3(playerObj.localScale.x, startYScale, playerObj.localScale.z);

        slideForce = baseSlideForce;

        if (!OnSlope())
            StartCoroutine(ReduceFOV());

        FOVTimer = maxFOVTimer;
    }

    private IEnumerator ReduceFOV()
    {
        while (fovEffect.fieldOfView > baseFOV)
        {
            fovEffect.fieldOfView -= fovModifier * Time.deltaTime * 2;
            yield return null;
        }

        fovEffect.fieldOfView = baseFOV;
        yield break;
    }

    public bool OnSlope()
    {
        RaycastHit slopeHit;
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, PlayerController.playerHeight * 0.5f + 0.2f))
        {
            if (slopeHit.collider.gameObject.layer == 12)
            {
                float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
                return angle < maxSlopeAngle && angle != 0;
            }
        }
        return false;
    }

    public void SlideSound(float volume = 1.0f)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;
        AudioManager.Instance.PlaySound(19, audioSource);
    }
}