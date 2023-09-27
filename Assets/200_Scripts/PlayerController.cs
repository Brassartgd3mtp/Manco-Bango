using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputManager inputManager;
    private InputAction movement;
    [SerializeField] private int walkSpeed;
    private Rigidbody rb;


    private void Awake()
    {
        inputManager = new InputManager();
    }

    private void OnEnable()
    {
        if (rb == null)
            rb = this.GetComponent<Rigidbody>();

        movement = inputManager.Player.Move;
        movement.Enable();

        //inputManager.Player.Reload.started += Reload();
        //inputManager.Player.Reload.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
        //inputManager.Player.Reload.Disable();
    }

    private void FixedUpdate()
    {
        Debug.Log($"Movement Values {movement.ReadValue<Vector2>()}");
    }
}
