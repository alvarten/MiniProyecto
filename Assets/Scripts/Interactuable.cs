using UnityEngine;
using UnityEngine.Events;

public class Interactuable : MonoBehaviour
{
    public GameObject elementToShow; // Elemento que se hará visible
    public UnityEvent onInteract;    // Evento para ejecutar la función
    private bool isPlayerNearby = false;

    void Start()
    {
        if (elementToShow != null)
            elementToShow.SetActive(false); // Ocultar al inicio
    }

    //Mostrar el elemento Interactuable
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            if (elementToShow != null)
                elementToShow.SetActive(true); 
            isPlayerNearby = true;
        }
    }
    //Ocultar el elemento Interactuable
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (elementToShow != null)
                elementToShow.SetActive(false); 
            isPlayerNearby = false;
        }
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            onInteract.Invoke(); // Ejecuta la función
        }
    }
}
