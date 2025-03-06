using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuPanel;  // Panel del menu de pausa
    public GameObject pauseMenuPanelHide; //Panel que se esconde al mostrar opciones
    public GameObject optionsMenuPanel; // Panel de opciones
    private bool isPaused = false;     // Estado de la pausa del juego

    void Update()
    {
        // Si presionamos ESC, o P se activa o desactiva el menú de pausa
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
        Time.timeScale = 1f;             // El tiempo vuelve a la normalidad
        SceneManager.LoadScene("Menu");  // Cargar la escena del menú principal
    }
    public void ShowOptionsMenu()
    {
        pauseMenuPanelHide.SetActive(false);  // Oculta el menú de pausa
        optionsMenuPanel.SetActive(true); // Muestra el menú de opciones
    }
    public void ShowPauseMenu()
    {
        optionsMenuPanel.SetActive(false); // Oculta el menú de opciones
        pauseMenuPanelHide.SetActive(true);   // Muestra el menú de pausa
    }
}
