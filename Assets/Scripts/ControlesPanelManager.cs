using UnityEngine;

public class ControlesPanelManager : MonoBehaviour
{
    public GameObject panelControles; // Asigna el panel desde el inspector

    void Start()
    {        
        if (PlayerPrefs.GetInt("PantallaControles", 0) == 1)
        {
            ShowPanelAndPause(panelControles);
        }
    }

    // Muestra el panel y pausa el tiempo del juego
    public void ShowPanelAndPause(GameObject panel)
    {
        if (panel != null)
        {
            panel.SetActive(true);
            Time.timeScale = 0f; // Pausar el juego
        }
    }

    // Oculta el panel y reanuda el tiempo del juego
    public void HidePanelAndResume(GameObject panel)
    {
        if (panel != null)
        {
            panel.SetActive(false);
            Time.timeScale = 1f; // Reanudar el juego
        }
    }
}