using UnityEngine;
using System.Collections;


public class Melee : MonoBehaviour
{
    public float attackRange = 1.5f; // Rango de ataque
    public float attackCooldown = 1.5f; // Tiempo entre ataques
    public GameObject attackAreaPrefab; // Prefab del área de ataque
    public float attackDelay = 0.5f; // Momento exacto en el que se crea el ataque dentro de la animación
    //public float attackDuration = 0.2f; // Tiempo que el área de ataque será visible
    private float lastAttackTime;
    private Transform player;
    public Animator animator;
    private bool isAttacking = false;

    // Variables para gestion de sonido
    public AudioClip attackSound;
    private AudioSource audioSource;
    void Start()
    {
        // Buscar automáticamente al jugador
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("No se encontró un objeto con el tag 'Player'. Asegúrate de que tu jugador tiene ese tag.");
        }

        // Agregar y configurar el AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 1f;  // Sonido 3D
        audioSource.volume = 1.4f * PlayerPrefs.GetFloat("Volume", 1f);
        audioSource.loop = false;
    }

    void Update()
    {
        if (player == null || isAttacking) return; // No hacer nada si ya está atacando

        // Calcular la distancia al jugador
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Si el jugador está en rango y el cooldown ha pasado, atacar
        if (distanceToPlayer <= attackRange && Time.time > lastAttackTime + attackCooldown)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true; // Bloquea otros ataques mientras se realiza uno
        lastAttackTime = Time.time; // Reiniciar cooldown

        animator.SetTrigger("Attack"); // Activar la animación de ataque

        //Reproducir sonido de ataque
        PlayAttackSound();


        yield return new WaitForSeconds(attackDelay); // Esperar hasta el momento exacto del golpe

        Instantiate(attackAreaPrefab, transform.position, Quaternion.identity); // Instanciar el área de ataque

        yield return new WaitForSeconds(attackCooldown - attackDelay); // Esperar el resto del cooldown
        isAttacking = false; // Permitir nuevos ataques
    }

    void PlayAttackSound()
    {
        if (attackSound != null)
        {
            audioSource.clip = attackSound;
            audioSource.time = 6.8f ; // Empieza en soundStartTime
            audioSource.Play();
            audioSource.SetScheduledEndTime(AudioSettings.dspTime + (1.3f));
        }
    }

    // Dibujar la zona de ataque en la escena (para visualizar el rango)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
