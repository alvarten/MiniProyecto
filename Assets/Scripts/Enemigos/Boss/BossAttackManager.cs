using UnityEngine;
using System.Collections;

public class BossAttackManager : MonoBehaviour
{
    public Animator animator; // Referencia al Animator del Boss

    public GameObject ataquePrefab; // Prefab del área de ataque
    public Transform golpeDerechoSpawnPoint; // Punto donde aparecerá el área de ataque derecho
    public Transform golpeIzquierdoSpawnPoint; // Punto donde aparecerá el área de ataque izquierdo
    public GameObject bulletPrefab; // Prefab de la bala
    public Transform bulletSpawnPoint; // Punto de spawn de las balas
    public float bulletForce = 10f; // Fuerza aplicada a las balas
    public GameObject cangrejoPrefab; // Prefab de cangrejo

    // Variables de sonido
    public AudioSource audioSource;
    public AudioClip disparoClip;
    public AudioClip hitClip;


    public float probIdle = 0.1f;
    public float probDisparar = 0.9f;
    public float probGolpeDerecho = 0.2f;
    public float probGolpeIzquierdo = 0.2f;
    public float probGolpeFrontal = 0.2f;

    public float cooldown = 1f; // Tiempo de espera entre ataques
    private bool isAttacking = false; // Control de estado
    private string lastAttack = ""; // Para evitar ataques consecutivos de la misma animación

    void Start()
    {
        InvokeRepeating("GestorAtaques", 1f, 1.3f); // Intenta atacar cada 1.3 segundos        
    }

    void GestorAtaques()
    {      
        if (isAttacking) return; // Si está en cooldown, no ataca
        // Reseteamos a la animación base
        animator.Play("Idle", 0, 0);
        float randomValue = Random.value; // Número entre 0 y 1

        // Determinamos qué acción se ejecutará
        if (randomValue < probIdle)
        {
            StartCoroutine(Idle());
        }
        else if (randomValue < probIdle + probDisparar)
        {
            StartCoroutine(Disparar());
        }
        else if (randomValue < probIdle + probDisparar + probGolpeDerecho)
        {
            StartCoroutine(GolpeDerecho());
        }
        else if (randomValue < probIdle + probDisparar + probGolpeDerecho + probGolpeIzquierdo)
        {
            StartCoroutine(GolpeIzquierdo());
        }
        else
        {
            StartCoroutine(GolpeFrontal());
        }
    }

    IEnumerator Idle()
    {
        isAttacking = true;
        Debug.Log("El Boss está en estado Idle.");

        animator.Play("Idle",0,0); // Usamos Play para reproducir la animación Idle
        lastAttack = "Idle"; // Guardar la última animación ejecutada
        
        
        yield return new WaitForSeconds(cooldown); // Tiempo de espera antes de permitir el siguiente ataque
        isAttacking = false;
    }

    IEnumerator Disparar()
    {
        isAttacking = true;
        Debug.Log("El Boss ha disparado.");

        animator.Play("Idle", 0, 0); // Usamos Play para reproducir la animación Idle para disparar
        lastAttack = "Disparar"; // Guardar la última animación ejecutada

        // Disparar las 3 balas
        DispararBala(180);    // Recto
        DispararBala(210);   // Derecha
        DispararBala(-210);  // Izquierda

        yield return new WaitForSeconds(cooldown); // Tiempo de espera antes de permitir el siguiente ataque
        isAttacking = false;
    }

    IEnumerator GolpeDerecho()
    {
        isAttacking = true;
        Debug.Log("El Boss ha lanzado un Golpe Derecho.");

        animator.Play("RightAttack", 0, 0); // Usamos Play para reproducir la animación de GolpeDerecho
        lastAttack = "GolpeDerecho"; // Guardar la última animación ejecutada

        // Instanciamos el área de ataque 10 unidades delante del Boss
        if (ataquePrefab != null)
        {
            PlayHitClip();
            Instantiate(ataquePrefab, golpeDerechoSpawnPoint.position, golpeDerechoSpawnPoint.rotation);
            Instantiate(cangrejoPrefab, golpeDerechoSpawnPoint.position, golpeDerechoSpawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("No se ha asignado el prefab del ataque en el Inspector.");
        }

        yield return new WaitForSeconds(cooldown); // Tiempo de espera antes de permitir el siguiente ataque
        isAttacking = false;
    }

    IEnumerator GolpeIzquierdo()
    {
        isAttacking = true;
        Debug.Log("El Boss ha lanzado un Golpe Izquierdo.");

        animator.Play("LeftAttack", 0, 0); // Usamos Play para reproducir la animación de GolpeIzquierdo
        lastAttack = "GolpeIzquierdo"; // Guardar la última animación ejecutada

        // Instanciamos el área de ataque 10 unidades delante del Boss
        if (ataquePrefab != null)
        {
            PlayHitClip();
            Instantiate(ataquePrefab, golpeIzquierdoSpawnPoint.position, golpeIzquierdoSpawnPoint.rotation);
            Instantiate(cangrejoPrefab, golpeIzquierdoSpawnPoint.position, golpeIzquierdoSpawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("No se ha asignado el prefab del ataque en el Inspector.");
        }

        yield return new WaitForSeconds(cooldown); // Tiempo de espera antes de permitir el siguiente ataque
        isAttacking = false;
    }

    IEnumerator GolpeFrontal()
    {
        isAttacking = true;
        Debug.Log("El Boss ha lanzado un Golpe Frontal.");
        
        animator.Play("BothAttack", 0, 0); // Usamos Play para reproducir la animación de GolpeFrontal
        lastAttack = "GolpeFrontal"; // Guardar la última animación ejecutada

        // Instanciamos el área de ataque 10 unidades delante del Boss
        if (ataquePrefab != null)
        {
            PlayHitClip();
            Instantiate(ataquePrefab, golpeDerechoSpawnPoint.position, golpeDerechoSpawnPoint.rotation);
            Instantiate(cangrejoPrefab, golpeDerechoSpawnPoint.position, golpeDerechoSpawnPoint.rotation);
            Instantiate(ataquePrefab, golpeIzquierdoSpawnPoint.position, golpeIzquierdoSpawnPoint.rotation);
            Instantiate(cangrejoPrefab, golpeIzquierdoSpawnPoint.position, golpeIzquierdoSpawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("No se ha asignado el prefab del ataque en el Inspector.");
        }

        yield return new WaitForSeconds(cooldown); // Tiempo de espera antes de permitir el siguiente ataque
        isAttacking = false;
    }

    //Metodo de disparo de balas
    void DispararBala(float angleOffset)
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        PlayDisparoClip();
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Calcular la dirección con el ángulo modificado
            Vector3 direction = Quaternion.Euler(0, angleOffset, 0) * bulletSpawnPoint.forward;
            rb.AddForce(direction * bulletForce, ForceMode.Impulse);
        }
    }

    void PlayDisparoClip()
    {
        if (disparoClip != null)
        {
            // Agregar un AudioSource si no existe
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = 1f; // 3D Sound
            audioSource.minDistance = 5f;  // Distancia mínima antes de atenuarse
            audioSource.maxDistance = 50f; // Distancia máxima de audición
            audioSource.volume = 0.7f * PlayerPrefs.GetFloat("Volume", 1f); // Ajusta al volumen general
            audioSource.clip = disparoClip;
            audioSource.time = 0.7f; // Iniciar en el segundo 0.7
            audioSource.Play();
            audioSource.SetScheduledEndTime(AudioSettings.dspTime + (3.5 - 0.7));
        }
    }
    void PlayHitClip()
    {
        if (disparoClip != null)
        {
            // Agregar un AudioSource si no existe
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = 1f; // 3D Sound
            audioSource.minDistance = 50f;  // Distancia mínima antes de atenuarse
            audioSource.maxDistance = 500f; // Distancia máxima de audición
            audioSource.volume = 0.7f * PlayerPrefs.GetFloat("Volume", 1f); // Ajusta al volumen general
            audioSource.clip = hitClip;
            audioSource.time = 0.15f; // Iniciar en el segundo 0.7
            audioSource.Play();
            audioSource.SetScheduledEndTime(AudioSettings.dspTime + (1.8 - 0.2));
        }
    }

}
