using UnityEngine;

public class IntroduccionManager : MonoBehaviour
{
    public GameObject panelIntroduccion; // Asigna el panel desde el inspector
    public ShopManager shopManager; // Referencia a otro script

    void Start()
    {
        if (PlayerPrefs.GetInt("IntroduccionTexto", 0) == 0)
        {
            shopManager.ShowPanel(panelIntroduccion);    

            // Actualiza el valor para que no vuelva a mostrarse
            PlayerPrefs.SetInt("IntroduccionTexto", 1);
            PlayerPrefs.Save();
        }
    }


}
