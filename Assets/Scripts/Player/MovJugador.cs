using UnityEngine;

public class MovJugador : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 5f;
    public float gravity = -9.81f;
    public Animator animator;

    public AudioSource footstepsAudio;  // AudioSource para el sonido de los pasos
    public float footstepCooldown = 0.5f; // Tiempo entre cada paso
    private float lastFootstepTime = 0f;  // Última vez que se reprodujo un sonido de paso

    private Vector3 velocity;
    private bool isMoving = false;
    private bool facingRight = true;

    void Update()
    {
        // Verificar si el personaje está en el suelo
        bool isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Pequeño ajuste para evitar acumulación de gravedad
        }

        // Obtener entrada del teclado
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        // Mover el personaje en la dirección deseada
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * speed * Time.deltaTime);

        // Aplicar gravedad
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Determinar si el jugador se está moviendo
        isMoving = (moveX != 0 || moveZ != 0);
        animator.SetBool("isWalking", isMoving);

        // Manejar el sonido de los pasos
        HandleFootstepSound();

        // Cambiar dirección del sprite si se mueve en el eje X
        if (moveX > 0 && !facingRight) Flip();
        else if (moveX < 0 && facingRight) Flip();
    }

    void HandleFootstepSound()
    {
        if (isMoving)
        {
            // Solo reproducir si ha pasado suficiente tiempo desde el último sonido
            if (Time.time >= lastFootstepTime + footstepCooldown)
            {
                footstepsAudio.Play();
                lastFootstepTime = Time.time; // Guardar el tiempo actual
            }
        }
        else
        {
            // Detener el sonido si el jugador deja de moverse
            footstepsAudio.Stop();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }
}
