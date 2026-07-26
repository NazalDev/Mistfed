using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private GameObject prefabToSpawn; // The object you want to duplicate
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private float spawnRange = 10f;      // Seconds between spawns

    private float timer = 0f;

    void Update()
    {
        // Track the passing time
        timer += Time.deltaTime;

        // Check if it is time to spawn
        if (timer >= spawnRate)
        {
            SpawnObject();
            timer = 0f; // Reset the clock
        }
    }

    void SpawnObject()
    {
        if (prefabToSpawn != null)
        {
            // Instantiates the prefab at the spawner's current position and rotation
            Instantiate(prefabToSpawn, new Vector3(transform.position.x + Random.Range(-spawnRange, spawnRange), transform.position.y, transform.position.z + Random.Range(-spawnRange, spawnRange)), transform.rotation);
        }
        else
        {
            Debug.LogWarning("Spawner is missing a Prefab assignment!", this);
        }
    }
}