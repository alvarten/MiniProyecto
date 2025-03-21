using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 3; // Vida del enemigo
    public GameObject objectToDestroy; // Objeto a destruir

    //Valores maximos y minimos de loot del enemigo
    public int minCoins = 10, maxCoins = 50;
    public int minChests = 0, maxChests = 1;

    public GameObject lootPrefab; // Prefab del loot

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
        if (lootPrefab != null)
        {
            Quaternion lootRotation = Quaternion.Euler(30f, 0, 0);
            GameObject loot = Instantiate(lootPrefab, transform.position, lootRotation);
            LootPickup lootPickup = loot.GetComponent<LootPickup>();

            if (lootPickup != null)
            {
                lootPickup.coins = Random.Range(minCoins, maxCoins + 1);
                lootPickup.chests = Random.Range(minChests, maxChests + 1);
            }
        }
    }
}
