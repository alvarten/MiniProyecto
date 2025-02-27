using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject mainMenuPanel;  // Panel del Menú Principal
    public GameObject optionsPanel;   // Panel de Opciones

    public void ShowOptions()
    {
        mainMenuPanel.SetActive(false); // Oculta el Menú Principal
        optionsPanel.SetActive(true);   // Muestra el Panel de Opciones
    }

    public void ShowMainMenu()
    {
        optionsPanel.SetActive(false);  // Oculta el Panel de Opciones
        mainMenuPanel.SetActive(true);  // Muestra el Menú Principal
    }
}
