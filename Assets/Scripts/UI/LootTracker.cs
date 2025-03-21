using UnityEngine;
using System.Collections;
using TMPro;

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

    IEnumerator HidePanelAfterDelay(GameObject panel, System.Action resetAction)
    {
        yield return new WaitForSeconds(3f);
        panel.SetActive(false);
        resetAction();
    }
}
