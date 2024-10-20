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


    // Funci�n para reanudar el juego
    public void ResumeGame()
    {
        pauseMenu.SetActive(false); // Desactivar el men� de pausa
        Time.timeScale = 1f; // Reanudar el tiempo
        isPaused = false;
    }

    // Funci�n para pausar el juego
    void PauseGame()
    {
        pauseMenu.SetActive(true); // Mostrar el men� de pausa
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

    // Funci�n para salir del juego
    public void ExitGame()
    {
        #if UNITY_EDITOR
                // Si estamos en el editor, simplemente parar la ejecuci�n
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                // Si es una build, salir del juego
                Application.Quit();
        #endif
    }

}
