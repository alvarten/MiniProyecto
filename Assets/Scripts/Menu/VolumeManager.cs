using UnityEngine;
using UnityEngine.UI;


public class VolumeManager : MonoBehaviour
{
    public Slider volumeSlider;
    private static VolumeManager instance;
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
        AssignVolumeSlider();
    }

    void AssignVolumeSlider()
    {
        // Buscar todos los sliders en la escena
        Slider[] sliders = Resources.FindObjectsOfTypeAll<Slider>();

        foreach (Slider slider in sliders)
        {
            if (slider.CompareTag("VolumeSlider")) // Buscar el que tenga la etiqueta correcta
            {
                volumeSlider = slider;
                volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
                volumeSlider.onValueChanged.AddListener(SetVolume);
                return;
            }
        }

        Debug.LogWarning("No se encontró un Slider con la etiqueta 'VolumeSlider'");
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }

    void OnLevelWasLoaded(int level)
    {
        AssignVolumeSlider(); // Reasignar el Slider al cambiar de escena
    }
}
