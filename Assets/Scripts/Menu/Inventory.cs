using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Inventory : MonoBehaviour
{
    public GameObject inventoryPanel; // Panel del inventario
    private bool isInventoryOpen = false; // Estado del inventario

    public TMP_Text ammoText;    // Referencia al Text de la munición
    public TMP_Text coinsText;   // Referencia al Text de las monedas
    public TMP_Text chestText;    // Referencia al Text de los cofres
    public TMP_Text harponText;   // Referencia al Text de los arpones

    void Start()
    {
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(false); // Asegurarse de que empiece oculto
        }

        // Cargar las cantidades de PlayerPrefs (si están guardadas) o usar valores predeterminados
        int ammo = PlayerPrefs.GetInt("CannonBallAmmo", 50); // Valor predeterminado 50 si no existe
        int harpon = PlayerPrefs.GetInt("HarpoonAmmo", 50); // Valor predeterminado 50 si no existe
        int coins = PlayerPrefs.GetInt("Coins", 100);  // Valor predeterminado 100 si no existe
        int chest = PlayerPrefs.GetInt("Chests", 5); // Valor predeterminado 5 si no existe

        // Actualizar los textos en la UI
        ammoText.text = ammo.ToString();
        coinsText.text = coins.ToString();
        chestText.text = chest.ToString();
        harponText.text = harpon.ToString();

        //Para el testeo
        //PlayerPrefs.SetInt("CannonBallAmmo", 50);
        //PlayerPrefs.SetInt("HarpoonAmmo", 50);
        PlayerPrefs.SetInt("Coins", 5000);
        PlayerPrefs.SetInt("Relics", 50);
        PlayerPrefs.SetInt("Chests", 500);
        //PlayerPrefs.SetFloat("vidaMaxima", 100f);
    }

    void Update()
    {
        // Si presionamos "I" se activa o desactiva el inventario
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryPanel.SetActive(isInventoryOpen);
        UpdateAllTexts();
        // Pausar el juego cuando el inventario está abierto
        Time.timeScale = isInventoryOpen ? 0f : 1f;
    }

    // Métodos para actualizar cada texto
    public void UpdateAmmoText() => ammoText.text = PlayerPrefs.GetInt("CannonBallAmmo", 50).ToString();
    public void UpdateCoinsText() => coinsText.text = PlayerPrefs.GetInt("Coins", 100).ToString();
    public void UpdateChestText() => chestText.text = PlayerPrefs.GetInt("Chests", 5).ToString();
    public void UpdateHarponText() => harponText.text = PlayerPrefs.GetInt("HarpoonAmmo", 50).ToString();

    // Método para actualizar todos los textos
    public void UpdateAllTexts()
    {
        UpdateAmmoText();
        UpdateCoinsText();
        UpdateChestText();
        UpdateHarponText();
    }

    // Métodos para añadir objetos
    public void AddAmmo(int amount)
    {
        int newAmmo = PlayerPrefs.GetInt("CannonBallAmmo", 50) + amount;
        PlayerPrefs.SetInt("CannonBallAmmo", newAmmo);
        UpdateAmmoText();
    }
    public void AddCoins(int amount)
    {
        int newCoins = PlayerPrefs.GetInt("Coins", 100) + amount;
        PlayerPrefs.SetInt("Coins", newCoins);
        UpdateCoinsText();
    }
    public void AddChest(int amount)
    {
        int newChest = PlayerPrefs.GetInt("Chests", 5) + amount;
        PlayerPrefs.SetInt("Chests", newChest);
        UpdateChestText();
    }
    public void AddHarpon(int amount)
    {
        int newHarpon = PlayerPrefs.GetInt("HarpoonAmmo", 100) + amount;
        PlayerPrefs.SetInt("HarpoonAmmo", newHarpon);
        UpdateHarponText();
    }

    // Método para reducir cualquier tipo de objeto
    public void RemoveItem(string itemType, int amount)
    {
        switch (itemType.ToLower())
        {
            case "ammo":
                int ammo = Mathf.Max(0, PlayerPrefs.GetInt("CannonBallAmmo", 50) - amount);
                PlayerPrefs.SetInt("CannonBallAmmo", ammo);
                UpdateAmmoText();
                break;
            case "coins":
                int coins = Mathf.Max(0, PlayerPrefs.GetInt("Coins", 100) - amount);
                PlayerPrefs.SetInt("Coins", coins);
                UpdateCoinsText();
                break;
            case "chest":
                int chest = Mathf.Max(0, PlayerPrefs.GetInt("Chests", 5) - amount);
                PlayerPrefs.SetInt("Chests", chest);
                UpdateChestText();
                break;
            case "harpon":
                int harpon = Mathf.Max(0, PlayerPrefs.GetInt("HarpoonAmmo", 100) - amount);
                PlayerPrefs.SetInt("HarpoonAmmo", harpon);
                UpdateHarponText();
                break;
            default:
                Debug.LogWarning("Tipo de objeto desconocido: " + itemType);
                break;
        }
    }
}
