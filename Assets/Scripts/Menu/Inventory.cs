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



    // M�todo para actualizar el texto de la munici�n
    public void UpdateAmmoText()
    {
        ammoText.text =  ammo.ToString();
    }

    // M�todo para actualizar el texto de las monedas
    public void UpdateCoinsText()
    {
        coinsText.text =  coins.ToString();
    }

    // M�todo para actualizar el texto de los cofres
    public void UpdateChestText()
    {
        chestText.text = ammo.ToString();
    }

    // M�todo para actualizar el texto de los harpones
    public void UpdateHarponText()
    {
        harponText.text = coins.ToString();
    }
    // M�todos para cambiar las variables
    public void AddAmmo(int amount)
    {
        ammo += amount;
        UpdateAmmoText();  // Actualizamos el texto de munici�n cada vez que cambia
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateCoinsText();  // Actualizamos el texto de monedas cada vez que cambia
    }
    public void AddChest(int amount)
    {
        chest += amount;
        UpdateChestText();  // Actualizamos el texto de munici�n cada vez que cambia
    }

    public void AddHarpon(int amount)
    {
        harpon += amount;
        UpdateHarponText();  // Actualizamos el texto de monedas cada vez que cambia
    }
}
