using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public float detectionRadius = 5f;  // Radio de detecci�n
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
                Debug.LogError("No se encontr� el AudioManager en la escena. Aseg�rate de que exista.");
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
                break; // No es necesario seguir buscando m�s
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

    // Mostrar el radio en la vista de escena para depuraci�n
    /* private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
    */
}
