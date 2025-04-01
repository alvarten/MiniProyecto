using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public float detectionRadius = 5f;  // Radio de detección
    public string targetTag = "Enemy";  // Filtrar por etiqueta
    public AudioManager audioManager;   // Referencia al AudioManager

    private bool isBattleMusicPlaying = false; // Para evitar repetir llamadas innecesarias

    void Start()
    {
        if (audioManager == null)
        {
            audioManager = FindObjectOfType<AudioManager>();

            if (audioManager == null)
            {
                Debug.LogError("No se encontró el AudioManager en la escena. Asegúrate de que exista.");
            }
        }
    }

    void Update()
    {
        DetectEnemies();
    }

    void DetectEnemies()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);

        bool enemyDetected = false;

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(targetTag))
            {
                enemyDetected = true;
                break; // No es necesario seguir buscando más
            }
        }

        if (enemyDetected && !isBattleMusicPlaying)
        {
            Debug.Log("Enemigo detectado dentro del radio.");
            audioManager?.FadeToBattleMusic(0.7f);
            isBattleMusicPlaying = true;
        }
        else if (!enemyDetected && isBattleMusicPlaying)
        {
            Debug.Log("No hay enemigos en el radio.");
            audioManager?.FadeToChillMusic(0.7f);
            isBattleMusicPlaying = false;
        }
    }

    // Mostrar el radio en la vista de escena para depuración
    /* private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
    */
}
