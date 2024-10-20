using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    // public TextMeshProUGUI startText;
    private bool isPaused = false;

    public Slider speedSlider;
    public PlayerManager player;

    private void Awake()
    {
        // Desactivar el menú de pausa al inicio
        pauseMenu.SetActive(false);

        // Asegurarse de que el valor inicial del slider coincida con la velocidad del jugador
        speedSlider.value = player.GetSpeed();

        speedSlider.onValueChanged.AddListener(SetPlayerSpeed);

    }

    // Función para reanudar el juego
    public void ResumeGame()
    {
        pauseMenu.SetActive(false); // Ocultar el menú de pausa
        Time.timeScale = 1f; // Reanudar el tiempo
        isPaused = false;
    }
   
    public void PauseGame()
    {
        pauseMenu.SetActive(true); // Mostrar el menú de pausa
        Time.timeScale = 0f; // Pausar el tiempo
        isPaused = true;

        speedSlider.value = player.GetSpeed(); // Actualizar el valor del slider a la velocidad actual del jugador
        speedSlider.value = player.GetJumpForce(); 
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    // Cambiar velocidad player
    public void SetPlayerSpeed(float valor)
    {
        player.SetSpeed(valor);
        player.SetJumpForce(valor); 

    }

    // Función para salir del juego
    public void ExitGame()
    {
        #if UNITY_EDITOR
                // Si estamos en el editor, simplemente parar la ejecución
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                        // Si es una build, salir del juego
                        Application.Quit();
        #endif
    }

}
