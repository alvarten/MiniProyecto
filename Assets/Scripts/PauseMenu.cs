using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuPanel;  // Panel del menu de pausa
    private bool isPaused = false;     // Estado de la pausa del juego

    void Update()
    {
        // Si presionamos ESC, o P se activa o desactiva el men� de pausa
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenuPanel.SetActive(true);  // Muestra el men� de pausa
        Time.timeScale = 0f;             // Pausa el tiempo del juego
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false); // Oculta el men� de pausa
        Time.timeScale = 1f;             // Reactiva el tiempo del juego
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;             // El tiempo vuelve a la normalidad
        SceneManager.LoadScene("Menu");  // Cargar la escena del men� principal
    }
}
