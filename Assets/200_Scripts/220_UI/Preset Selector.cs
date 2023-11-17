using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PresetSelector : MonoBehaviour
{
    private PlayerController playerController;
    [Space]
    private PlayerDash playerDash;
    public Canvas[] presetsCanvas = new Canvas[2];
    public Canvas[] otherCanvas;
    [Space]
    public SceneFader sceneFader;

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
    }

    private void Update()
    {
        if (!presetsCanvas[0].enabled && !presetsCanvas[1].enabled)
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

    private void CanvaDisable(Canvas _canva)
    {
        _canva.enabled = false;
    }

    public void CPreset1()
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
    }
    public void CPreset2()
    {
        #region Movement
            playerController.moveSpeed = 15;
            playerController.airMultiplier = .4f;
            playerController.groundDrag = 5;
        #endregion
        #region Jump
            playerController.jumpForce = 10;
            playerController.jumpCooldown = .25f;
        #endregion
        #region Coyotte
            playerController.maxCoyotteTime = .5f;
        #endregion

        CanvaDisable(presetsCanvas[0]);
    }

    public void DPreset1()
    {
        playerDash.dashForce = 200;
        playerDash.dashCooldown = 1;
        playerDash.dashDuration = .5f;

        CanvaDisable(presetsCanvas[1]);
    }
    public void DPreset2()
    {
        playerDash.dashForce = 800;
        playerDash.dashCooldown = 1;
        playerDash.dashDuration = .25f;

        CanvaDisable(presetsCanvas[1]);
    }
}
