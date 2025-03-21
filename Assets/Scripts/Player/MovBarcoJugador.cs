using UnityEngine;

public class MovBarcoJugador : MonoBehaviour
{
    public CharacterController controller;    
    public float gravity = -9.81f;
    public Animator animator;

    private Vector3 velocity;
    private bool isMoving = false;
    private bool facingRight = true;

    private float speed;  // Ya no es necesario asignar un valor fijo aquí

    void Start()
    {
        // Cargar el valor de "Speed" al empezar la escena
        speed = PlayerPrefs.GetFloat("Speed", 5f);
    }
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

        // Cambiar dirección del sprite si se mueve en el eje X
        if (moveX > 0 && !facingRight) Flip();
        else if (moveX < 0 && facingRight) Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }
}
