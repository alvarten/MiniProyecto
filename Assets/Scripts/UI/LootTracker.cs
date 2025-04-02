using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.Audio;

public class LootTracker : MonoBehaviour
{
    public GameObject coinPanel; // Panel de monedas
    public TextMeshProUGUI coinText; // Texto de monedas

    public GameObject chestPanel; // Panel de cofres
    public TextMeshProUGUI chestText; // Texto de cofres

    private int previousCoins;    // Cantidad de monedas antes del cambio
    private int accumulatedCoins; // Cantidad de monedas acumuladas en la UI
    private Coroutine coinCoroutine; // Referencia a la corrutina de ocultado

    private int previousChests; //Lo mismo pero para el caso de los cofres
    private int accumulatedChests;
    private Coroutine chestCoroutine;

    // Para gestionar el audio
    public AudioClip lootSound;  // Sonido de loot
    private AudioSource audioSource; // Fuente de sonido

    void Start()
    {
        previousCoins = PlayerPrefs.GetInt("Coins", 0);
        previousChests = PlayerPrefs.GetInt("Chests", 0);

        coinPanel.SetActive(false);
        chestPanel.SetActive(false);
    }

    void Update()
    {
        CheckForCoinChange();
        CheckForChestChange();
    }

    void CheckForCoinChange()
    {
        int currentCoins = PlayerPrefs.GetInt("Coins", 0);

        if (currentCoins > previousCoins)
        {
            int gainedCoins = currentCoins - previousCoins;
            ShowCoins(gainedCoins);
        }

        previousCoins = currentCoins;
    }

    void ShowCoins(int gainedCoins)
    {
        LootSound(); // Reproducir sonido de loot
        accumulatedCoins += gainedCoins;
        coinText.text =  accumulatedCoins.ToString();

        coinPanel.SetActive(true);

        if (coinCoroutine != null)
        {
            StopCoroutine(coinCoroutine);
        }
        coinCoroutine = StartCoroutine(HidePanelAfterDelay(coinPanel, () => accumulatedCoins = 0));
    }

    void CheckForChestChange()
    {
        int currentChests = PlayerPrefs.GetInt("Chests", 0);

        if (currentChests > previousChests)
        {
            int gainedChests = currentChests - previousChests;
            ShowChests(gainedChests);
        }

        previousChests = currentChests;
    }

    void ShowChests(int gainedChests)
    {
        accumulatedChests += gainedChests;
        chestText.text = accumulatedChests.ToString();

        chestPanel.SetActive(true);

        if (chestCoroutine != null)
        {
            StopCoroutine(chestCoroutine);
        }
        chestCoroutine = StartCoroutine(HidePanelAfterDelay(chestPanel, () => accumulatedChests = 0));
    }

    void LootSound()
    {
        // Agregar un AudioSource si no existe
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 1f; // 3D Sound
        audioSource.minDistance = 5f;  // Distancia mínima antes de atenuarse
        audioSource.maxDistance = 50f; // Distancia máxima de audición
        audioSource.volume = PlayerPrefs.GetFloat("Volume", 1f); // Ajusta al volumen general
        audioSource.clip = lootSound;
        audioSource.time = 0f; // Iniciar en el segundo 0.7
        audioSource.Play();
        audioSource.SetScheduledEndTime(AudioSettings.dspTime + (0.22));

    }

    IEnumerator HidePanelAfterDelay(GameObject panel, System.Action resetAction)
    {
        yield return new WaitForSeconds(3f);
        panel.SetActive(false);
        resetAction();
    }
}
