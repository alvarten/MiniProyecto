using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 3; // Vida del enemigo
    public GameObject objectToDestroy; // Objeto a destruir

    //Valores maximos y minimos de loot del enemigo
    public int minCoins = 10, maxCoins = 50;
    public int minChests = 0, maxChests = 1;

    //Referencia al manager de inventario
    private Inventory inventory;


    void Start()
    {
        inventory = FindObjectOfType<Inventory>(); // Encuentra el inventario en la escena
        if (inventory == null)
        {
            Debug.LogWarning("No se encontró Inventory en la escena.");
        }
    }

    public void TakeDamage()
    {
        health--; // Restar una vida

        if (health <= 0)
        {
            GiveLoot(); //El enemigo llama a la funcion para soltar loot
            Destroy(objectToDestroy); // Destruir enemigo si su vida llega a 0
        }
    }

    //Metodo para que el enemigo aporte loot
    void GiveLoot()
    {
        if (inventory != null)
        {
            int coinsToAdd = Random.Range(minCoins, maxCoins + 1);
            int chestsToAdd = Random.Range(minChests, maxChests + 1);

            inventory.AddCoins(coinsToAdd);
            Debug.Log("Añadidas " + coinsToAdd+ " monedas");
            inventory.AddChest(chestsToAdd);
            Debug.Log("Añadidos " + chestsToAdd + " cofres");
        }
    }
}
