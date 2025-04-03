using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public GameObject objectToDestroy; // Objeto a destruir

    // Definir si es parte de monstruo o de barco
    public bool monstruo;

    void Start()
    {
        // Leer la vida inicial del Boss desde PlayerPrefs
        if (!PlayerPrefs.HasKey("VidaBoss"))
        {
            PlayerPrefs.SetInt("VidaBoss", 60); // Si no tiene valor, lo inicializa en 60
        }
        PlayerPrefs.SetInt("VidaBoss", 60);// Siempre se actualiza al principio de la escena la vida del boss
    }

    public void TakeDamage(int damageAmount)
    {
        // Restar la cantidad de daño recibida de VidaBoss en PlayerPrefs
        int currentHealth = PlayerPrefs.GetInt("VidaBoss");

        currentHealth -= damageAmount; // Reducir la vida actual

        // Actualizar el valor de VidaBoss en PlayerPrefs
        PlayerPrefs.SetInt("VidaBoss", currentHealth);
        PlayerPrefs.Save(); // Guardar los cambios

        // Comprobar si la vida del Boss es menor o igual a 0
        if (currentHealth <= 0)
        {
            Destroy(objectToDestroy); // Destruir el objeto del Boss si su vida llega a 0
            Debug.Log("El Boss ha sido destruido.");
        }
        else
        {
            Debug.Log("Vida del Boss restante: " + currentHealth);
        }
    }
}

