using UnityEngine;

public class Bala : MonoBehaviour
{
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
        if (other.CompareTag("Enemy")) // Si impacta con un enemigo
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>(); // Obtener el script del enemigo
            if (enemy != null)
            {
                enemy.TakeDamage(); // Llamar a la función de daño del enemigo
            }

            Destroy(gameObject); // Destruir la bala
        }
    }
}
