using UnityEngine;
using System.Collections;

public class BossAttackManager : MonoBehaviour
{
    public Animator animator; // Referencia al Animator del Boss

    public float probIdle = 0.1f;
    public float probDisparar = 0.3f;
    public float probGolpeDerecho = 0.2f;
    public float probGolpeIzquierdo = 0.2f;
    public float probGolpeFrontal = 0.2f;

    public float cooldown = 1f; // Tiempo de espera entre ataques
    private bool isAttacking = false; // Control de estado
    private string lastAttack = ""; // Para evitar ataques consecutivos de la misma animaci�n

    void Start()
    {
        InvokeRepeating("GestorAtaques", 1f, 1.3f); // Intenta atacar cada 1.3 segundos
    }

    void GestorAtaques()
    {      
        if (isAttacking) return; // Si est� en cooldown, no ataca
        // Reseteamos a la animaci�n base
        animator.Play("Idle", 0, 0);
        float randomValue = Random.value; // N�mero entre 0 y 1

        // Determinamos qu� acci�n se ejecutar�
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
        Debug.Log("El Boss est� en estado Idle.");

        animator.Play("Idle",0,0); // Usamos Play para reproducir la animaci�n Idle
        lastAttack = "Idle"; // Guardar la �ltima animaci�n ejecutada
        
        
        yield return new WaitForSeconds(cooldown); // Tiempo de espera antes de permitir el siguiente ataque
        isAttacking = false;
    }

    IEnumerator Disparar()
    {
        isAttacking = true;
        Debug.Log("El Boss ha disparado.");

        animator.Play("Idle", 0, 0); // Usamos Play para reproducir la animaci�n Idle para disparar
        lastAttack = "Disparar"; // Guardar la �ltima animaci�n ejecutada
        
        
        yield return new WaitForSeconds(cooldown); // Tiempo de espera antes de permitir el siguiente ataque
        isAttacking = false;
    }

    IEnumerator GolpeDerecho()
    {
        isAttacking = true;
        Debug.Log("El Boss ha lanzado un Golpe Derecho.");

        animator.Play("RightAttack", 0, 0); // Usamos Play para reproducir la animaci�n de GolpeDerecho
        lastAttack = "GolpeDerecho"; // Guardar la �ltima animaci�n ejecutada
        
        
        yield return new WaitForSeconds(cooldown); // Tiempo de espera antes de permitir el siguiente ataque
        isAttacking = false;
    }

    IEnumerator GolpeIzquierdo()
    {
        isAttacking = true;
        Debug.Log("El Boss ha lanzado un Golpe Izquierdo.");

        animator.Play("LeftAttack", 0, 0); // Usamos Play para reproducir la animaci�n de GolpeIzquierdo
        lastAttack = "GolpeIzquierdo"; // Guardar la �ltima animaci�n ejecutada
        
        
        yield return new WaitForSeconds(cooldown); // Tiempo de espera antes de permitir el siguiente ataque
        isAttacking = false;
    }

    IEnumerator GolpeFrontal()
    {
        isAttacking = true;
        Debug.Log("El Boss ha lanzado un Golpe Frontal.");
        
        animator.Play("BothAttack", 0, 0); // Usamos Play para reproducir la animaci�n de GolpeFrontal
        lastAttack = "GolpeFrontal"; // Guardar la �ltima animaci�n ejecutada
        
        
        yield return new WaitForSeconds(cooldown); // Tiempo de espera antes de permitir el siguiente ataque
        isAttacking = false;
    }
}
