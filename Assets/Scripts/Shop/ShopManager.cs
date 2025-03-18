using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    // Precios ajustables desde el Inspector
    public int priceAmmo10 = 50;   // Precio por 10 balas de ca��n
    public int priceMaxAmmoUpgrade = 150; // Precio para aumentar el m�ximo de munici�n en 20
    public int priceHarpoonAmmo10 = 75; // Precio por 10 arpones
    public int priceDamageUpgrade = 200; // Precio para mejorar el da�o de los ca�ones/arpones

    // Texto de dinero
    public TMP_Text coinsText;


    void Start()
    {
        // Inicializar valores en PlayerPrefs si no existen
        if (!PlayerPrefs.HasKey("Coins")) PlayerPrefs.SetInt("Coins", 200);
        if (!PlayerPrefs.HasKey("CannonBallAmmo")) PlayerPrefs.SetInt("CannonBallAmmo", 0);
        if (!PlayerPrefs.HasKey("MaxCannonBallAmmo")) PlayerPrefs.SetInt("MaxCannonBallAmmo", 50);
        if (!PlayerPrefs.HasKey("HarpoonAmmo")) PlayerPrefs.SetInt("HarpoonAmmo", 0);
        if (!PlayerPrefs.HasKey("MaxHarpoonAmmo")) PlayerPrefs.SetInt("MaxHarpoonAmmo", 30);
        if (!PlayerPrefs.HasKey("CannonBallDamage")) PlayerPrefs.SetInt("CannonBallDamage", 10);
        if (!PlayerPrefs.HasKey("HarpoonDamage")) PlayerPrefs.SetInt("HarpoonDamage", 5);
    }

    void Update()
    {
        // Se actualizan los valores de los textos
        UpdateCoinsText();
    }
    // Mostrar un panel de UI
    public void ShowPanel(GameObject panel)
    {
        if (panel != null)
        {
            panel.SetActive(true);
        }
    }

    // Ocultar un panel de UI
    public void HidePanel(GameObject panel)
    {
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }
    //Mostrar dinero actual
    public void UpdateCoinsText() => coinsText.text = PlayerPrefs.GetInt("Coins", 100).ToString();

    // Comprar 10 balas de ca��n
    public void BuyCannonBallAmmo10()
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        int currentAmmo = PlayerPrefs.GetInt("CannonBallAmmo", 0);
        int maxAmmo = PlayerPrefs.GetInt("MaxCannonBallAmmo", 0);

        if (coins >= priceAmmo10 && currentAmmo + 10 <= maxAmmo)
        {
            PlayerPrefs.SetInt("Coins", coins - priceAmmo10);
            PlayerPrefs.SetInt("CannonBallAmmo", currentAmmo + 10);
            Debug.Log("�Compraste 10 balas de ca��n!");
        }
        else
        {
            Debug.Log("No puedes comprar m�s munici�n.");
        }
    }

    // Comprar el m�ximo posible de balas de ca��n
    public void BuyMaxCannonBallAmmo()
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        int currentAmmo = PlayerPrefs.GetInt("CannonBallAmmo", 0);
        int maxAmmo = PlayerPrefs.GetInt("MaxCannonBallAmmo", 0);

        int ammoNeeded = maxAmmo - currentAmmo;
        int maxAffordable = Mathf.Min(ammoNeeded, coins / priceAmmo10 * 10);

        if (maxAffordable > 0)
        {
            PlayerPrefs.SetInt("Coins", coins - (maxAffordable / 10 * priceAmmo10));
            PlayerPrefs.SetInt("CannonBallAmmo", currentAmmo + maxAffordable);
            Debug.Log("�Has comprado munici�n hasta el m�ximo!");
        }
        else
        {
            Debug.Log("No tienes suficiente dinero o ya tienes el m�ximo.");
        }
    }

    // Aumentar el m�ximo de munici�n en 20
    public void UpgradeMaxCannonBallAmmo()
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        int maxAmmo = PlayerPrefs.GetInt("MaxCannonBallAmmo", 0);

        if (coins >= priceMaxAmmoUpgrade)
        {
            PlayerPrefs.SetInt("Coins", coins - priceMaxAmmoUpgrade);
            PlayerPrefs.SetInt("MaxCannonBallAmmo", maxAmmo + 20);
            Debug.Log("�Has aumentado el l�mite de munici�n en 20!");
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
            Debug.Log("�Compraste 10 arpones!");
        }
        else
        {
            Debug.Log("No puedes comprar m�s arpones.");
        }
    }

    // Comprar el m�ximo posible de arpones
    public void BuyMaxHarpoonAmmo()
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        int currentAmmo = PlayerPrefs.GetInt("HarpoonAmmo", 0);
        int maxAmmo = PlayerPrefs.GetInt("MaxHarpoonAmmo", 0);

        int ammoNeeded = maxAmmo - currentAmmo;
        int maxAffordable = Mathf.Min(ammoNeeded, coins / priceHarpoonAmmo10 * 10);

        if (maxAffordable > 0)
        {
            PlayerPrefs.SetInt("Coins", coins - (maxAffordable / 10 * priceHarpoonAmmo10));
            PlayerPrefs.SetInt("HarpoonAmmo", currentAmmo + maxAffordable);
            Debug.Log("�Has comprado arpones hasta el m�ximo!");
        }
        else
        {
            Debug.Log("No tienes suficiente dinero o ya tienes el m�ximo.");
        }
    }

    // Aumentar el m�ximo de arpones en 20
    public void UpgradeMaxHarpoonAmmo()
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        int maxAmmo = PlayerPrefs.GetInt("MaxHarpoonAmmo", 0);

        if (coins >= priceMaxAmmoUpgrade)
        {
            PlayerPrefs.SetInt("Coins", coins - priceMaxAmmoUpgrade);
            PlayerPrefs.SetInt("MaxHarpoonAmmo", maxAmmo + 20);
            Debug.Log("�Has aumentado el l�mite de arpones en 20!");
        }
        else
        {
            Debug.Log("No tienes suficiente dinero.");
        }
    }
    // Mejorar da�o de las balas de ca��n
    public void UpgradeCannonBallDamage()
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        int cannonBallDamage = PlayerPrefs.GetInt("CannonBallDamage", 10);

        if (coins >= priceDamageUpgrade)
        {
            PlayerPrefs.SetInt("Coins", coins - priceDamageUpgrade);
            PlayerPrefs.SetInt("CannonBallDamage", cannonBallDamage + 5); // Aumenta en 5 puntos
            Debug.Log("�Has mejorado el da�o de los ca�ones!");
        }
        else
        {
            Debug.Log("No tienes suficiente dinero.");
        }
    }

    // Mejorar da�o de los arpones
    public void UpgradeHarpoonDamage()
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        int harpoonDamage = PlayerPrefs.GetInt("HarpoonDamage", 5);

        if (coins >= priceDamageUpgrade)
        {
            PlayerPrefs.SetInt("Coins", coins - priceDamageUpgrade);
            PlayerPrefs.SetInt("HarpoonDamage", harpoonDamage + 3); // Aumenta en 3 puntos
            Debug.Log("�Has mejorado el da�o de los arpones!");
        }
        else
        {
            Debug.Log("No tienes suficiente dinero.");
        }
    }
}
