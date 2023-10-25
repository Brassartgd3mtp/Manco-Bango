using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlide : MonoBehaviour
{
    [Header("Slide")]
    [SerializeField] private float maxSlideTime;
    [SerializeField] private float slideForce;
    [SerializeField] private float slideYScale;
    private float slideTimer;
    private float startYScale;
    private bool sliding;

    [Header("FOV")]
    [SerializeField] private int fovModifier;
    [SerializeField] private float maxFOVTimer;
    [SerializeField] private float FOVTimer;
    [SerializeField] private Camera fovEffect;
    private float baseFOV;

    [Header("Slope Slide")]
    [SerializeField] private float maxSlopeAngle;

    private void Start()
    {
        startYScale = gameObject.transform.localScale.y;

        maxFOVTimer = maxSlideTime / 2f;
        FOVTimer = maxFOVTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.moveDirection != new Vector3(0, 0, 0))
        {
            if (Input.GetButtonDown("Slide"))
            {
                StartSlide();
            }
            else if (Input.GetButtonUp("Slide"))
            {
                StopSlide();
            }

            if (OnSlope())
            {
                if (!Input.GetButtonDown("Jump"))
                    PlayerController.rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }

        }
    }

    private void FixedUpdate()
    {
        if (sliding)
            Sliding();
    }

    private void StartSlide()
    {
        sliding = true;
        
        Transform playerObj = gameObject.transform;
        playerObj.localScale = new Vector3(playerObj.localScale.x, slideYScale, playerObj.localScale.z);
        PlayerController.rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        
        slideTimer = maxSlideTime;
        
        baseFOV = fovEffect.fieldOfView;
    }

    private void Sliding()
    {
        PlayerController.rb.AddForce(PlayerController.moveDirection * slideForce, ForceMode.Force);
        
        slideTimer -= Time.deltaTime;
        
        if (slideTimer <= 0 && !OnSlope())
            StopSlide();
        
        if (FOVTimer > 0)
        {
            FOVTimer -= Time.deltaTime;
            fovEffect.fieldOfView += fovModifier * Time.deltaTime;
        }
        
        else if (fovEffect.fieldOfView != baseFOV)
            fovEffect.fieldOfView -= fovModifier * Time.deltaTime;
    }

    private void StopSlide()
    {
        sliding = false;

        Transform playerObj = gameObject.transform;

        playerObj.localScale = new Vector3(playerObj.localScale.x, startYScale, playerObj.localScale.z);

        slideForce = 200;

        fovEffect.fieldOfView = baseFOV;

        FOVTimer = maxFOVTimer;
    }

    public bool OnSlope()
    {
        RaycastHit slopeHit;
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, PlayerController.playerHeight * 0.5f + 0.01f))
        {
            if (slopeHit.collider.gameObject.layer == 12)
            {
                float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
                return angle < maxSlopeAngle && angle != 0;
            }
        }
        return false;
    }
}
