using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10;

    public float groundDrag = 5;

    public float jumpForce = 8;
    public float jumpCooldown = 0.25f;
    public float airMultiplier = 1;
    private bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    [SerializeField] private float playerHeight = 2;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform orientation;
    private bool grounded;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;

    private Rigidbody rb;

    [SerializeField] private AudioSource movementSound;
    [SerializeField] private AudioSource jumpSound;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        // Obtenez les composants AudioSource de l'objet joueur
        movementSound = GetComponentInChildren<AudioSource>();
    
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

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            // Jouez le son de saut
            if (jumpSound != null)
            {
                jumpSound.Play();
            }

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        // Movement sounds
        if (horizontalInput != 0f || verticalInput != 0f)
        {
            // Jouez le son de mouvement
            if (movementSound != null && !movementSound.isPlaying)
            {
                movementSound.Play();
            }
        }
        else
        {
            // Arr�tez le son de mouvement lorsque le joueur ne bouge pas
            if (movementSound != null)
            {
                movementSound.Stop();
            }
        }
    }

    private void MovePlayer()
    {
        //Je calcul la direction de mon mouvement
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //Je l'applique quand je suis au sol
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        //Et aussi quand je suis en l'air, avec un modifieur (airMultiplier)
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
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
}