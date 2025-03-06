using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Inventory : MonoBehaviour
{
    public GameObject inventoryPanel; // Panel del inventario
    private bool isInventoryOpen = false; // Estado del inventario

    public TMP_Text ammoText;    // Referencia al Text de la munici�n
    public TMP_Text coinsText;   // Referencia al Text de las monedas
    public TMP_Text chestText;    // Referencia al Text de los cofres
    public TMP_Text harponText;   // Referencia al Text de los arpones

    private int ammo = 50;    // Variable de munici�n
    private int coins = 100;  // Variable de monedas
    private int chest = 5;    // Variable de cofres
    private int harpon = 100;  // Variable de arpones

    void Start()
    {
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(false); // Asegurarse de que empiece oculto
        }

        // Asegurarse de que los valores se actualicen en la UI al inicio
        UpdateAmmoText();
        UpdateCoinsText();
        UpdateChestText();
        UpdateHarponText();
        RemoveItem("ammo",10);
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

        // Opcional: Pausar el juego cuando el inventario est� abierto
        Time.timeScale = isInventoryOpen ? 0f : 1f;
    }

    // M�todos para actualizar cada texto
    public void UpdateAmmoText() => ammoText.text = "" + ammo;
    public void UpdateCoinsText() => coinsText.text = "" + coins;
    public void UpdateChestText() => chestText.text = "" + chest;
    public void UpdateHarponText() => harponText.text = ""+ harpon;

    // M�todo para actualizar todos los textos
    public void UpdateAllTexts()
    {
        UpdateAmmoText();
        UpdateCoinsText();
        UpdateChestText();
        UpdateHarponText();
    }

    // M�todos para a�adir objetos
    public void AddAmmo(int amount) { ammo += amount; UpdateAmmoText(); }
    public void AddCoins(int amount) { coins += amount; UpdateCoinsText(); }
    public void AddChest(int amount) { chest += amount; UpdateChestText(); }
    public void AddHarpon(int amount) { harpon += amount; UpdateHarponText(); }

    // M�todo para reducir cualquier tipo de objeto
    public void RemoveItem(string itemType, int amount)
    {
        switch (itemType.ToLower())
        {
            case "ammo":
                ammo = Mathf.Max(0, ammo - amount);
                UpdateAmmoText();
                break;
            case "coins":
                coins = Mathf.Max(0, coins - amount);
                UpdateCoinsText();
                break;
            case "chest":
                chest = Mathf.Max(0, chest - amount);
                UpdateChestText();
                break;
            case "harpon":
                harpon = Mathf.Max(0, harpon - amount);
                UpdateHarponText();
                break;
            default:
                Debug.LogWarning("Tipo de objeto desconocido: " + itemType);
                break;
        }
    }
}
