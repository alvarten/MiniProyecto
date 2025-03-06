using UnityEngine;

public class Melee : MonoBehaviour
{
    public float attackRange = 1.5f; // Rango de ataque
    public float attackCooldown = 1.5f; // Tiempo entre ataques
    public GameObject attackAreaPrefab; // Prefab del �rea de ataque
    public float attackDuration = 0.2f; // Tiempo que el �rea de ataque ser� visible
    private float lastAttackTime;
    private Transform player;
    void Start()
    {
        // Buscar autom�ticamente al jugador
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("No se encontr� un objeto con el tag 'Player'. Aseg�rate de que tu jugador tiene ese tag.");
        }
    }

    void Update()
    {
        if (player == null) return;

        // Calcular la distancia al jugador
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Si el jugador est� dentro del rango de ataque y el cooldown ha pasado, atacar
        if (distanceToPlayer <= attackRange && Time.time > lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time; // Reiniciar el cooldown del ataque
        }
    }

    void Attack()
    {
        Debug.Log("�El enemigo ataca!");

        // Crear el �rea de ataque en la posici�n del enemigo
        GameObject attackArea = Instantiate(attackAreaPrefab, transform.position, Quaternion.identity);

        // Destruir el �rea de ataque despu�s de cierto tiempo
        Destroy(attackArea, attackDuration);
    }

    // Dibujar la zona de ataque en la escena (para visualizar el rango)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
