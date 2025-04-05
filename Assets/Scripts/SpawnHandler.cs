using System.Collections;
using UnityEngine;

public class SpawnHandler : MonoBehaviour
{
    public Transform player; // El jugador que se moverá al spawn
    public Transform spawnFromCueva;
    public Transform spawnDefault;

    IEnumerator Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        yield return null; // Espera un frame
        string fromScene = PlayerSpawnManager.Instance != null ? PlayerSpawnManager.Instance.lastScene : "";

        if (fromScene == "Cueva" && spawnFromCueva != null)
        {
            player.position = spawnFromCueva.position;
        }
        else
        {
            player.position = spawnDefault.position;
        }
    }
}
