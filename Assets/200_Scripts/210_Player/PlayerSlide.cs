using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlide : MonoBehaviour
{
    [SerializeField] private float maxSlideTime;
    [SerializeField] private float slideForce;
    [SerializeField] private float slideYScale;

    [SerializeField] private int fovModifier;
    [SerializeField] private float maxFOVTimer;
    [SerializeField ]private float FOVTimer;
    private float baseFOV;

    private float slideTimer;
    private float startYScale;
    private bool sliding;

    [SerializeField] private Camera fovEffect;
    private PlayerController playerController;
    private Rigidbody rb;

    private bool onSlope;
    [SerializeField] private double downwardSlidingStart;
    [SerializeField] private double downwardSlidingEnd;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();

        startYScale = gameObject.transform.localScale.y;

        maxFOVTimer = maxSlideTime / 2f;
        FOVTimer = maxFOVTimer;
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
            StopCoroutine(DownwardSlope());
            StopSlide();
        }

        if (onSlope && downwardSlidingStart < downwardSlidingEnd)
            StopSlide();
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
        if (onSlope) StartCoroutine(DownwardSlope());

        if (FOVTimer > 0) FOVTimer -= Time.deltaTime;

        rb.AddForce(playerController.moveDirection * slideForce, ForceMode.Force);

        slideTimer -= Time.deltaTime;

        if (slideTimer <= 0 && !onSlope)
        {
            StopCoroutine(DownwardSlope());
            StopSlide();
        }

        if (FOVTimer > 0)
            fovEffect.fieldOfView += fovModifier * Time.deltaTime;

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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 12) onSlope = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 12) onSlope = false;
    }

    private IEnumerator DownwardSlope()
    {
        downwardSlidingStart = gameObject.transform.position.y;

        yield return new WaitForSeconds(0.001f);

        downwardSlidingEnd = gameObject.transform.position.y;
    }
}
