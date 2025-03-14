using UnityEngine;

public class EnemyFlip : MonoBehaviour
{
    private Vector3 lastPosition;  // Posici�n anterior del enemigo
    private bool isFacingRight = true;  // Estado de si el enemigo est� mirando a la derecha

    void Start()
    {
        lastPosition = transform.position;  // Guardamos la posici�n inicial
    }

    void Update()
    {
        // Comparamos la posici�n actual con la anterior
        CheckMovementDirection();

        // Actualizamos la posici�n anterior
        lastPosition = transform.position;
    }

    void CheckMovementDirection()
    {
        // Si la posici�n actual est� m�s a la derecha que la anterior, el enemigo se mueve hacia la derecha
        if (transform.position.x > lastPosition.x && !isFacingRight)
        {
            Flip();  // Volteamos el sprite
        }
        // Si la posici�n actual est� m�s a la izquierda que la anterior, el enemigo se mueve hacia la izquierda
        else if (transform.position.x < lastPosition.x && isFacingRight)
        {
            Flip();  // Volteamos el sprite
        }
    }

    void Flip()
    {
        // Cambiamos el estado de si el enemigo est� mirando a la derecha
        isFacingRight = !isFacingRight;

        // Volteamos el sprite invirtiendo la escala en X
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;  // Multiplicamos la escala en X por -1 para voltear el sprite
        transform.localScale = localScale;
    }
}
