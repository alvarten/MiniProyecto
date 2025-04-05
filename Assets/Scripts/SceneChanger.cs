using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{    
    public void ChangeScene(string sceneName)
    {
        // Guardamos desde dónde venimos
        if (PlayerSpawnManager.Instance != null)
        {
            PlayerSpawnManager.Instance.SetLastScene(SceneManager.GetActiveScene().name);
        }

        // Guardamos la posición del jugador antes de cambiar de escena
        PlayerPrefs.Save();

        //Cambiar la escena
        SceneManager.LoadScene(sceneName);

        // Cambiar valores para asegurar que no se cumpla condición de victoria o derrota al cambiar de escena
        if (PlayerPrefs.GetFloat("vidaActual", 100f) == 0)
        {
            PlayerPrefs.SetFloat("vidaActual", 10f);
        }
        if (PlayerPrefs.GetInt("VidaBoss", 60) == 0)
        {
            PlayerPrefs.SetInt("VidaBoss", 60);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Juego Cerrado"); // Para checkear en el editor
    }
}
