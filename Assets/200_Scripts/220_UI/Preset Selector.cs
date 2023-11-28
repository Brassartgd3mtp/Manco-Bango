using TMPro;
using UnityEngine;

public class PresetSelector : MonoBehaviour
{
    public Canvas[] presetsCanvas = new Canvas[2];
    [Space]
    public Canvas[] reviewCanvas = new Canvas[2];
    public TextMeshProUGUI[] reviewText = new TextMeshProUGUI[2];
    [Space]
    public Canvas[] otherCanvas;
    [Space]
    public SceneFader sceneFader;
    [Space]
    public Camera PlayerCam;
    public Transform GunTransform;

    private PlayerController playerController;
    private PlayerDash playerDash;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        playerDash = FindObjectOfType<PlayerDash>();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    private void Start()
    {
        foreach(Canvas other in otherCanvas)
        {
            other.gameObject.SetActive(false);
        }

        foreach (Canvas review in reviewCanvas)
        {
            review.enabled = false;
        }
    }

    private void Update()
    {
        if (!presetsCanvas[0].enabled && !presetsCanvas[1].enabled && !reviewCanvas[0].enabled && !reviewCanvas[1].enabled)
        {
            foreach (Canvas other in otherCanvas)
            {
                other.gameObject.SetActive(true);
            }

            Time.timeScale = 1.00f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            sceneFader.FuncFadeIn();

            gameObject.SetActive(false);
        }
    }

    private void CanvaEnable(Canvas _canva)
    {
        _canva.enabled = true;
    }

    private void CanvaDisable(Canvas _canva)
    {
        _canva.enabled = false;
    }

    private void Review(bool _isDefault, float _dashForce, float _dashCooldown, float _dashDuration)
    {
        if (_isDefault)
        {
            reviewText[1].text =
                $"Dash Force = {_dashForce}" +
                $"\nDash Cooldown = {_dashCooldown}" +
                $"\nDash Duration = {_dashDuration}";
        }

        else
        {
            reviewText[1].text =
                $"Dash Force = <color=\"blue\">200</color> -> <color=\"green\">{_dashForce}</color>" +
                $"\nDash Cooldown = <color=\"blue\">1</color> -> <color=\"green\">{_dashCooldown}</color>" +
                $"\nDash Duration = <color=\"blue\">0.5</color> -> <color=\"green\">{_dashDuration}</color>";
        }
    }

    private void Review(bool _isDefault, float _movespeed, float _airmult, float _groundDrag, float _jumpForce, float _jumpCooldown, float _maxCoyotteTime, bool _isAbrupt)
    {
        if (_isDefault)
        {
            reviewText[0].text =
                $"Move Speed = {_movespeed}" +
                $"\nAir Speed Multiplier = {_airmult}" +
                $"\nGround Attraction = {_groundDrag}" +
                $"\nJump Force = {_jumpForce}" +
                $"\nJump Cooldown = {_jumpCooldown}" +
                $"\nMax Coyotte Time = {_maxCoyotteTime}" +
                $"\nAbrupt Walk = {_isAbrupt}";
        }

        else
        {
            reviewText[0].text =
                $"Move Speed = <color=\"blue\">10</color> -> <color=\"green\">{_movespeed}</color>" +
                $"\nAir Speed Multiplier = <color=\"blue\">1</color> -> <color=\"green\">{_airmult}</color>" +
                $"\nGround Attraction = <color=\"blue\">5</color> -> <color=\"green\">{_groundDrag}</color>" +
                $"\nJump Force = <color=\"blue\">10</color> -> <color=\"green\">{_jumpForce}</color>" +
                $"\nJump Cooldown = <color=\"blue\">0.25</color> -> <color=\"green\">{_jumpCooldown}</color>" +
                $"\nMax Coyotte Time = <color=\"blue\">0.5f</color> -> <color=\"green\">{_maxCoyotteTime}</color>" +
                $"\nAbrupt Walk = <color=\"blue\">true</color> -> <color=\"green\">{_isAbrupt}</color>";
        }
    }

    public void Cancel(int _index)
    {
        CanvaDisable(reviewCanvas[_index]);
        CanvaEnable(presetsCanvas[_index]);
    }

    public void Validate(int _index)
    {
        CanvaDisable(reviewCanvas[_index]);
    }

    public void CDefault()
    {
        #region Movement
            playerController.MoveSpeed = 15;
            playerController.AirMultiplier = 1f;
            playerController.GroundDrag = 5;
        #endregion
        #region Jump
            playerController.JumpForce = 12;
            playerController.JumpCooldown = .25f;
        #endregion
        #region Coyotte
            playerController.MaxCoyotteTime = 1f;
        #endregion
        playerController.AbruptWalk = true;

        CanvaDisable(presetsCanvas[0]);
        CanvaEnable(reviewCanvas[0]);
        Review(true, playerController.MoveSpeed, playerController.AirMultiplier, playerController.GroundDrag, playerController.JumpForce, playerController.JumpCooldown, playerController.MaxCoyotteTime, playerController.AbruptWalk);
    }

    public void CPreset1()
    {
        #region Movement
            playerController.MoveSpeed = 18;
            playerController.AirMultiplier = .5f;
            playerController.GroundDrag = 5;
        #endregion
        #region Jump
            playerController.JumpForce = 8;
            playerController.JumpCooldown = .25f;
        #endregion
        #region Coyotte
            playerController.MaxCoyotteTime = 0.5f;
        #endregion
            playerController.AbruptWalk = false;

        CanvaDisable(presetsCanvas[0]);
        CanvaEnable(reviewCanvas[0]);
        Review(false, playerController.MoveSpeed, playerController.AirMultiplier, playerController.GroundDrag, playerController.JumpForce, playerController.JumpCooldown, playerController.MaxCoyotteTime, playerController.AbruptWalk);
    }

    public void DDefault()
    {
        playerDash.dashForce = 800;
        playerDash.dashCooldown = 1;
        playerDash.dashDuration = .25f;

        CanvaDisable(presetsCanvas[1]);
        CanvaEnable(reviewCanvas[1]);
        Review(true, playerDash.dashForce, playerDash.dashCooldown, playerDash.dashDuration);
    }
}
