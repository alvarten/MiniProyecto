using UnityEngine;

public class VictoriaDerrotaManager : MonoBehaviour
{
    public GameObject victoryPanel; // Panel que se muestra cuando el Boss muere
    public GameObject defeatPanel;  // Panel que se muestra cuando el jugador muere

    private float playerHealth;
    private int bossHealth;

    void Start()
    {
        // Inicializar la vida del jugador y el boss
        playerHealth = PlayerPrefs.GetFloat("vidaActual", 100); // Vida del jugador (por defecto 100)
        bossHealth = PlayerPrefs.GetInt("VidaBoss", 60); // Vida del Boss (por defecto 60)
        
        // Asegurar que los paneles estén ocultos al inicio
        victoryPanel.SetActive(false);
        defeatPanel.SetActive(false);
    }
    private void Update()
    {
        CheckBossHealth();
        CheckPlayerHealth();
    }


    public void CheckBossHealth()
    {
        bossHealth = PlayerPrefs.GetInt("VidaBoss"); // Obtener la vida actual del Boss

        if (bossHealth <= 0)
        {
            ShowVictoryPanel();
        }
    }

    public void CheckPlayerHealth()
    {
        playerHealth = PlayerPrefs.GetFloat("vidaActual"); // Obtener la vida actual del jugador
        Debug.Log(playerHealth);
        if (playerHealth <= 0)
        {
            ShowDefeatPanel();
        }
    }

    void ShowVictoryPanel()
    {
        victoryPanel.SetActive(true); // Mostrar el panel de victoria
        Time.timeScale = 0;
        Debug.Log("¡Has derrotado al Boss!");
    }

    void ShowDefeatPanel()
    {
        defeatPanel.SetActive(true); // Mostrar el panel de derrota
        Time.timeScale = 0;
        Debug.Log("¡Has sido derrotado!");
    }
}
