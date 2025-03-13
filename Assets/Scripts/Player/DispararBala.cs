using UnityEngine;

public class DispararBala : MonoBehaviour
{
    public GameObject cannonBallPrefab; // Prefab de la bala de cañón
    public GameObject harpoonPrefab; // Prefab del arpón
    public Transform leftFirePoint; // Punto de disparo izquierda
    public Transform rightFirePoint; // Punto de disparo derecha
    public Transform upFirePoint; // Punto de disparo arriba
    public Transform downFirePoint; // Punto de disparo abajo
    public float bulletSpeed = 10f; // Velocidad de las balas
    //Para el cooldown entre disparos
    public float fireRate = 1f;         // Cooldown entre disparos
    private float nextFireTime = 0f;    // Tiempo para el siguiente disparo


    private bool isCannonBall = true; // Controla el tipo de bala
    private Vector3 lastMoveDirection = Vector3.right; // Última dirección de movimiento

    void Start()
    {
        // Inicializar las municiones de las balas en PlayerPrefs si no existen
        if (!PlayerPrefs.HasKey("CannonBallAmmo"))
            PlayerPrefs.SetInt("CannonBallAmmo", 50); // Default 10 balas de cañón

        if (!PlayerPrefs.HasKey("HarpoonAmmo"))
            PlayerPrefs.SetInt("HarpoonAmmo", 50); // Default 5 arpones
    }
    void Update()
    {
        HandleWeaponSwitch();
        HandleShooting();
        HandleDirectionTracking();
    }

    void HandleWeaponSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isCannonBall = !isCannonBall; // Cambia el tipo de bala
            Debug.Log("Tipo de bala cambiado a: " + (isCannonBall ? "Cañón" : "Arpón"));
        }
    }

    void HandleShooting()
    {
        // Si el tiempo de cooldown ha pasado, permitir el disparo
        if (Time.time >= nextFireTime)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) // Disparar
            {
                GameObject bulletPrefab = isCannonBall ? cannonBallPrefab : harpoonPrefab;
                Transform firePoint = GetFirePoint();

                // Verificar la munición antes de disparar
                if (isCannonBall && PlayerPrefs.GetInt("CannonBallAmmo", 10) > 0)
                {
                    PlayerPrefs.SetInt("CannonBallAmmo", PlayerPrefs.GetInt("CannonBallAmmo", 10) - 1); // Restar una bala
                    FireBullet(bulletPrefab, firePoint);
                }
                else if (!isCannonBall && PlayerPrefs.GetInt("HarpoonAmmo", 5) > 0)
                {
                    PlayerPrefs.SetInt("HarpoonAmmo", PlayerPrefs.GetInt("HarpoonAmmo", 5) - 1); // Restar un arpón
                    FireBullet(bulletPrefab, firePoint);
                }
                else
                {
                    Debug.Log("No hay munición disponible.");
                }
                nextFireTime = Time.time + fireRate; // Reiniciar el cooldown
            }
        }
    }
    void FireBullet(GameObject bulletPrefab, Transform firePoint)
    {
        if (firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.linearVelocity = lastMoveDirection * bulletSpeed; // Aplica velocidad en la dirección de movimiento

            // En caso de ser un arpón, rotamos el sprite
            if (!isCannonBall)
            {
                RotateHarpoon(bullet);
            }
        }
    }
    void HandleDirectionTracking()
    {
        if (Input.GetKey(KeyCode.W)) lastMoveDirection = Vector3.forward;
        if (Input.GetKey(KeyCode.S)) lastMoveDirection = Vector3.back;
        if (Input.GetKey(KeyCode.A)) lastMoveDirection = Vector3.left;
        if (Input.GetKey(KeyCode.D)) lastMoveDirection = Vector3.right;
    }

    Transform GetFirePoint()
    {
        if (lastMoveDirection == Vector3.forward) return upFirePoint;
        if (lastMoveDirection == Vector3.back) return downFirePoint;
        if (lastMoveDirection == Vector3.left) return leftFirePoint;
        if (lastMoveDirection == Vector3.right) return rightFirePoint;

        return rightFirePoint; // Valor por defecto si no hay movimiento
    }
    void RotateHarpoon(GameObject harpoon)
    {
        float yAngle = 0f; // Rotación en el eje Y
        float xAngle = 0f; // Rotación en el eje X

        if (lastMoveDirection == Vector3.forward)
        {
            yAngle = -90f;  // Arriba
            xAngle = 90f;  // Rotamos en X para que el sprite sea visible
        }
        else if (lastMoveDirection == Vector3.back)
        {
            yAngle = 90f; // Abajo
            xAngle = 90f;  // Rotamos en X para que el sprite sea visible
        }
        else if (lastMoveDirection == Vector3.left)
        {
            yAngle = 180f; // Izquierda
        }
        else if (lastMoveDirection == Vector3.right)
        {
            yAngle = 0f;  // Derecha
        }

        harpoon.transform.rotation = Quaternion.Euler(xAngle, yAngle, 0);
    }
}
