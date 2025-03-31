using UnityEngine;

public class BarcoDisparo : MonoBehaviour
{
    public float detectionDistance = 20f;   // Distancia a la que el barco detecta al jugador
    public float shootCooldown = 2f;        // Tiempo entre disparos
    public float velocidadBala = 15f;       // Velocidad de la bala
    public Transform player;                // Referencia al jugador
    public GameObject bulletPrefab;         // Prefab de la bala que el barco dispara
    public LayerMask playerLayer;           // Capa que representa al jugador
    public Transform firePointUp;           // Punto de disparo para arriba
    public Transform firePointDown;         // Punto de disparo para abajo
    public Transform firePointLeft;         // Punto de disparo para izquierda
    public Transform firePointRight;        // Punto de disparo para derecha
    public float shootAngleTolerance = 15f; // �ngulo de tolerancia para considerar que el jugador est� en el mismo eje

    private float timeSinceLastShot = 0f;   // Tiempo transcurrido desde el �ltimo disparo

    void Update()
    {
        // Actualizar el tiempo entre disparos
        timeSinceLastShot += Time.deltaTime;

        // Detectar si el jugador est� dentro del rango de disparo
        if (IsPlayerInRange() && IsPlayerInLine())
        {
            // Verificar si ha pasado el tiempo de recarga para disparar
            if (timeSinceLastShot >= shootCooldown)
            {
                ShootAtPlayer();
                timeSinceLastShot = 0f; // Reiniciar el temporizador de disparo
            }
        }
    }

    // M�todo para verificar si el jugador est� dentro del rango de detecci�n
    bool IsPlayerInRange()
    {
        // Verificar la distancia entre el barco y el jugador
        float distance = Vector3.Distance(transform.position, player.position);
        return distance <= detectionDistance;
    }

    // M�todo para verificar si el jugador est� alineado con alguno de los ejes del barco (izquierda, derecha, arriba, abajo)
    bool IsPlayerInLine()
    {
        Vector3 directionToPlayer = player.position - transform.position;

        // Verificar si el jugador est� en el mismo eje (izquierda, derecha, arriba, abajo)
        bool isAligned = false;

        if (Mathf.Abs(directionToPlayer.x) <= shootAngleTolerance) // Verifica si est� cerca del eje X (izquierda / derecha)
        {
            isAligned = true;
        }
        else if (Mathf.Abs(directionToPlayer.z) <= shootAngleTolerance) // Verifica si est� cerca del eje Z (arriba / abajo)
        {
            isAligned = true;
        }

        return isAligned;
    }

    // M�todo para disparar al jugador en la direcci�n correcta
    void ShootAtPlayer()
    {
        if (bulletPrefab != null)
        {
            // Determinar el punto de disparo y la direcci�n de la bala
            Transform firePoint = null;

            // Elegir el punto de disparo basado en la direcci�n del jugador
            Vector3 directionToPlayer = player.position - transform.position;

            if (Mathf.Abs(directionToPlayer.x) <= shootAngleTolerance) // Jugador est� alineado en el eje X (izquierda/derecha)
            {
                if (directionToPlayer.x > 0)  // Jugador a la derecha
                {
                    firePoint = firePointRight;
                }
                else  // Jugador a la izquierda
                {
                    firePoint = firePointLeft;
                }
            }
            else if (Mathf.Abs(directionToPlayer.z) <= shootAngleTolerance) // Jugador est� alineado en el eje Z (arriba/abajo)
            {
                if (directionToPlayer.z > 0)  // Jugador arriba
                {
                    firePoint = firePointUp;
                }
                else  // Jugador abajo
                {
                    firePoint = firePointDown;
                }
            }

            // Si se ha elegido un punto de disparo
            if (firePoint != null)
            {
                // Instanciar la bala en el punto de disparo
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

                // Calcular la direcci�n hacia el jugador
                Vector3 direction = (player.position - firePoint.position).normalized;

                // Configurar la direcci�n de la bala (con la velocidad adecuada)
                bullet.GetComponent<Rigidbody>().linearVelocity = direction * velocidadBala; // Cambia la velocidad segun lo necesites

                //Debug.Log("El barco ha disparado al jugador en la direcci�n: " + direction);
            }
        }
    }
}
