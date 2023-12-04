using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CheckpointManager checkpointManager;

    [Header("Movement")]
    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private CapsuleCollider capsuleCollider;
    public Transform Orientation;
    public float MoveSpeed = 10;
    public float AirMultiplier = 1;
    public float GroundDrag = 5;
    public bool AbruptWalk = false;
    //[SerializeField] private int fallSpeedModifier = 5; (voir ligne 183)

    [Header("Jump")]
    public float JumpForce = 8;
    public float JumpCooldown = 0.25f;
    public bool readyToJump;

    [Header("Coyotte")]
    public float MaxCoyotteTime = 0.25f;
    private bool canCoyotte;

    private bool isWalking = false;
    public float repeatInterval = 2f;

    public bool CanCoyotte
    {
        get => canCoyotte;
        private set
        {
            if (grounded && readyToJump) canCoyotte = true;
            else canCoyotte = value;
        }
    }
    public bool coyotteShow;

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
        
        InvokeRepeating("PlayWalkSound", 0f, repeatInterval);
    }

    private void Update()
    {
        coyotteShow = CanCoyotte;
        if (Input.GetKeyDown(KeyCode.F5))
        {
            //Je reviens au dernier checkpoint
            checkpointManager.ReturnToCheckpoint();
        }

        if (Input.GetKeyDown(KeyCode.F8))
        {
            SceneManager.LoadScene(1);
        }

        //Je fais un Raycast qui part de mon personnage et qui va en direction du sol pour détecter s'il est en contact avec le sol
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        //Et un deuxième me permettant de sauter un peu avant de toucher le sol
        jumpGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.6f, whatIsGround);

        //Je modifie l'attraction si le joueur est en l'air
        if (grounded)
            rb.drag = GroundDrag;
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

            Invoke(nameof(ResetJump), JumpCooldown);

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


        isWalking = moveDirection.x > 0 || moveDirection.z > 0;


        //Je stop le joueur dès qu'il lâche ses inputs (seulement s'il est au sol)
        if (AbruptWalk && horizontalInput == 0 && verticalInput == 0 && grounded && !PlayerSlide.sliding)
            rb.velocity = new Vector3(0, rb.velocity.y, 0);

        //Je vérifie si le personnage est en contact avec un mur dans la direction vers laquelle is se déplace
        bool isTouchingWall = Physics.BoxCast(transform.position, capsuleCollider.bounds.extents, moveDirection, out RaycastHit hitInfo, transform.rotation, 1f, whatIsWall);



        //Si le personnage est au sol, je dépalce le joueur et j'ignore l'adhérence avec un mur
        if (grounded)
        {
            

            if (!isTouchingWall)
            {
                rb.AddForce(moveDirection.normalized * MoveSpeed * 10f, ForceMode.Force);
            }
            else
            {

                Vector3 moveDirectionWithoutWall = Vector3.ProjectOnPlane(moveDirection, hitInfo.normal).normalized;
                rb.AddForce(moveDirectionWithoutWall * MoveSpeed * 10f, ForceMode.Force);
            }
        }
        else //J'ajoute la force en l'air en tenant compte de l'adhérence au mur
        {
            isWalking = false;

            if (!isTouchingWall)
            {
                rb.AddForce(moveDirection.normalized * MoveSpeed * 10f * AirMultiplier, ForceMode.Force);
            }
            else
            {
                Vector3 moveDirectionWithoutWall = Vector3.ProjectOnPlane(moveDirection, hitInfo.normal).normalized;
                rb.AddForce(moveDirectionWithoutWall * MoveSpeed * 10f * AirMultiplier, ForceMode.Force);
            }

            //J'augmente la vitesse de chute du joueur au moment où il commence à chuter (potentiellemnt une feature du style groundpound)
            //if (!jumpGrounded && rb.velocity.y < 0 && rb.velocity.y > -fallSpeedModifier + 1) rb.velocity = new Vector3(rb.velocity.x, -fallSpeedModifier, rb.velocity.z);
        }
    }

    private void SpeedControl()
    {
        //Je limite la vélocité max sur l'axe X et Z du joueur
        if (rb.velocity.magnitude > MoveSpeed)
        {
            Vector3 limitedVelHor = rb.velocity.normalized * MoveSpeed;
            rb.velocity = new Vector3(limitedVelHor.x, rb.velocity.y, limitedVelHor.z);
        }

        //Je limite la vélocité max sur l'axe Y du joueur
        if (rb.velocity.y > JumpForce * 10)
        {
            Vector3 limitedVelVer = rb.velocity.normalized * JumpForce * 10;
            rb.velocity = new Vector3(rb.velocity.x,limitedVelVer.y, rb.velocity.z);
        }

        if (rb.velocity.y < -JumpForce * 10)
        {
            Vector3 limitedVelVer = rb.velocity.normalized * -JumpForce * 10;
            rb.velocity = new Vector3(rb.velocity.x, -limitedVelVer.y, rb.velocity.z);
        }

        //J'utilise la détéction du sol de grounded pour annuler la vélocité Y afin d'éviter de se bloquer dans le sol lorsque l'on chute
        if (grounded && rb.velocity.y < 0 && !playerSlide.OnSlope())
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
    }

    private void Jump()
    {
        JumpSound();

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * JumpForce, ForceMode.Impulse);


    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private IEnumerator CoyotteLimit()
    {
        yield return new WaitForSeconds(MaxCoyotteTime);
        CanCoyotte = false;
    }

    public void JumpSound()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioManager.Instance.PlaySound(9, audioSource);
    }

    public void WalkSound()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioManager.Instance.PlaySound(7, audioSource);
    }



    private void PlayWalkSound()
    {
        if (isWalking)
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            AudioManager.Instance.PlaySound(7, audioSource);
        }
    }

    public void SetNewPosition(Transform _newPos)
    {
        transform.position = _newPos.position;
    }
}