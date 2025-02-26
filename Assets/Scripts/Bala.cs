using UnityEngine;

public class Bala : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 3f); // La bala se destruye 3 segundos tras ser disparada
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
