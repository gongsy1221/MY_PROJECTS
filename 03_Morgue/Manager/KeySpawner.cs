using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class KeySpawner : MonoSingleton<KeySpawner>
{
    public GameObject objectToSpawn;
    public Transform[] spawnPoints;

    public void RandomSpawnKey()
    {
        if (spawnPoints.Length == 0) return;

        int randomIndex = Random.Range(1, spawnPoints.Length - 1);
        Transform spawnPoint = spawnPoints[randomIndex];

        Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
    }
}
