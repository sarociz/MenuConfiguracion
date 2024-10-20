using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    //timer de tres segundos para que el juego no empiece de repente.
    private float timer = 3.0f;
   // public TextMeshProUGUI startText;
    private bool isPaused = false;


    // Función para reanudar el juego
    public void ResumeGame()
    {
        pauseMenu.SetActive(false); // Desactivar el menú de pausa
        Time.timeScale = 1f; // Reanudar el tiempo
        isPaused = false;
    }

    // Función para pausar el juego
    void PauseGame()
    {
        pauseMenu.SetActive(true); // Mostrar el menú de pausa
        Time.timeScale = 0f; // Detener el tiempo (pausa)
        isPaused = true;
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
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
