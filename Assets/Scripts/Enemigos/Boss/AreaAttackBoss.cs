using UnityEngine;
using System.Collections;

public class AreaAttckBoss : MonoBehaviour
{
    public float scaleDuration = 0.3f;  // Tiempo en segundos para escalar de 0 a 1
    public float blinkDuration = 0.25f;  // Tiempo en segundos que parpadea antes de hacer daño
    public float damageDuration = 0.35f;   // Tiempo en segundos que el área permanece activa para hacer daño
    public int dano = 15;                // Daño que inflige al jugador

    private Renderer objectRenderer;
    private Collider areaCollider;
    private bool canDealDamage = false;

    void Start()
    {
        Destroy(gameObject, 5);
        objectRenderer = GetComponent<Renderer>();
        areaCollider = GetComponent<Collider>();

        transform.localScale = Vector3.zero; // Iniciar el objeto con escala 0
        StartCoroutine(ExpandAndBlink());
    }

    IEnumerator ExpandAndBlink()
    {
        // Fase 1: Expandir el objeto hasta tamaño 1
        float elapsedTime = 0f;
        while (elapsedTime < scaleDuration)
        {
            float scale = Mathf.Lerp(0, 1, elapsedTime / scaleDuration);
            transform.localScale = new Vector3(scale, scale, scale);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = Vector3.one; // Asegurar tamaño final

        // Fase 2: Parpadeo antes de hacer daño
        for (int i = 0; i < 5; i++) // 4 cambios en 0.5s = Parpadeo rápido
        {
            objectRenderer.enabled = !objectRenderer.enabled;
            yield return new WaitForSeconds(blinkDuration / 5);
        }
        objectRenderer.enabled = true;

        // Fase 3: Activar daño por un tiempo
        canDealDamage = true;
        yield return new WaitForSeconds(damageDuration);

        // Fase 4: Destruir el objeto
        Destroy(gameObject);
    }

    void OnTriggerStay(Collider other)
    {
        if (canDealDamage && other.CompareTag("Player"))
        {
            VidaJugador playerHealth = other.GetComponent<VidaJugador>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(dano);
                Debug.Log("¡El jugador ha recibido daño por el ataque del enemigo!");
            }
        }
    }

}
