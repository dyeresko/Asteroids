
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    private float spawnTime = 2.0f;
    public Asteroid asteroidPrefab;
    public float spawnDistance = 15.0f;
    private float angle = 15.0f;

    void Start()
    {
        InvokeRepeating(nameof(Spawn), spawnTime, spawnTime);
    }
    private void Spawn()
    {
        Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawnDistance;
        Vector3 spawnPoint = this.transform.position + spawnDirection;
        float variance = Random.Range(-angle, angle);
        Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

        Asteroid asteroid = Instantiate(asteroidPrefab, spawnPoint, rotation);
        asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);
        asteroid.SetTrajectory(rotation * -spawnDirection);
    }
}
