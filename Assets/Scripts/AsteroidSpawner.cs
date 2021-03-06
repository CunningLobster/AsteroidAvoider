using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] asteroidPrefabs;
    [SerializeField] private float secondsBetweenAsteroids = 1.5f;
    [SerializeField] private Vector2 forceRange;

    private Camera mainCamera;
    private float timer;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            SpawnAsteroid();

            timer += secondsBetweenAsteroids;
        }
    }

    private void SpawnAsteroid()
    {
        int side = Random.Range(0, 3);
        
        Vector2 spawnPoint = Vector2.zero;
        Vector2 direction = Vector2.zero;

        switch (side)
        {
            case 0:
                spawnPoint.x = 0;
                spawnPoint.y = Random.value;
                direction = new Vector2(1, Random.Range(-1f, 1f));
                break;
            case 1:
                spawnPoint.x = 1;
                spawnPoint.y = Random.value;
                direction = new Vector2(-1, Random.Range(-1f, 1f));
                break;
            case 2:
                spawnPoint.x = Random.value;
                spawnPoint.y = 0;
                direction = new Vector2(Random.Range(-1f, 1f), 1);
                break;
            case 3:
                spawnPoint.x = Random.value;
                spawnPoint.y = 1;
                direction = new Vector2(Random.Range(-1f, 1f), -1);
                break;
        }

        Vector3 worldSpawnPoint = mainCamera.ViewportToWorldPoint(spawnPoint);
        worldSpawnPoint.z = 0;

        var selectedAsteroid = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];
        GameObject asteroidInstance = 
            Instantiate(selectedAsteroid, 
            worldSpawnPoint, 
            Quaternion.Euler(0, 0, Random.Range(0f, 360f)));
        Rigidbody rb = asteroidInstance.GetComponent<Rigidbody>();

        rb.velocity = direction.normalized * Random.Range(forceRange.x, forceRange.y);
    }
}
