using UnityEngine;
using UnityEngine.UI;

public class ScreenSettings : MonoBehaviour
{
    public Toggle fullscreenToggle;
    private static ScreenSettings instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Mantiene el objeto al cambiar de escena
        }
        else
        {
            Destroy(gameObject); // Si ya existe un objeto con la música, lo destruye
        }
    }
    void Start()
    {
        AssignFullscreenToggle();
    }
    void Update()
    {
        if (!Screen.fullScreen)
        {
            PlayerPrefs.SetInt("WindowWidth", Screen.width);
            PlayerPrefs.SetInt("WindowHeight", Screen.height);
            PlayerPrefs.Save();
        }
    }
    void AssignFullscreenToggle()
    {
        // Buscar todos los Toggles en la escena, incluso si están desactivados
        Toggle[] toggles = Resources.FindObjectsOfTypeAll<Toggle>();

        foreach (Toggle toggle in toggles)
        {
            if (toggle.CompareTag("FullscreenToggle")) // Buscar el que tenga la etiqueta correcta
            {
                fullscreenToggle = toggle;
                fullscreenToggle.isOn = Screen.fullScreen;
                fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
                return;
            }
        }

        Debug.LogWarning("No se encontró un Toggle con la etiqueta 'FullscreenToggle'");
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;

        if (!isFullscreen)
        {
            // Permitir que la ventana sea redimensionable cuando no esté en pantalla completa
            int width = PlayerPrefs.GetInt("WindowWidth", 1280);
            int height = PlayerPrefs.GetInt("WindowHeight", 720);
            Screen.SetResolution(width, height, false);
        }

        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    void OnLevelWasLoaded(int level)
    {
        AssignFullscreenToggle(); // Reasignar el Toggle al cambiar de escena
    }
}
