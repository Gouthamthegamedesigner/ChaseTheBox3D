using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField]
    private float playerSpeed = 2.0f;

    private float gravityValue = -9.81f;
    private Player playerInput;

    private void Awake()
    {
        playerInput = new Player();
        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer)
        {
            playerVelocity.y = 0f; // Reset vertical velocity when grounded
        }
        else
        {
            playerVelocity.y += gravityValue * Time.deltaTime; // Apply gravity when not grounded
        }

        // Get movement input
        Vector2 movementInput = playerInput.PlayerMain.Move.ReadValue<Vector2>();
        Vector3 move = new Vector3(movementInput.x, 0f, movementInput.y);

        // Apply movement
        controller.Move(move * Time.deltaTime * playerSpeed);

        // Rotate player to face movement direction
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Apply gravity
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
