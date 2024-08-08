using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    // Prefab yang akan di-spawn
    public GameObject objectToSpawn;

    // Posisi spawn
    public Transform spawnPosition;

    // Total objek yang akan di-spawn
    public int totalObjectsToSpawn = 10;

    // Waktu jeda antara spawn
    public float spawnInterval = 1f;

    private int objectsSpawned = 0;

    private void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        while (objectsSpawned < totalObjectsToSpawn)
        {
            // Spawn objek
            Instantiate(objectToSpawn, spawnPosition.position, spawnPosition.rotation);
            objectsSpawned++;

            // Tunggu sebelum spawn objek berikutnya
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
