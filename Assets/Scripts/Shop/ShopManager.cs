using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    // Precios ajustables desde el Inspector
    public int priceCannonAmmo10 = 50;   // Precio por 10 balas de cañón
    public int priceMaxAmmoUpgrade = 450; // Precio para aumentar el máximo de munición en 20
    public int priceHarpoonAmmo10 = 50; // Precio por 10 arpones
    public int priceDamageUpgrade = 200; // Precio para mejorar el daño de los cañones/arpones
    public int priceHeal20 = 40; // Precio para curar 20 de vida
    public int priceMaxHealth150 = 1200; // Precio para mejorar a 150 de vida máxima
    public int priceMaxHealth200 = 2300; // Precio para mejorar a 200 de vida máxima
    public int priceSpeed = 250; // Precio para mejorar la velocidad


    // Texto de dinero
    public TMP_Text coinsText1;
    public TMP_Text coinsText2;
    public TMP_Text coinsText3;
    public TMP_Text coinsText4;
    public TMP_Text coinsText5;
    public TMP_Text chestText;
    public TMP_Text relicText1;
    public TMP_Text relicText2;
    public TMP_Text CannonMaxText;
    public TMP_Text HarpoonMaxText;
    public TMP_Text VidaMaxText;
    public TMP_Text UpgradeVidaMaxText;

    public MonoBehaviour playerMovementScript; // Script de movimiento del jugador

    void Start()
    {        
        // Inicializar valores en PlayerPrefs si no existen
        if (!PlayerPrefs.HasKey("Coins")) PlayerPrefs.SetInt("Coins", 200);
        if (!PlayerPrefs.HasKey("CannonBallAmmo")) PlayerPrefs.SetInt("CannonBallAmmo", 0);
        if (!PlayerPrefs.HasKey("MaxCannonBallAmmo")) PlayerPrefs.SetInt("MaxCannonBallAmmo", 50);
        if (!PlayerPrefs.HasKey("HarpoonAmmo")) PlayerPrefs.SetInt("HarpoonAmmo", 0);
        if (!PlayerPrefs.HasKey("MaxHarpoonAmmo")) PlayerPrefs.SetInt("MaxHarpoonAmmo", 50);
        if (!PlayerPrefs.HasKey("CannonBallDamage")) PlayerPrefs.SetInt("CannonBallDamage", 10);
        if (!PlayerPrefs.HasKey("HarpoonDamage")) PlayerPrefs.SetInt("HarpoonDamage", 5);

        //En caso de haber terminado la quest de Henry, al llegar a puerto se rellena la municion de manera automatica sin coste

        if (PlayerPrefs.GetInt("progresoHenry", 0) == 4) {
            PlayerPrefs.SetInt("HarpoonAmmo", PlayerPrefs.GetInt("MaxHarpoonAmmo", 50));
            PlayerPrefs.SetInt("CannonBallAmmo", PlayerPrefs.GetInt("MaxCannonBallAmmo", 50));
        }
    }




    void Update()
    {
        // Se actualizan los valores de los textos
        UpdateCoinsText();
        UpdateMaxHarpoonText();
        UpdateMaxCannonText();
        UpdateHealMaxPriceText();
        UpdateUpgradeVidaMaxText();


        if (Input.GetKeyDown(KeyCode.F))
        {
            int relics = PlayerPrefs.GetInt("Relics", 0); // Obtener cantidad actual de reliquias
            relics++; // Sumar una reliquia
            PlayerPrefs.SetInt("Relics", relics); // Guardar en PlayerPrefs
            Debug.Log("Reliquia añadida. Total: " + relics);
        }



    }
    // Mostrar un panel de UI
    public void ShowPanel(GameObject panel)
    {
        if (panel != null)
        {
            panel.SetActive(true);

            if (playerMovementScript != null)
            {
                playerMovementScript.enabled = false; // Desactiva el movimiento
            }
        }
    }

    // Ocultar un panel de UI
    public void HidePanel(GameObject panel)
    {
        if (panel != null)
        {
            panel.SetActive(false);

            if (playerMovementScript != null)
            {
                playerMovementScript.enabled = true; // Reactiva el movimiento
            }
        }
    }
    //Actualizar textos para mostrar dinero actual (y cofres)
    public void UpdateCoinsText() 
    { 
        coinsText1.text = PlayerPrefs.GetInt("Coins", 100).ToString();
        coinsText2.text = PlayerPrefs.GetInt("Coins", 100).ToString();
        coinsText3.text = PlayerPrefs.GetInt("Coins", 100).ToString();
        coinsText4.text = PlayerPrefs.GetInt("Coins", 100).ToString();
        coinsText5.text = PlayerPrefs.GetInt("Coins", 100).ToString();
        chestText.text = PlayerPrefs.GetInt("Chests", 10).ToString();
        relicText1.text = PlayerPrefs.GetInt("Relics", 1).ToString();
        relicText2.text = PlayerPrefs.GetInt("Relics", 1).ToString();
    }

    // Actualizar la compra de arpones
    public void UpdateMaxHarpoonText() {

        int coins = PlayerPrefs.GetInt("Coins", 0);
        int currentAmmo = PlayerPrefs.GetInt("HarpoonAmmo", 0);
        int maxAmmo = PlayerPrefs.GetInt("MaxHarpoonAmmo", 0);

        int ammoNeeded = maxAmmo - currentAmmo;
        float pricePerAmmo = priceHarpoonAmmo10 / 10f;
        int maxAffordable = Mathf.Min(ammoNeeded, (int)(coins / pricePerAmmo));

        HarpoonMaxText.text = (Mathf.RoundToInt(maxAffordable * pricePerAmmo)).ToString() + " Coins";

    }

    // Actualizar la compra de balas de canon
    public void UpdateMaxCannonText()
    {

        int coins = PlayerPrefs.GetInt("Coins", 0);
        int currentAmmo = PlayerPrefs.GetInt("CannonBallAmmo", 0);
        int maxAmmo = PlayerPrefs.GetInt("MaxCannonBallAmmo", 0);

        int ammoNeeded = maxAmmo - currentAmmo;
        float pricePerAmmo = priceCannonAmmo10 / 10f;
        int maxAffordable = Mathf.Min(ammoNeeded, (int)(coins / pricePerAmmo));

        CannonMaxText.text = (Mathf.RoundToInt(maxAffordable * pricePerAmmo)).ToString() + " Coins";
    }

    // Actualizar el texto de la compra de vida maxima
    public void UpdateHealMaxPriceText()
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        float vidaActual = PlayerPrefs.GetFloat("vidaActual", 100f);
        float vidaMaxima = PlayerPrefs.GetFloat("vidaMaxima", 100f);

        float vidaFaltante = vidaMaxima - vidaActual;
        float costoPorUnidad = priceHeal20 / 20f; // Precio de curar 1 punto de vida
        float vidaCurable = Mathf.Min(vidaFaltante, coins / costoPorUnidad); // Vida máxima que se puede curar

        int costoFinal = Mathf.FloorToInt(vidaCurable * costoPorUnidad); // Coste real en monedas

        // Actualizar el texto con el precio calculado
        VidaMaxText.text = costoFinal + " Coins";
    }
    // Actualizar el precio de mejorar vida maxima
    public void UpdateUpgradeVidaMaxText() 
    {
        float vidaMaxima = PlayerPrefs.GetFloat("vidaMaxima", 100f);

        if (vidaMaxima == 100)
        {
            UpgradeVidaMaxText.text = priceMaxHealth150.ToString() + " Coins";
        }
        else if(vidaMaxima == 150)
        {
            UpgradeVidaMaxText.text = priceMaxHealth200.ToString() + " Coins";
        }
        else
        {
            UpgradeVidaMaxText.text = "Sold Out";
        }


    } 


    // Curar 20 de vida si se tiene suficiente dinero
    public void Heal20()
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        float vidaActual = PlayerPrefs.GetFloat("vidaActual", 100f);
        float vidaMaxima = PlayerPrefs.GetFloat("vidaMaxima", 100f);

        if (coins >= priceHeal20 && vidaActual < vidaMaxima)
        {
            PlayerPrefs.SetInt("Coins", coins - priceHeal20);
            PlayerPrefs.SetFloat("vidaActual", Mathf.Min(vidaActual + 20, vidaMaxima));
        }
    }

    // Curar al máximo según el dinero disponible
    public void HealMax()
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        float vidaActual = PlayerPrefs.GetFloat("vidaActual", 100f);
        float vidaMaxima = PlayerPrefs.GetFloat("vidaMaxima", 100f);

        float vidaFaltante = vidaMaxima - vidaActual;
        float costoPorUnidad = priceHeal20 / 20f; // Precio de curar 1 punto de vida
        float vidaCurable = Mathf.Min(vidaFaltante, coins / costoPorUnidad); // Vida máxima que se puede curar

        int costoFinal = Mathf.FloorToInt(vidaCurable * costoPorUnidad); // Coste real en monedas

        if (vidaCurable > 0 && costoFinal > 0)
        {
            PlayerPrefs.SetInt("Coins", coins - costoFinal);
            PlayerPrefs.SetFloat("vidaActual", vidaActual + vidaCurable);
        }
    }

    // Aumentar la vida máxima en 50 si se tiene suficiente dinero asta 200
    public void UpgradeMaxHealth()
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        float vidaMaxima = PlayerPrefs.GetFloat("vidaMaxima", 100f);

        if (vidaMaxima == 100 && coins >= priceMaxHealth150)
        {
            PlayerPrefs.SetInt("Coins", coins - priceMaxHealth150);
            PlayerPrefs.SetFloat("vidaMaxima", 150);
        }
        else if (vidaMaxima == 150 && coins >= priceMaxHealth200)
        {
            PlayerPrefs.SetInt("Coins", coins - priceMaxHealth200);
            PlayerPrefs.SetFloat("vidaMaxima", 200);
        }
        else
        {
            Debug.Log("No se puede mejorar más la vida o no tienes suficientes monedas.");
        }
    }


    // Comprar 10 balas de cañón
    public void BuyCannonBallAmmo10()
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        int currentAmmo = PlayerPrefs.GetInt("CannonBallAmmo", 0);
        int maxAmmo = PlayerPrefs.GetInt("MaxCannonBallAmmo", 0);

        if (coins >= priceCannonAmmo10 && currentAmmo + 10 <= maxAmmo)
        {
            PlayerPrefs.SetInt("Coins", coins - priceCannonAmmo10);
            PlayerPrefs.SetInt("CannonBallAmmo", currentAmmo + 10);
            Debug.Log("¡Compraste 10 balas de cañón!");
        }
        else
        {
            Debug.Log("No puedes comprar más munición.");
        }
    }

    // Comprar el máximo posible de balas de cañón
    public void BuyMaxCannonBallAmmo()
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        int currentAmmo = PlayerPrefs.GetInt("CannonBallAmmo", 0);
        int maxAmmo = PlayerPrefs.GetInt("MaxCannonBallAmmo", 0);

        int ammoNeeded = maxAmmo - currentAmmo;
        float pricePerAmmo = priceCannonAmmo10 / 10f;
        int maxAffordable = Mathf.Min(ammoNeeded, (int)(coins / pricePerAmmo));

        if (maxAffordable > 0)
        {
            int totalCost = (int)(maxAffordable * pricePerAmmo);
            PlayerPrefs.SetInt("Coins", coins - totalCost);
            PlayerPrefs.SetInt("CannonBallAmmo", currentAmmo + maxAffordable);
            Debug.Log("¡Has comprado munición hasta el máximo!");
        }
        else
        {
            Debug.Log("No tienes suficiente dinero o ya tienes el máximo.");
        }
    }

    // Aumentar el máximo de munición en 20
    public void UpgradeMaxCannonBallAmmo()
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        int maxAmmo = PlayerPrefs.GetInt("MaxCannonBallAmmo", 0);

        if (coins >= priceMaxAmmoUpgrade)
        {
            PlayerPrefs.SetInt("Coins", coins - priceMaxAmmoUpgrade);
            PlayerPrefs.SetInt("MaxCannonBallAmmo", maxAmmo + 20);
            Debug.Log("¡Has aumentado el límite de munición en 20!");
        }
        else
        {
            Debug.Log("No tienes suficiente dinero.");
        }
    }

    // Comprar 10 arpones
    public void BuyHarpoonAmmo10()
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        int currentAmmo = PlayerPrefs.GetInt("HarpoonAmmo", 0);
        int maxAmmo = PlayerPrefs.GetInt("MaxHarpoonAmmo", 0);

        if (coins >= priceHarpoonAmmo10 && currentAmmo + 10 <= maxAmmo)
        {
            PlayerPrefs.SetInt("Coins", coins - priceHarpoonAmmo10);
            PlayerPrefs.SetInt("HarpoonAmmo", currentAmmo + 10);
            Debug.Log("¡Compraste 10 arpones!");
        }
        else
        {
            Debug.Log("No puedes comprar más arpones.");
        }
    }

    // Comprar el máximo posible de arpones
    public void BuyMaxHarpoonAmmo()
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        int currentAmmo = PlayerPrefs.GetInt("HarpoonAmmo", 0);
        int maxAmmo = PlayerPrefs.GetInt("MaxHarpoonAmmo", 0);

        int ammoNeeded = maxAmmo - currentAmmo;
        float pricePerAmmo = priceHarpoonAmmo10 / 10f;
        int maxAffordable = Mathf.Min(ammoNeeded, (int)(coins / pricePerAmmo));

        if (maxAffordable > 0)
        {
            int totalCost = (int)(maxAffordable * pricePerAmmo);
            PlayerPrefs.SetInt("Coins", coins - totalCost);
            PlayerPrefs.SetInt("HarpoonAmmo", currentAmmo + maxAffordable);
            Debug.Log("¡Has comprado arpones hasta el máximo!");
        }
        else
        {
            Debug.Log("No tienes suficiente dinero o ya tienes el máximo.");
        }
    }

    // Aumentar el máximo de arpones en 20
    public void UpgradeMaxHarpoonAmmo()
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        int maxAmmo = PlayerPrefs.GetInt("MaxHarpoonAmmo", 0);

        if (coins >= priceMaxAmmoUpgrade)
        {
            PlayerPrefs.SetInt("Coins", coins - priceMaxAmmoUpgrade);
            PlayerPrefs.SetInt("MaxHarpoonAmmo", maxAmmo + 20);
            Debug.Log("¡Has aumentado el límite de arpones en 20!");
        }
        else
        {
            Debug.Log("No tienes suficiente dinero.");
        }
    }
    // Mejorar daño de las balas de cañón
    public void UpgradeCannonBallDamage()
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        int cannonBallDamage = PlayerPrefs.GetInt("CannonBallDamage", 10);

        if (coins >= priceDamageUpgrade)
        {
            PlayerPrefs.SetInt("Coins", coins - priceDamageUpgrade);
            PlayerPrefs.SetInt("CannonBallDamage", cannonBallDamage + 1); // Aumenta en 1 punto
            Debug.Log("¡Has mejorado el daño de los cañones!");
        }
        else
        {
            Debug.Log("No tienes suficiente dinero.");
        }
    }

    // Mejorar daño de los arpones
    public void UpgradeHarpoonDamage()
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        int harpoonDamage = PlayerPrefs.GetInt("HarpoonDamage", 5);

        if (coins >= priceDamageUpgrade)
        {
            PlayerPrefs.SetInt("Coins", coins - priceDamageUpgrade);
            PlayerPrefs.SetInt("HarpoonDamage", harpoonDamage + 1); // Aumenta en 1 punto
            Debug.Log("¡Has mejorado el daño de los arpones!");
        }
        else
        {
            Debug.Log("No tienes suficiente dinero.");
        }
    }

    // Mejorar la velocidad del barco
    public void Speed20()
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        float velocidad = PlayerPrefs.GetFloat("Speed", 5f);

        if (coins >= priceSpeed)
        {
            PlayerPrefs.SetInt("Coins", coins - priceSpeed);
            PlayerPrefs.SetFloat("Speed", (Mathf.Round((velocidad + 0.2f) * 10f) / 10f));
            Debug.Log("Tu velocidad actual es " + PlayerPrefs.GetFloat("Speed",0));
        }
    }

    //NPC de cofres
    // Función para abrir un solo cofre
    public void OpenOneChest()
    {
        int chests = PlayerPrefs.GetInt("Chests", 0); // Obtener cantidad de cofres
        int coins = PlayerPrefs.GetInt("Coins", 0);  // Obtener cantidad de monedas
        int relic = PlayerPrefs.GetInt("Relics", 0);  // Obtener cantidad de reliquias

        if (chests > 0) // Si hay cofres disponibles
        {
            // Restar uno de los cofres
            PlayerPrefs.SetInt("Chests", chests - 1);
            Debug.Log("Cofre abierto. Cofres restantes: " + (chests - 1));

            // Generar un valor aleatorio entre 0 y 100
            int randomChance = Random.Range(0, 100);

            // 90% de probabilidad de obtener monedas entre 120 y 200
            if (randomChance < 90)
            {
                int coinsToGive = Random.Range(30, 120);
                PlayerPrefs.SetInt("Coins", coins + coinsToGive);
                Debug.Log("Has obtenido " + coinsToGive + " monedas.");
            }
            // 10% de probabilidad de obtener una reliquia
            else
            {
                if (relic < 30)
                {
                    // Si hay menos de 30 reliquias, se suma una reliquia
                    PlayerPrefs.SetInt("Relics", relic + 1);
                    Debug.Log("Has obtenido una reliquia.");
                }
                else
                {
                    // Si ya tienes 30 reliquias, se suman 500 monedas
                    PlayerPrefs.SetInt("Coins", coins + 500);
                    Debug.Log("Ya tienes 30 reliquias. Has recibido 500 monedas.");
                }
            }
        }
        else
        {
            Debug.Log("No tienes cofres disponibles.");
        }
    }

    // Función para abrir todos los cofres disponibles
    public void OpenAllChests()
    {
        int chests = PlayerPrefs.GetInt("Chests", 0); // Obtener cantidad de cofres

        if (chests > 0) // Mientras haya cofres
        {
            OpenOneChest(); // Abre un solo cofre
            OpenAllChests(); // Llama a la función recursiva para abrir el siguiente cofre
        }
        else
        {
            Debug.Log("Todos los cofres han sido abiertos.");
        }
    }
}
