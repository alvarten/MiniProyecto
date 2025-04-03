using System.Collections;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public GameObject impactEffectPrefab; // Prefab de explosion
    public bool isCannonBall; // Determina si es una bala de canon o harpon    

    void Start()
    {
        Destroy(gameObject, 3f); // La bala se destruye 3 segundos tras ser disparada
        //Debug.Log("En posicion " + this.transform.position);
        
        //Ajuste de bug
        Vector3 newPosition = transform.position;
        newPosition.y = 0.58f;
        transform.position = newPosition;        
    }

    void Update()
    {
        
    }
    //Impacto de bala
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) // Si impacta con un enemigo
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>(); // Obtener el script del enemigo
            if (enemy != null)
            {
                // Obtener los valores de dano desde PlayerPrefs
                int cannonBallDamage = PlayerPrefs.GetInt("CannonBallDamage", 2);
                int harpoonDamage = PlayerPrefs.GetInt("HarpoonDamage", 2);

                int damage;

                if (isCannonBall) // En caso de disparar una bala de canon
                {
                    damage = enemy.monstruo ? cannonBallDamage / 2 : cannonBallDamage;
                    // Si impacta en un monstruo, hace la mitad del dano de la bola de canon
                }
                else // En caso de disparar un harpon
                {
                    damage = enemy.monstruo ? harpoonDamage : harpoonDamage / 2;
                    // Si impacta en un monstruo, hace dano normal, en barco hace la mitad del dano del arpon
                }

                enemy.TakeDamage(damage); // Aplica el dano correspondiente
            }
            //Crear la explosion
            if (impactEffectPrefab != null)
            {
                Instantiate(impactEffectPrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject); // Destruir la bala
        }

        if (other.CompareTag("EnemyBoss")) // Si impacta con el boss
        {
            BossHealth enemy = other.GetComponent<BossHealth>(); // Obtener el script del boss
            if (enemy != null)
            {
                // Obtener los valores de dano desde PlayerPrefs
                int cannonBallDamage = PlayerPrefs.GetInt("CannonBallDamage", 2);
                int harpoonDamage = PlayerPrefs.GetInt("HarpoonDamage", 2);

                int damage;

                if (isCannonBall) // En caso de disparar una bala de canon
                {
                    damage = enemy.monstruo ? cannonBallDamage / 2 : cannonBallDamage;
                    // Si impacta en un monstruo, hace la mitad del dano de la bola de canon
                }
                else // En caso de disparar un harpon
                {
                    damage = enemy.monstruo ? harpoonDamage : harpoonDamage / 2;
                    // Si impacta en un monstruo, hace dano normal, en barco hace la mitad del dano del arpon
                }

                enemy.TakeDamage(damage); // Aplica el dano correspondiente
            }
            //Crear la explosion
            if (impactEffectPrefab != null)
            {
                Instantiate(impactEffectPrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject); // Destruir la bala
        }
    }

    
}
