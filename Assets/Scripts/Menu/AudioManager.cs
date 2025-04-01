using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Range(0f, 1f)] public float menuVolume = 1f;  // Ajustable desde el Inspector
    [Range(0f, 1f)] public float puertoVolume = 0.3f; 
    [Range(0f, 1f)] public float ambianceVolume = 1f; 
    [Range(0f, 1f)] public float marChillVolume = 1f; 

    private float generalVolume;

    public AudioSource musicSource;
    public AudioSource ambianceSource;

    public AudioClip mainMenuMusic;
    public AudioClip puertoMusic;
    public AudioClip ambianceClip;
    public AudioClip marMusicChill;
    public AudioClip marMusicBattle;

    private Coroutine currentFadeCoroutine;  // Almacena la coroutine activa
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {

        musicSource = gameObject.AddComponent<AudioSource>();
        ambianceSource = gameObject.AddComponent<AudioSource>();

        musicSource.loop = true;
        ambianceSource.loop = true;

        // Sincronizar valores con PlayerPrefs
        generalVolume = PlayerPrefs.GetFloat("Volume", 1f);

        PlayMainMenuMusic();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Menu")
        {
            PlayMainMenuMusic();
            StopAmbiance();
        }
        else if (scene.name == "Puerto")
        {
            PlayPuertoMusic();
            PlayAmbiance();
        }
        else if (scene.name == "Mar")
        {
            PlayMarMusic();
            PlayAmbiance();
        }
    }

    public void PlayMainMenuMusic()
    {
        if (musicSource.clip != mainMenuMusic)
        {
            musicSource.clip = mainMenuMusic;
            musicSource.volume = menuVolume * generalVolume;
            musicSource.Play();
        }
    }

    public void PlayPuertoMusic()
    {
        if (musicSource.clip != puertoMusic)
        {
            musicSource.clip = puertoMusic;
            musicSource.volume = puertoVolume * generalVolume;
            musicSource.Play();
        }
    }

    public void PlayAmbiance()
    {
        if (ambianceSource.clip != ambianceClip)
        {
            ambianceSource.clip = ambianceClip;
            ambianceSource.volume = ambianceVolume * generalVolume;
            ambianceSource.Play();
        }
    }

    public void PlayMarMusic()
    {
        if (musicSource.clip != marMusicChill)
        {
            musicSource.clip = marMusicChill;
            musicSource.volume = marChillVolume * generalVolume;
            musicSource.Play();
        }
    }

    public void StopAmbiance()
    {
        ambianceSource.Stop();
    }
    public void StopMusic()
    {
        musicSource.volume = 0;
    }


    // Gestionar el cambio de musica cuando se entra en un combate
    public void FadeToBattleMusic(float fadeDuration)
    {
        Debug.Log("musica battle");
        if (musicSource.clip != marMusicBattle)  // Asegurar que solo cambia si es necesario
        {
            StartMusicFade(marMusicBattle, fadeDuration, marChillVolume);
        }
    }

    // Gestionar el cambio de musica cuando se sale del combate
    public void FadeToChillMusic(float fadeDuration)
    {
        Debug.Log("musica chill");
        if (musicSource.clip != marMusicChill)  // Asegurar que solo cambia si es necesario
        {
            StartMusicFade(marMusicChill, fadeDuration, marChillVolume);
        }
    }

    // Función central para gestionar las transiciones de música
    private void StartMusicFade(AudioClip newClip, float duration, float targetVolumeFactor)
    {
        if (currentFadeCoroutine != null)
        {
            StopCoroutine(currentFadeCoroutine);  // Detener la transición anterior si hay una activa
        }

        currentFadeCoroutine = StartCoroutine(FadeMusic(newClip, duration, targetVolumeFactor));
    }

    // Coroutine para el efecto de fade
    private IEnumerator FadeMusic(AudioClip newClip, float duration, float targetVolumeFactor)
    {
        float startVolume = musicSource.volume;

        // Fase de Fade Out
        while (musicSource.volume > 0)
        {
            musicSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        // Cambiar la pista
        musicSource.clip = newClip;
        musicSource.Play();

        // Calcular volumen final basado en Volume General
        float targetVolume = targetVolumeFactor * generalVolume;

        // Fase de Fade In
        while (musicSource.volume < targetVolume)
        {
            musicSource.volume += targetVolume * Time.deltaTime / duration;
            yield return null;
        }

        musicSource.volume = targetVolume; // Asegurar volumen exacto
    }
}
