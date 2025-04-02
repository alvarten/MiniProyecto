using UnityEngine;

public class Explosion : MonoBehaviour
{
    public AudioClip explosionSound; // Clip de sonido de la explosi�n
    private AudioSource audioSource;    

    void Start()
    {
        PlayExplosionSound();
    }

    void PlayExplosionSound()
    {
        if (explosionSound != null)
        {
            // Agregar y configurar el AudioSource
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = 1f;  // Sonido 3D
            audioSource.minDistance = 5f;
            audioSource.maxDistance = 50f;
            audioSource.volume = PlayerPrefs.GetFloat("Volume", 1f);
            audioSource.loop = false;
            audioSource.clip = explosionSound;

            // Configurar la reproducci�n desde startTime
            audioSource.time = 0.6f;
            audioSource.Play();

            // Programar la detenci�n en endTime
            audioSource.SetScheduledEndTime(AudioSettings.dspTime + (2));
        }
    }

    public void DestroyAfterAnimation()
    {
        Destroy(gameObject); // Destruye la explosi�n tras la animaci�n
    }
}
