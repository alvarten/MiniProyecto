using UnityEngine;

public class LootPickup : MonoBehaviour
{
    public int coins; // Cantidad de monedas en este loot
    public int chests; // Cantidad de cofres en este loot

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Detectar si el jugador toca el loot
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins", 0) + coins);
            PlayerPrefs.SetInt("Chests", PlayerPrefs.GetInt("Chests", 0) + chests);

            //Debug.Log("Recogiste " + coins + " monedas y " + chests + " cofres.");

            Destroy(gameObject); // Eliminar el loot después de recogerlo
        }
    }
}
