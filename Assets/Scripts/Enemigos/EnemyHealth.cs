using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 3; // Vida del enemigo
    public GameObject objectToDestroy; // Objeto a destruir

    public void TakeDamage()
    {
        health--; // Restar una vida

        if (health <= 0)
        {
            Destroy(objectToDestroy); // Destruir enemigo si su vida llega a 0
        }
    }
}
