using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlide : MonoBehaviour
{
    [SerializeField] private float maxSlideTime;
    [SerializeField] private float slideForce;
    [SerializeField] private float slideYScale;

    [SerializeField] private int fovModifier;
    private float slideTimer;
    private float startYScale;
    private float baseFOV;
    private bool sliding;

    [SerializeField] private Camera fovEffect;
    private PlayerController playerController;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();

        startYScale = gameObject.transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Slide"))
        {
            StartSlide();
        }
        else if (Input.GetButtonUp("Slide"))
        {
            StopSlide();
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
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

        slideTimer = maxSlideTime;

        baseFOV = fovEffect.fieldOfView;
    }

    private void Sliding()
    {
        rb.AddForce(playerController.moveDirection * slideForce, ForceMode.Force);

        slideTimer -= Time.deltaTime;

        fovEffect.fieldOfView += fovModifier * Time.deltaTime;

        if (slideTimer <= 0)
            StopSlide();
    }

    private void StopSlide()
    {
        sliding = false;

        Transform playerObj = gameObject.transform;
        playerObj.localScale = new Vector3(playerObj.localScale.x, startYScale, playerObj.localScale.z);

        slideForce = 200;

        fovEffect.fieldOfView = baseFOV;
    }
}
