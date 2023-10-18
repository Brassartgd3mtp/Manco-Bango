using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.PackageManager;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10;
    public float airMultiplier = 1;
    public float groundDrag = 5;
    [SerializeField] LayerMask whatIsWall;
    [SerializeField] CapsuleCollider capsuleCollider;
    [SerializeField] private Transform orientation;

    [Header("Jump")]
    public float jumpForce = 8;
    public float jumpCooldown = 0.25f;
    private bool readyToJump;
    [SerializeField] private float coyotteTimer = 0.25f;
    [SerializeField] private float maxCoyotteTime = 0.25f;

    [Header("Ground Check")]
    [SerializeField] private float playerHeight = 2;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private bool grounded;

    [HideInInspector] public float horizontalInput;
    [HideInInspector] public float verticalInput;

    [HideInInspector] public Vector3 moveDirection;

    private Rigidbody rb;

    //public Animator animator;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;


        readyToJump = true;
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

        //if (moveDirection.x != 0 && rb.velocity.y <= 0)
        //{
        //    animator.SetBool("IsMoving", true);
        //    Debug.Log("Move");
        //}
        //
        //else
        //{
        //    animator.SetBool("IsMoving", false);
        //    Debug.Log("Move pas");
        //
        //}
        //
        //
        //if (moveDirection.y != 0)
        //{
        //    animator.SetBool("IsMoving", false);
        //    Debug.Log("Jump");
        //
        //}
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump") && readyToJump && (grounded || coyotteTimer > 0))
        {
            readyToJump = false;
            
            Jump();
            
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if (grounded)
        {
            coyotteTimer = maxCoyotteTime;
        }

        else if (coyotteTimer > 0)
        {
            coyotteTimer -= Time.deltaTime;
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // Je vérifie si le personnage est en contact avec un mur dans la direction de déplacement
        bool isTouchingWall = Physics.BoxCast(transform.position, capsuleCollider.bounds.extents, moveDirection, out RaycastHit hitInfo, transform.rotation, 1.5f, whatIsWall);

        // Si le personnage est au sol, ajoutez la force de déplacement
        if (grounded)
        {
            if (!isTouchingWall)
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            }
            else
            {
                Vector3 moveDirectionWithoutWall = Vector3.ProjectOnPlane(moveDirection, hitInfo.normal).normalized;
                rb.AddForce(moveDirectionWithoutWall * moveSpeed * 10f, ForceMode.Force);
            }
        }
        else
        {
            // Ajoutez la force en l'air en tenant compte de l'adhérence au mur
            if (!isTouchingWall)
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
            }
            else
            {
                Vector3 moveDirectionWithoutWall = Vector3.ProjectOnPlane(moveDirection, hitInfo.normal).normalized;
                rb.AddForce(moveDirectionWithoutWall * moveSpeed * 10f * airMultiplier, ForceMode.Force);
            }
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
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}