using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CheckpointManager checkpointManager;

    [Header("Movement")]
    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private CapsuleCollider capsuleCollider;
    public Transform Orientation;
    public float moveSpeed = 10;
    public float airMultiplier = 1;
    public float groundDrag = 5;
    //[SerializeField] private int fallSpeedModifier = 5; (voir ligne 151)

    [Header("Jump")]
    public float jumpForce = 8;
    public float jumpCooldown = 0.25f;
    private bool readyToJump;

    [Header("Coyotte")]
    public float maxCoyotteTime = 0.25f;
    private bool canCoyotte;
    
    public bool CanCoyotte
    {
        get => canCoyotte;
        private set
        {
            if (grounded && readyToJump) canCoyotte = true;
            else canCoyotte = value;
        }
    }

    [Header("Ground Check")]
    [SerializeField] private LayerMask whatIsGround;
    private bool grounded;
    private bool jumpGrounded;
    public static float playerHeight = 2;

    [HideInInspector] public static float horizontalInput;
    [HideInInspector] public static float verticalInput;

    [HideInInspector] public static Vector3 moveDirection;
    [HideInInspector] public static Rigidbody rb;

    [HideInInspector] public PlayerDash playerDash;
    [HideInInspector] public PlayerSlide playerSlide;

    private void Start()
    {
        playerSlide = GetComponent<PlayerSlide>();
        playerDash = GetComponent<PlayerDash>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        CanCoyotte = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            //Je reviens au dernier checkpoint
            checkpointManager.ReturnToCheckpoint(gameObject.transform);
        }

        //Je fais un Raycast qui part de mon personnage et qui va en direction du sol pour détecter s'il est en contact avec le sol
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        //Et un deuxième me permettant de sauter un peu avant de toucher le sol
        jumpGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.6f, whatIsGround);

        //Je modifie l'attraction si le joueur est en l'air
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0; //Permet d'avoir un déplacement aérien agréable

        JumpAction();
        SpeedControl();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private Coroutine _coyotteCoroutine;
    private void JumpAction()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump") && readyToJump && (jumpGrounded || CanCoyotte))
        {
            readyToJump = false;

            CanCoyotte = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);

            return;
        }

        if (!grounded)
            _coyotteCoroutine = StartCoroutine(CoyotteLimit()); //Je lance mon timer durant lequel j'ai le droit de coyotte

        else if (_coyotteCoroutine is not null)
            StopCoroutine(_coyotteCoroutine); //Si je rentre en contact avec le sol, j'arrête de force ma coroutine
    }

    private void MovePlayer()
    {
        moveDirection = Orientation.forward * verticalInput + Orientation.right * horizontalInput;

        //Je stop le joueur dès qu'il lâche ses inputs (seulement s'il est au sol)
        if (horizontalInput == 0 && verticalInput == 0 && grounded && !PlayerSlide.sliding)
            rb.velocity = new Vector3(0, rb.velocity.y, 0);

        //Je vérifie si le personnage est en contact avec un mur dans la direction vers laquelle is se déplace
        bool isTouchingWall = Physics.BoxCast(transform.position, capsuleCollider.bounds.extents, moveDirection, out RaycastHit hitInfo, transform.rotation, 1f, whatIsWall);

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

            //J'augmente la vitesse de chute du joueur au moment où il commence à chuter (potentiellemnt une feature du style groundpound)
            //if (!jumpGrounded && rb.velocity.y < 0 && rb.velocity.y > -fallSpeedModifier + 1) rb.velocity = new Vector3(rb.velocity.x, -fallSpeedModifier, rb.velocity.z);
        }
    }

    private void SpeedControl()
    {
        //Je limite la vélocité max sur l'axe X et Z du joueur
        if (rb.velocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelHor = rb.velocity.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVelHor.x, rb.velocity.y, limitedVelHor.z);
        }

        //Je limite la vélocité max sur l'axe Y du joueur
        if (rb.velocity.y > jumpForce * 10)
        {
            Vector3 limitedVelVer = rb.velocity.normalized * jumpForce * 10;
            rb.velocity = new Vector3(rb.velocity.x,limitedVelVer.y, rb.velocity.z);
        }

        if (rb.velocity.y < -jumpForce * 10)
        {
            Vector3 limitedVelVer = rb.velocity.normalized * -jumpForce * 10;
            rb.velocity = new Vector3(rb.velocity.x, -limitedVelVer.y, rb.velocity.z);
        }

        //J'utilise la détéction du sol de grounded pour annuler la vélocité Y afin d'éviter de se bloquer dans le sol lorsque l'on chute
        if (grounded && rb.velocity.y < 0 && !playerSlide.OnSlope())
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
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