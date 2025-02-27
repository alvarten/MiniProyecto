using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuPanel;  // Panel del Menú de Pausa
    private bool isPaused = false;     // Estado del juego

    void Update()
    {
        // Si presionamos ESC, se activa o desactiva el menú de pausa
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
        pauseMenuPanel.SetActive(true);  // Muestra el menú de pausa
        Time.timeScale = 0f;             // Pausa el tiempo del juego
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false); // Oculta el menú de pausa
        Time.timeScale = 1f;             // Reactiva el tiempo del juego
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;             // Asegurarse de que el tiempo vuelve a la normalidad
        SceneManager.LoadScene("Menu");  // Cargar la escena del menú principal
    }
}
