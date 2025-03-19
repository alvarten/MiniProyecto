using UnityEngine;
using System.Collections;
public class VidaJugador : MonoBehaviour
{
    public float vidaMaxima;
    private float vidaActual;
    private bool canTakeDamage = true; // Indica si el jugador puede recibir da�o
    public float damageCooldown = 2f; // Tiempo de espera antes de volver a recibir da�o


    public GameObject healthBarObject; // Referencia al objeto que tiene el script HealthBar
    private HealthBar healthBarScript;

    void Start()
    {
        // Obtener la vida m�xima desde PlayerPrefs, si no existe, usar 100 por defecto
        vidaMaxima = PlayerPrefs.GetFloat("vidaMaxima", 100);

        // Obtener la vida actual desde PlayerPrefs, si no existe, establecerla igual a maxHealth
        vidaActual = PlayerPrefs.GetFloat("vidaActual", vidaMaxima);

        // Obtener el objeto de barra de vida
        healthBarScript = healthBarObject.GetComponent<HealthBar>();
    }

    public void TakeDamage(int amount)
    {
        
        if (!canTakeDamage) return; // Si no puede recibir da�o, salir

        vidaActual = PlayerPrefs.GetFloat("vidaActual");
        // Invocamos al metodo para hacer dano
        healthBarScript.TakeDamage(amount);

        Debug.Log("�El jugador ha recibido da�o! Vida restante: " + vidaActual);

        if (vidaActual <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(DamageCooldown()); // Activar la invulnerabilidad temporalmente
        }
    }

    IEnumerator DamageCooldown()
    {
        canTakeDamage = false; 
        yield return new WaitForSeconds(damageCooldown); // Esperar 1 segundo
        canTakeDamage = true; // Permitir recibir da�o nuevamente
    }

    void Die()
    {
        Debug.Log("�El jugador ha muerto!");
        // Aqu� puedes agregar l�gica para reiniciar el nivel o mostrar una pantalla de muerte
    }
}
