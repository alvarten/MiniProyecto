using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    public static PlayerSpawnManager Instance;

    [HideInInspector] public string lastScene;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantener entre escenas
        }
        else
        {
            Destroy(gameObject); // Eliminar duplicados
        }
    }

    public void SetLastScene(string sceneName)
    {
        lastScene = sceneName;
    }
}
