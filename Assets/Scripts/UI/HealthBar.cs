using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class HealthBar : MonoBehaviour
{
    public Image lifeMaskImage100; // Imagen de la barra de vida
    public Image lifeMaskImage150; // Imagen de la barra de vida
    public Image lifeMaskImage200; // Imagen de la barra de vida
    public Transform lifeMask;   // Objeto que tapa la barra de vida
    public Sprite sprite100; // Sprite cuando la vida máxima es 100
    public Sprite sprite150; // Sprite cuando la vida máxima es 150
    public Sprite sprite200; // Sprite cuando la vida máxima es 200
    public Image healthBarSpriteRenderer; // Imagen que muestra la barra de vida

    private float vidaMaxima;
    private float vidaActual;
    private Vector3 maskStartScale; // Escala inicial del objeto negro

    void Start()
    {
        vidaMaxima = PlayerPrefs.GetFloat("vidaMaxima", 100f);
        vidaActual = PlayerPrefs.GetFloat("vidaActual", vidaMaxima);

        // Para testear
        //SetMaxHealth(200);
        //TakeDamage(0);
        UpdateHealthUI();
    }
    void Update()
    {        
        UpdateHealthUI();        
    }
    public void TakeDamage(float damage)
    {
        vidaActual -= damage;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);
        PlayerPrefs.SetFloat("vidaActual", vidaActual);
        UpdateHealthUI();
    }

    public void SetMaxHealth(float newMaxHealth)
    {
        vidaMaxima = newMaxHealth;
        PlayerPrefs.SetFloat("vidaMaxima", vidaMaxima);
        vidaActual = vidaMaxima; // Rellenar la vida al cambiar la máxima
        PlayerPrefs.SetFloat("vidaActual", vidaActual);
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        // Calcular lo que falta para llegar a la vida máxima
        float healthMissing = vidaMaxima - vidaActual;

        // Calcular el porcentaje de vida faltante
        float healthPercentage = healthMissing / vidaMaxima;

        // Asegurar que el valor esté entre 0 y 1
        healthPercentage = Mathf.Clamp01(healthPercentage);



        // Cambia el sprite de la barra de vida según la vida máxima
        if (healthBarSpriteRenderer != null)
        {
            if (vidaMaxima == 100) {
                healthBarSpriteRenderer.sprite = sprite100;

                // Asegurarnos de que solo esté activa la barra necesaria
                lifeMaskImage100.gameObject.SetActive(true);
                lifeMaskImage150.gameObject.SetActive(false);
                lifeMaskImage200.gameObject.SetActive(false);

                // Aplicar el relleno de derecha a izquierda
                if (lifeMaskImage100 != null)
                {
                    lifeMaskImage100.fillAmount = healthPercentage;
                }
            }
            else if (vidaMaxima == 150)
            {

                healthBarSpriteRenderer.sprite = sprite150;

                // Asegurarnos de que solo esté activa la barra necesaria
                lifeMaskImage100.gameObject.SetActive(false);
                lifeMaskImage150.gameObject.SetActive(true);
                lifeMaskImage200.gameObject.SetActive(false);

                // Aplicar el relleno de derecha a izquierda
                if (lifeMaskImage150 != null)
                {
                    lifeMaskImage150.fillAmount = healthPercentage;
                }
            }            
            else if (vidaMaxima == 200)
            {
                healthBarSpriteRenderer.sprite = sprite200;

                // Asegurarnos de que solo esté activa la barra necesaria
                lifeMaskImage100.gameObject.SetActive(false);
                lifeMaskImage150.gameObject.SetActive(false);
                lifeMaskImage200.gameObject.SetActive(true);

                // Aplicar el relleno de derecha a izquierda
                if (lifeMaskImage200 != null)
                {
                    lifeMaskImage200.fillAmount = healthPercentage;
                }
            }
            
        }
     
        Debug.Log("Vida Actual: " + vidaActual + " Vida Maxima: " + vidaMaxima + " Porcentaje de vida: " + healthPercentage);

    }
}

