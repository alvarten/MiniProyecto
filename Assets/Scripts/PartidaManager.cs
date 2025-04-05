using UnityEngine;

public class PartidaManager : MonoBehaviour
{
    void Update()
    {
        // Hacer pruebas de reseteo de variables
        //PlayerPrefs.SetInt("inicioPartida", 1);
        //PlayerPrefs.Save();

        // Al iniciar partida se setean las variables de PlayerPrefs
        if (PlayerPrefs.GetInt("inicioPartida", 1) == 1)
        {
            PlayerPrefs.SetInt("IntroduccionTexto", 0);
            PlayerPrefs.SetInt("CannonBallAmmo", 15);
            PlayerPrefs.SetInt("MaxCannonBallAmmo", 50);
            PlayerPrefs.SetInt("HarpoonAmmo", 15);
            PlayerPrefs.SetInt("MaxHarpoonAmmo", 50);
            PlayerPrefs.SetInt("Coins", 200);
            PlayerPrefs.SetInt("Chests", 0);
            PlayerPrefs.SetInt("CannonBallDamage", 2);
            PlayerPrefs.SetInt("HarpoonDamage", 2);
            PlayerPrefs.SetFloat("vidaMaxima", 100f);
            PlayerPrefs.SetInt("Relics", 0);
            PlayerPrefs.SetInt("progresoHenry", 0);
            PlayerPrefs.SetInt("VidaBoss", 60);
            PlayerPrefs.SetFloat("Speed", 5f);
            PlayerPrefs.SetFloat("vidaActual", 100f);
            PlayerPrefs.SetFloat("vidaMaxima", 100f);
            PlayerPrefs.SetInt("PantallaControles", 1);
            PlayerPrefs.SetInt("inicioPartida", 0);
            PlayerPrefs.Save();
        }
    }    
}
