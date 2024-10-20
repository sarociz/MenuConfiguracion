using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    // Referencia al Input Action Asset
    public CustomControls customControls;
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
        // Habilitar el mapa de entrada del jugador
        customControls.PlayerInput.Enable();
        customControls.PlayerInput.Movement.performed += OnMove;
        customControls.PlayerInput.Movement.canceled += OnMove;
        customControls.PlayerInput.Jump.performed += OnJump;

        // Habilitar el mapa de entrada para pausa
        customControls.PauseInput.Enable();
        customControls.PauseInput.Pause.performed += pauseMenu.OnPause;
    }

    private void OnDisable()
    {
        // Deshabilitar el mapa de entrada del jugador
        customControls.PlayerInput.Movement.performed -= OnMove;
        customControls.PlayerInput.Movement.canceled -= OnMove;
        customControls.PlayerInput.Jump.performed -= OnJump;
        customControls.PlayerInput.Disable();

        // Deshabilitar el mapa de entrada de pausa
        customControls.PauseInput.Pause.performed -= pauseMenu.OnPause;
        customControls.PauseInput.Disable();
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
        // Saltar si est� en el suelo
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

    // Funci�n para actualizar la velocidad desde el slider
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed; // Actualiza la velocidad del jugador
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SetJumpForce(float newJumpForce)
    {
        jumpForce = Mathf.Clamp(newJumpForce, 0f, 20f); // Puedes ajustar los l�mites de la fuerza de salto aqu�
    }

    public float GetJumpForce()
    {
        return jumpForce;
    }
}
