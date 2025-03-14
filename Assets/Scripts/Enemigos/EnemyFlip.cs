using UnityEngine;

public class EnemyFlip : MonoBehaviour
{
    private Vector3 lastPosition;  // Posición anterior del enemigo
    private bool isFacingRight = true;  // Estado de si el enemigo está mirando a la derecha

    void Start()
    {
        lastPosition = transform.position;  // Guardamos la posición inicial
    }

    void Update()
    {
        // Comparamos la posición actual con la anterior
        CheckMovementDirection();

        // Actualizamos la posición anterior
        lastPosition = transform.position;
    }

    void CheckMovementDirection()
    {
        // Si la posición actual está más a la derecha que la anterior, el enemigo se mueve hacia la derecha
        if (transform.position.x > lastPosition.x && !isFacingRight)
        {
            Flip();  // Volteamos el sprite
        }
        // Si la posición actual está más a la izquierda que la anterior, el enemigo se mueve hacia la izquierda
        else if (transform.position.x < lastPosition.x && isFacingRight)
        {
            Flip();  // Volteamos el sprite
        }
    }

    void Flip()
    {
        // Cambiamos el estado de si el enemigo está mirando a la derecha
        isFacingRight = !isFacingRight;

        // Volteamos el sprite invirtiendo la escala en X
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;  // Multiplicamos la escala en X por -1 para voltear el sprite
        transform.localScale = localScale;
    }
}
