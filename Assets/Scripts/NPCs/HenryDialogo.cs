using TMPro;
using UnityEngine;

public class HenryDialogo : MonoBehaviour
{
    public TextMeshProUGUI dialogo; // Referencia al texto UI

    void Update()
    {
        UpdateText();
    }

    void UpdateText()
    {
        int relics = PlayerPrefs.GetInt("Relics", 0);
        int progresoHenry = PlayerPrefs.GetInt("progresoHenry", 0);
        int coins = PlayerPrefs.GetInt("Coins", 0);
        string message = "";

        if (relics >= 5 && progresoHenry < 1)
        {
            coins += 1000;
            progresoHenry = 1;
            PlayerPrefs.SetInt("Coins", coins);
            PlayerPrefs.SetInt("progresoHenry", progresoHenry);
        }
        else if (relics >= 10 && progresoHenry < 2)
        {
            coins += 2500;
            progresoHenry = 2;
            PlayerPrefs.SetInt("Coins", coins);
            PlayerPrefs.SetInt("progresoHenry", progresoHenry);
        }
        else if (relics >= 15 && progresoHenry < 3)
        {
            coins += 3000;
            progresoHenry = 3;
            PlayerPrefs.SetInt("Coins", coins);
            PlayerPrefs.SetInt("progresoHenry", progresoHenry);
        }
        else if (relics >= 30 && progresoHenry < 4)
        {
            progresoHenry = 4;
            PlayerPrefs.SetInt("progresoHenry", progresoHenry);
            // La recarga de municion se gestiona en ShopManager
        }

        if (relics < 5)
            message = "Reliquias: " + relics + ". \nPor favor, sigue buscando, cuando llegues a 5 te recompensaré";
        else if (relics < 10)
            message = "Reliquias: " + relics + ". Has alcanzado 5 reliquias! \nMuchas gracias, acepta estas 1000 monedas como pago, si llegas a 10 te daré más";
        else if (relics < 15)
            message = "Reliquias: " + relics + ". Has conseguido 10 reliquias! \nCada vez veo más cerca el poder tenerlas todas de nuevo, por favor acepta estas 2500 monedas";
        else if (relics < 30)
            message = "Reliquias: " + relics + ". Tienes 15 reliquias!\nEstá claro que puedo confiar en ti, toma 3000 monedas. Si llegas a 30 me haré cargo de los costes de tu munición, te debo mucho";
        else
            message = "Increíble! Has conseguido las 30 reliquias. \nComo te lo prometí, a partir de ahora cada vez que llegues a puerto me encargaré personalmente de rellenar tu munición.";

        dialogo.text = message;
    }
}
