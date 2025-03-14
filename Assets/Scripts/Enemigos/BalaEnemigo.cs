using UnityEngine;

public class BalaEnemigo : MonoBehaviour
{
    public GameObject impactEffectPrefab; // Prefab de explosion
    public int dano;
    void Start()
    {
        Destroy(gameObject, 3f); // La bala se destruye 3 segundos tras ser disparada
    }

    void Update()
    {

    }
    //Impacto de bala
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Si impacta con el jugador
        {
            VidaJugador playerHealth = other.GetComponent<VidaJugador>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(dano);
                Debug.Log("¡El jugador ha recibido daño por el ataque del enemigo!");
            }
            //Crear la explosion
            if (impactEffectPrefab != null)
            {
                Instantiate(impactEffectPrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject); // Destruir la bala
        }
    }
}
