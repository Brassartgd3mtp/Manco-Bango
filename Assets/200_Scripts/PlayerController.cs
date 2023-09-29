using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpForce;

    private CharacterController characterController;

    private Vector3 inputVector; 
    private Vector3 movementVector;
    private float gravity = -10f;
    [SerializeField] private bool isGrounded = true;

    private Rigidbody rb;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        GetInput(); //Récupère les inputs
        MovePlayer(); //Déplace le joueurs
        if (Input.GetButtonDown("Jump") && isGrounded) Jump(); //isGrounded = false;

    }
    private void GetInput()
    {
        inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")); //Converti en Vector3 l'input Horizontal et Vertical
        inputVector.Normalize(); //Permet un mouvement circulaire plutôt que linéaire
        inputVector = transform.TransformDirection(inputVector);

        movementVector = (inputVector * movementSpeed) + (Vector3.up * gravity);
    }

    private void MovePlayer()
    {
        characterController.Move(movementVector * Time.deltaTime);
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground")) isGrounded = true;
    }
    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) isGrounded = false;
    }
}
