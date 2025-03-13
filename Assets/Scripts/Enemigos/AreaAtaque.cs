using UnityEngine;

public class AreaAtaque : MonoBehaviour
{
    public float destroyTime = 0.2f; // Tiempo antes de destruir el objeto

    void Start()
    {
        Destroy(gameObject, destroyTime); // Destruye el objeto
    }
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
