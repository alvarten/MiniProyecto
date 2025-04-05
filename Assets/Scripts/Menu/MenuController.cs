using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject mainMenuPanel;  // Panel del menu principal
    public GameObject optionsPanel;   // Panel de Opciones
    public GameObject resetPanel;   // Panel de reseteo
    public void ShowOptions()
    {
        mainMenuPanel.SetActive(false); // Oculta el menu principal
        optionsPanel.SetActive(true);   // Muestra el panel de opciones
    }

    public void ShowMainMenu()
    {
        optionsPanel.SetActive(false);  // Oculta el panel de opciones
        mainMenuPanel.SetActive(true);  // Muestra el menu principal
    }

    public void ShowResetConfirm()
    {
        resetPanel.SetActive(true);  // Muestra el panel de reset
        mainMenuPanel.SetActive(false);
    }
    public void HideResetConfirm()
    {
        resetPanel.SetActive(false);  // Oculta el panel de reset
        mainMenuPanel.SetActive(true);
    }
    public void Reset()
    {
        PlayerPrefs.SetInt("inicioPartida", 1); // Setear el inicio de partida para activar el codigo PartidaManager
        HideResetConfirm(); // Oculta el panel
    }
}
