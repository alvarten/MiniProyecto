using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject mainMenuPanel;  // Panel del menu principal
    public GameObject optionsPanel;   // Panel de Opciones

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
}
