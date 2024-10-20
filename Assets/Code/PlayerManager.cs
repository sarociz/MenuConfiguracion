using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    // Referencia al Input Action Asset
    private CustomControls customControls;
    public PauseMenu pauseMenu;

    private Vector2 moveInput;
    private Rigidbody rb;
    public float speed = 5f;
    public float jumpForce = 5f;
    private bool isGrounded;

    private void Awake()
    {
        // Inicializar el Input Action Asset
        customControls = new CustomControls();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        // Activar el Action Map y registrar las acciones
        customControls.PlayerInput.Enable();
        customControls.PlayerInput.Movement.performed += OnMove;
        customControls.PlayerInput.Movement.canceled += OnMove;
        customControls.PlayerInput.Jump.performed += OnJump;
        // Activar el action map del emnu de pausa
        customControls.PauseInput.Enable();
        customControls.PauseInput.Pause.performed += pauseMenu.OnPause;
    }

    private void OnDisable()
    {
        // Desactivar el Action Map y desregistrar las acciones
        customControls.PlayerInput.Movement.performed -= OnMove;
        customControls.PlayerInput.Movement.canceled -= OnMove;
        customControls.PlayerInput.Jump.performed -= OnJump;
        customControls.PlayerInput.Disable();

        customControls.PauseInput.Disable();
        customControls.PauseInput.Pause.performed -= pauseMenu.OnPause;
    }


    private void FixedUpdate()
    {
        // Movimiento horizontal
        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y) * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        // Capturar el input de movimiento
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        // Saltar si está en el suelo
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
