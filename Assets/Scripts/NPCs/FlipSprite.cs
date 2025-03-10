using System.Collections;
using UnityEngine;

public class FlipSprite : MonoBehaviour
{
    //Script destina a cambiar la direccion en la que miran los NPCs
    public float minFlipInterval = 1f; // Tiempo mínimo entre intentos
    public float maxFlipInterval = 3f; // Tiempo máximo entre intentos
    [Range(0f, 1f)] public float flipChance = 0.2f; // Probabilidad de girar 

    private bool isFacingRight = true; // Estado inicial

    void Start()
    {
        StartCoroutine(ChangeDirectionRoutine());
    }

    IEnumerator ChangeDirectionRoutine()
    {
        while (true) // Bucle infinito para repetir la acción
        {
            float waitTime = Random.Range(minFlipInterval, maxFlipInterval); // Tiempo entre intentos
            yield return new WaitForSeconds(waitTime); // Espera el tiempo generado

            // Decide si girar según la probabilidad
            if (Random.value <= flipChance)
            {
                Flip();
            }
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight; // Invierte el estado
        Vector3 newScale = transform.localScale;
        newScale.x *= -1; // Invierte el eje X
        transform.localScale = newScale; // Aplica el cambio
    }
}

