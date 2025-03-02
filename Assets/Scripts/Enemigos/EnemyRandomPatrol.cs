using UnityEngine;
using System.Collections;

public class EnemyRandomPatrol : MonoBehaviour
{
    public Transform patrolArea;  // Área en la que se moverá
    private Transform player;      // Referencia al jugador
    public float speed = 2f;      // Velocidad de movimiento
    public float chaseSpeed = 3.5f; // Velocidad al perseguir
    public float waitTime = 2f;   // Tiempo que espera antes de cambiar de destino
    public float detectionRange = 5f; // Distancia para detectar al jugador
    public float lostRange = 7f;  // Distancia para perder al jugador
    public float moveProbability = 0.7f; // Probabilidad de moverse en vez de quedarse quieto

    private Vector3 targetPosition;
    private bool isWaiting = false;
    private bool isChasing = false;
    private float areaSizeX, areaSizeZ;
    private float initialY;

    void Start()
    {
        //Buscamos al jugador
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        //Guardamos el transform del jugador
        player = playerObject.transform;

        initialY = transform.position.y;
        Debug.Log(initialY);
        // Obtener el tamaño del área de patrullaje
        BoxCollider areaCollider = patrolArea.GetComponent<BoxCollider>();
        areaSizeX = areaCollider.size.x / 2;
        areaSizeZ = areaCollider.size.z / 2;

        StartCoroutine(ChangeTarget());
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            isChasing = true;
        }
        else if (distanceToPlayer >= lostRange)
        {
            isChasing = false;
        }

        if (isChasing)
        {
            ChasePlayer();
        }
        else if (!isWaiting)
        {
            Patrol();
        }
    }

    void ChasePlayer()
    {
        targetPosition = new Vector3(player.position.x, initialY, player.position.z); // Ir hacia el jugador
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, chaseSpeed * Time.deltaTime);
    }

    void Patrol()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    IEnumerator ChangeTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime); // Espera antes de elegir un nuevo punto

            if (!isChasing) // Solo cambiar de objetivo si no está persiguiendo
            {
                if (Random.value < moveProbability) // Probabilidad de moverse o quedarse quieto
                {
                    float randomX = Random.Range(-areaSizeX, areaSizeX);
                    float randomZ = Random.Range(-areaSizeZ, areaSizeZ);
                    targetPosition = new Vector3(patrolArea.position.x + randomX, initialY, patrolArea.position.z + randomZ);
                }
                else
                {
                    targetPosition = transform.position; // Quedarse quieto
                }
            }

            isWaiting = false;
        }
    }
}
