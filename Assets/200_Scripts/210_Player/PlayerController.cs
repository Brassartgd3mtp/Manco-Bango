using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10;
    public float airMultiplier = 1;
    public float groundDrag = 5;

    [Header("Jump")]
    public float jumpForce = 8;
    public float jumpCooldown = 0.25f;
    private bool readyToJump;

    [Header("Slide")]
    public float slideSpeed = 1;
    public float slideCooldown = 1;
    private bool readyToSlide;

    [Header("Ground Check")]
    [SerializeField] private float playerHeight = 2;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform orientation;
    private bool grounded;

    [HideInInspector] public float horizontalInput;
    [HideInInspector] public float verticalInput;

    private Vector3 moveDirection;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
        readyToSlide = true;
    }

    private void Update()
    {
        //Je fait un Raycast qui part de mon personnage et qui va en direction du sol pour d�tecter s'il est en contact avec le sol
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        GetInput();
        SpeedControl();

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump") && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if (Input.GetButtonDown("Slide") && readyToSlide)
        {
            readyToSlide = false;

            Slide();

            Invoke(nameof(ResetSlide), slideCooldown);
        }
    }

    private void MovePlayer()
    {
        //Je cr�e des vecteurs de d�placement s�par�s pour l'horizontal (x) et le vertical (z)
        Vector3 horizontalMovement = orientation.right * horizontalInput;
        Vector3 verticalMovement = orientation.forward * verticalInput;

        //Je v�rifie si le personnage est en contact avec un mur dans la direction de chaque d�placement
        bool isTouchingWallHorizontal = Physics.Raycast(transform.position, horizontalMovement, 1.0f);
        bool isTouchingWallVertical = Physics.Raycast(transform.position, verticalMovement, 1.0f);

        //J'ajoute la force de d�placement seulement si le personnage n'est pas en contact avec un mur dans cette direction
        if (grounded && !isTouchingWallHorizontal)
        {
            rb.AddForce(horizontalMovement.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded && !isTouchingWallHorizontal)
        {
            rb.AddForce(horizontalMovement.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }

        if (grounded && !isTouchingWallVertical)
        {
            rb.AddForce(verticalMovement.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded && !isTouchingWallVertical)
        {
            rb.AddForce(verticalMovement.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        //Limite la v�locit� max du joueur
        if (rb.velocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelHor = rb.velocity.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVelHor.x, rb.velocity.y, limitedVelHor.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        if(transform.position.y == 2) rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void Slide()
    {
        rb.AddForce(transform.forward * slideSpeed, ForceMode.Impulse);
        //while (slideSpeed > 1)
        //{
        //    rb.velocity -= Vector3.forward * slideSpeed * Time.deltaTime;
        //}
    }

    private void ResetSlide()
    {
        readyToSlide = true;
    }
}