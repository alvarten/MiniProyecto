using UnityEngine;

public class AreaAtaque : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            VidaJugador playerHealth = other.GetComponent<VidaJugador>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
                Debug.Log("¡El jugador ha recibido daño por el ataque del enemigo!");
            }
        }
    }
}
