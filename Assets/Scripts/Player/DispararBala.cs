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


    //private bool isCannonBall = true; // Controla el tipo de bala
    private Vector3 lastMoveDirection = Vector3.right; // Última dirección de movimiento

    // Para gestionar el audio
    public AudioClip fireSoundCanon;  // Sonido de disparo canon
    public AudioClip fireSoundHarpon;  // Sonido de disparo harpon
    private AudioSource audioSource; // Fuente de sonido


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
            bool currentWeapon = PlayerPrefs.GetInt("isCannonBall", 1) == 1; // Obtener el estado actual
            bool newWeapon = !currentWeapon; // Cambiar el estado
            PlayerPrefs.SetInt("isCannonBall", newWeapon ? 1 : 0); // Guardar el nuevo estado
            PlayerPrefs.Save(); 
            Debug.Log("Tipo de bala cambiado a: " + (newWeapon ? "Cañón" : "Arpón"));
        }
    }
    //Gestionar el bool de tipo de municion
    public bool IsCannonBall()
    {
        return PlayerPrefs.GetInt("isCannonBall", 0) == 1; // 1 es true, 0 es false
    }

    public void SetIsCannonBall(bool value)
    {
        PlayerPrefs.SetInt("isCannonBall", value ? 1 : 0);
        PlayerPrefs.Save(); // Guarda los cambios
    }

    void HandleShooting()
    {
        // Si el tiempo de cooldown ha pasado, permitir el disparo
        if (Time.time >= nextFireTime)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) // Disparar
            {
                // Obtener el estado actual de isCannonBall desde PlayerPrefs
                bool isCannonBall = PlayerPrefs.GetInt("isCannonBall", 1) == 1;

                GameObject bulletPrefab = isCannonBall ? cannonBallPrefab : harpoonPrefab;
                Transform firePoint = GetFirePoint();

                // Verificar la munición antes de disparar
                if (isCannonBall)
                {
                    int cannonAmmo = PlayerPrefs.GetInt("CannonBallAmmo", 10);
                    if (cannonAmmo > 0)
                    {
                        PlayerPrefs.SetInt("CannonBallAmmo", cannonAmmo - 1); // Restar una bala
                        PlayerPrefs.Save(); // Guardar cambios
                        FireBullet(bulletPrefab, firePoint);
                    }
                    else
                    {
                        Debug.Log("No hay munición de cañón disponible.");
                    }
                }
                else
                {
                    int harpoonAmmo = PlayerPrefs.GetInt("HarpoonAmmo", 5);
                    if (harpoonAmmo > 0)
                    {
                        PlayerPrefs.SetInt("HarpoonAmmo", harpoonAmmo - 1); // Restar un arpón
                        PlayerPrefs.Save(); // Guardar cambios
                        FireBullet(bulletPrefab, firePoint);
                    }
                    else
                    {
                        Debug.Log("No hay munición de arpón disponible.");
                    }
                }

                nextFireTime = Time.time + fireRate; // Reiniciar el cooldown
            }
        }
    }
    void FireBullet(GameObject bulletPrefab, Transform firePoint)
    {
        if (firePoint != null)
        {
            // Obtener el tipo de proyectil desde PlayerPrefs
            bool isCannonBall = PlayerPrefs.GetInt("isCannonBall", 1) == 1;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

            // Para reproducir el sonido de disparo
            // Agregar un AudioSource si no existe
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = 1f; // 3D Sound
            audioSource.minDistance = 5f;  // Distancia mínima antes de atenuarse
            audioSource.maxDistance = 50f; // Distancia máxima de audición
            audioSource.volume = 0.7f * PlayerPrefs.GetFloat("Volume", 1f); // Ajusta al volumen general

            if (isCannonBall)
            {
                // Reproducir sonido de canon
                if (fireSoundCanon != null)
                {
                    audioSource.clip = fireSoundCanon;
                    audioSource.time = 0.7f; // Iniciar en el segundo 0.7
                    audioSource.Play();
                    audioSource.SetScheduledEndTime(AudioSettings.dspTime + (3.5 - 0.7));
                }
            }else if (!isCannonBall)
            {
                // Reproducir sonido de harpoon
                if (fireSoundHarpon != null)
                {
                    audioSource.clip = fireSoundHarpon;
                    audioSource.time = 2.7f; // Iniciar en el segundo 0.7
                    audioSource.Play();
                    audioSource.SetScheduledEndTime(AudioSettings.dspTime + (3.5 - 2.7));
                }
            }
            


            //Debug.Log("Bala " + PlayerPrefs.GetInt("CannonBallAmmo", 50) + " disparada");
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

        // Crear un objeto temporal para almacenar la posición ajustada (resolucion de un bug al dispara las balas)
        GameObject tempFirePoint = new GameObject("TempFirePoint");

        if (lastMoveDirection == Vector3.left)
        {
            tempFirePoint.transform.position = new Vector3(leftFirePoint.position.x, 0.58f, transform.position.z);
        }
        else if (lastMoveDirection == Vector3.right)
        {
            tempFirePoint.transform.position = new Vector3(rightFirePoint.position.x, 0.58f, transform.position.z);
        }
        else
        {
            Destroy(tempFirePoint); // Si no es left ni right, no se necesita el objeto
            return rightFirePoint;  // Valor por defecto
        }

        return tempFirePoint.transform;
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
