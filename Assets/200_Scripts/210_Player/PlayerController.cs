using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    private CheckpointManager checkpointManager;
	private CheckpointManager checkpointManager;
    public CheckpointManager checkpointManager;
    private HealthManager healthManager;

    [Header("Movement")]
    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private CapsuleCollider capsuleCollider;
    [SerializeField] private Transform orientation;
    private float moveSpeed = 10;
    private float airMultiplier = 1;
    private float groundDrag = 5;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 8;
    [SerializeField] private float jumpCooldown = 0.25f;
    private bool readyToJump;

    [Header("Coyotte")]
    [SerializeField] private float maxCoyotteTime = 0.25f;
    private bool canCoyotte;
    
    public bool CanCoyotte
    {
        get { return canCoyotte; }
        private set
        {
            if (grounded && readyToJump) canCoyotte = true;
            else canCoyotte = value;
        }
    }

    [Header("Ground Check")]
    [SerializeField] private LayerMask whatIsGround;
    public static float playerHeight = 2;
    private bool grounded;

    [HideInInspector] public float horizontalInput;
    [HideInInspector] public float verticalInput;

    [HideInInspector] public static Vector3 moveDirection;

    [HideInInspector] public static Rigidbody rb;

    public Animator animator;
    public Animator CamAnimator;

    private void Start()
    {
        checkpointManager = FindObjectOfType<CheckpointManager>(); // Recherchez le gestionnaire de checkpoints dans la scène

        healthManager = GetComponent<HealthManager>();

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        CanCoyotte = true;
    }

    private void Update()
    {
        if (healthManager != null)
        {
            // Accédez à la variable health depuis le script HealthManager
            float playerHealth = healthManager.health;
            // Vous pouvez maintenant utiliser playerHealth dans ce script
        }


        if (Input.GetKeyDown(KeyCode.F5))
        {
            //Je revient au dernier checkpoint
            checkpointManager.ReturnToCheckpoint();
        }

        //Je fait un Raycast qui part de mon personnage et qui va en direction du sol pour détecter s'il est en contact avec le sol
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.01f, whatIsGround);

        //Je modifie l'attraction si le joueur est en l'air
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0; //Permet d'avoir un déplacement aérien agréable

        GetInput();
        SpeedControl();


        //Je change mon animation de marche en fonction de si je suis au sol ou non
        if (moveDirection.x != 0 && moveDirection.y <= 1.1)
        {
            animator.SetBool("IsMoving", true);
            CamAnimator.SetBool("IsMoving", true);
        }
        
        else
        {
            animator.SetBool("IsMoving", false);
            CamAnimator.SetBool("IsMoving", false);
        
        }
        
        if (moveDirection.y >=1.1 )
        {
            animator.SetBool("IsMoving", false);
            CamAnimator.SetBool("IsMoving", false);
        
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private Coroutine _coyotteCoroutine;
    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump") && readyToJump && (grounded || CanCoyotte))
        {
            readyToJump = false;

            CanCoyotte = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);

            return;
        }

        if (!grounded)
            _coyotteCoroutine = StartCoroutine(CoyotteLimit());

        else if
            (_coyotteCoroutine is not null) StopCoroutine(_coyotteCoroutine);
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //Je vérifie si le personnage est en contact avec un mur dans la direction de déplacement
        bool isTouchingWall = Physics.BoxCast(transform.position, capsuleCollider.bounds.extents, moveDirection, out RaycastHit hitInfo, transform.rotation, 1.5f, whatIsWall);

        //Si le personnage est au sol, je dépalce le joueur et j'ignore l'adhérence avec un mur
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
        else //J'ajoute la force en l'air en tenant compte de l'adhérence au mur
        {
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
        //Je limite la vélocité max (sur l'axe X et Z) du joueur
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

    private IEnumerator CoyotteLimit()
    {
        yield return new WaitForSeconds(maxCoyotteTime);
        CanCoyotte = false;
    }
}