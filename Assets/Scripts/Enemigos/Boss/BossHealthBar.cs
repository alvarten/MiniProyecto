using UnityEngine;
using UnityEngine.UI;


public class BossHealthBar : MonoBehaviour
{
    public Image healthBarBlack; // Imagen que tapa la barra de vida

    private int vidaMaxima;
    private int vidaActual;

    void Start()
    {
        vidaMaxima = 60;
        vidaActual = PlayerPrefs.GetInt("VidaBoss", vidaMaxima);

        UpdateHealthUI();
    }
    void Update()
    {
        UpdateHealthUI();
    }
  
    void UpdateHealthUI()
    {
        // Actualizar vida del boss actual
        vidaActual = PlayerPrefs.GetInt("VidaBoss", vidaMaxima);

        // Calcular lo que falta para llegar a la vida máxima
        float healthMissing = vidaMaxima - vidaActual;

        // Calcular el porcentaje de vida faltante
        float healthPercentage = healthMissing / vidaMaxima;

        // Asegurar que el valor esté entre 0 y 1
        healthPercentage = Mathf.Clamp01(healthPercentage);

        healthBarBlack.fillAmount = healthPercentage;


        //Debug.Log("Vida boss " + vidaActual + " Vida Maxima: " + vidaMaxima + " Porcentaje de vida: " + healthPercentage);

    }
}
