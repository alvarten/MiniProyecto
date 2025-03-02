using UnityEngine;
using System.Collections;
public class VidaJugador : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    private bool canTakeDamage = true; // Indica si el jugador puede recibir da�o
    public float damageCooldown = 1f; // Tiempo de espera antes de volver a recibir da�o

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (!canTakeDamage) return; // Si no puede recibir da�o, salir

        currentHealth -= amount;
        Debug.Log("�El jugador ha recibido da�o! Vida restante: " + currentHealth);

        if (currentHealth <= 0)
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
