using System.Runtime.CompilerServices;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

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

    private void Review(bool _isDefault, float _movespeed, float _airmult, float _groundDrag, float _jumpForce, float _jumpCooldown, float _maxCoyotteTime)
    {
        if (_isDefault)
        {
            reviewText[0].text =
                $"Move Speed = {_movespeed}" +
                $"\nAir Speed Multiplier = {_airmult}" +
                $"\nGround Attraction = {_groundDrag}" +
                $"\nJump Force = {_jumpForce}" +
                $"\nJump Cooldown = {_jumpCooldown}" +
                $"\nMax Coyotte Time = {_maxCoyotteTime}";
        }

        else
        {
            reviewText[0].text =
                $"Move Speed = <color=\"blue\">10</color> -> <color=\"green\">{_movespeed}</color>" +
                $"\nAir Speed Multiplier = <color=\"blue\">1</color> -> <color=\"green\">{_airmult}</color>" +
                $"\nGround Attraction = <color=\"blue\">5</color> -> <color=\"green\">{_groundDrag}</color>" +
                $"\nJump Force = <color=\"blue\">10</color> -> <color=\"green\">{_jumpForce}</color>" +
                $"\nJump Cooldown = <color=\"blue\">0.25</color> -> <color=\"green\">{_jumpCooldown}</color>" +
                $"\nMax Coyotte Time = <color=\"blue\">1</color> -> <color=\"green\">{_maxCoyotteTime}</color>";
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
            playerController.moveSpeed = 10;
            playerController.airMultiplier = 1;
            playerController.groundDrag = 5;
        #endregion
        #region Jump
            playerController.jumpForce = 10;
            playerController.jumpCooldown = .25f;
        #endregion
        #region Coyotte
            playerController.maxCoyotteTime = 1;
        #endregion

        CanvaDisable(presetsCanvas[0]);
        CanvaEnable(reviewCanvas[0]);
        Review(true, playerController.moveSpeed, playerController.airMultiplier, playerController.groundDrag, playerController.jumpForce, playerController.jumpCooldown, playerController.maxCoyotteTime);
    }
    public void CPreset1()
    {
        #region Movement
            playerController.moveSpeed = 15;
            playerController.airMultiplier = .4f;
            playerController.groundDrag = 5;
        #endregion
        #region Jump
            playerController.jumpForce = 12;
            playerController.jumpCooldown = .25f;
        #endregion
        #region Coyotte
            playerController.maxCoyotteTime = .5f;
        #endregion

        CanvaDisable(presetsCanvas[0]);
        CanvaEnable(reviewCanvas[0]);
        Review(false, playerController.moveSpeed, playerController.airMultiplier, playerController.groundDrag, playerController.jumpForce, playerController.jumpCooldown, playerController.maxCoyotteTime);
    }

    public void DDefault()
    {
        playerDash.dashForce = 200;
        playerDash.dashCooldown = 1;
        playerDash.dashDuration = .5f;

        CanvaDisable(presetsCanvas[1]);
        CanvaEnable(reviewCanvas[1]);
        Review(true, playerDash.dashForce, playerDash.dashCooldown, playerDash.dashDuration);
    }
    public void DPreset1()
    {
        playerDash.dashForce = 800;
        playerDash.dashCooldown = 1;
        playerDash.dashDuration = .25f;

        CanvaDisable(presetsCanvas[1]);
        CanvaEnable(reviewCanvas[1]);
        Review(false, playerDash.dashForce, playerDash.dashCooldown, playerDash.dashDuration);
    }
}
