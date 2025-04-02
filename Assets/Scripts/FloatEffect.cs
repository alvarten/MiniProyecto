using UnityEngine;

public class FloatEffect : MonoBehaviour
{    
    public float floatAmplitude = 0.5f; // Altura máxima de flotación
    public float floatSpeed = 1f;       // Velocidad del movimiento

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Movimiento senoidal para simular la flotación
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
