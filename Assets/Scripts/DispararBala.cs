using UnityEngine;

public class DispararBala : MonoBehaviour
{
    public GameObject bulletPrefab;  // Prefab de la bala
    public Transform firePoint;  // Lugar desde donde se dispara
    public float bulletSpeed = 10f;  // Velocidad de la bala

    private Vector3 lastMoveDirection = Vector3.forward; // Dirección en la que el personaje se movió por última vez

    void Update()
    {
        // Detectar la dirección de movimiento del personaje
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Si el jugador se mueve, actualizamos la dirección de disparo
        if (moveX != 0 || moveZ != 0)
        {
            lastMoveDirection = new Vector3(moveX, 0, moveZ).normalized;
        }

        // Detectar si el jugador presiona el botón de disparo (Mouse Izquierdo o Ctrl)
        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Instanciar la bala en la posición del firePoint
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Aplicar velocidad a la bala en la dirección de movimiento
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = lastMoveDirection * bulletSpeed;
    }
}
