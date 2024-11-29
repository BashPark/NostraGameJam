using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Enemyprefabs;
    [SerializeField] private Transform spawnZone;
    public int maxEnemies = 3;
    [SerializeField] private float minDistance = 2f;
    [SerializeField] private float spawnDelay = 5f; // Delay between spawn attempts
    [SerializeField] private Transform SafeZone;

    private List<GameObject> spawnedObjects = new List<GameObject>();
    private int currentPrefabIndex = 0;

    // Property to get the current number of spawned objects
    public int CurrentSpawnedObjectsCount => spawnedObjects.Count;

    private void Start()
    {
        SafeZone = GameObject.FindWithTag("Safezone").transform;

        // Start the spawning logic initially
        StartCoroutine(SpawnObjectsWithDelay());
    }

    private IEnumerator SpawnObjectsWithDelay()
    {
        while (true)
        {
            // Wait until the required delay before spawning
            yield return new WaitForSeconds(spawnDelay);
            // Check and spawn new objects if necessary
            SpawnEnemies();
        }
    }
    public void SpawnEnemies()
    {
        if (spawnedObjects.Count < maxEnemies)
        {
            bool spawnSuccess = false;
            int attempts = 0;

            while (!spawnSuccess && attempts < 10)
            {
                Vector3 spawnPos = GetRandomSpawnPosition();
                if (IsValidSpawnPosition(spawnPos))
                {
                    spawnSuccess = true;

                    // Use currentPrefabIndex instead of random selection
                    GameObject prefab = Enemyprefabs[currentPrefabIndex];

                    // Increment the index, looping back to 0 if necessary
                    currentPrefabIndex = (currentPrefabIndex + 1) % Enemyprefabs.Length;

                    Transform spawnPoint = prefab.transform.Find("SpawnPoint");
                    float yOffset = spawnPoint != null ? spawnPoint.localPosition.y : 0;
                    Vector3 adjustedSpawnPos = new Vector3(spawnPos.x, spawnPos.y + yOffset, spawnPos.z);

                    // Instantiate and add to the list
                    GameObject spawnedObject = Instantiate(prefab, adjustedSpawnPos, Quaternion.identity);

                    spawnedObjects.Add(spawnedObject);
                }
                attempts++;
            }
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Bounds zoneBounds = spawnZone.GetComponent<MeshRenderer>().bounds;
        float x = Random.Range(zoneBounds.min.x, zoneBounds.max.x);
        float z = Random.Range(zoneBounds.min.z, zoneBounds.max.z);
        float y = spawnZone.position.y;
        return new Vector3(x, y, z);
    }

    private bool IsValidSpawnPosition(Vector3 position)
    {
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj != null && Vector3.Distance(position, obj.transform.position) < minDistance)
                return false;
        }

        if (Vector3.Distance(position, SafeZone.position) < minDistance)
            return false;

        return true;
    }

    public void HandleEnemyDeath(GameObject deadEnemy)
    {
        // Remove the collected object from the list
        spawnedObjects.Remove(deadEnemy);
        Destroy(deadEnemy);

        // Check if we need to spawn more objects to reach the max limit
        if (spawnedObjects.Count < maxEnemies)
        {
            SpawnEnemies();
        }
    }

}