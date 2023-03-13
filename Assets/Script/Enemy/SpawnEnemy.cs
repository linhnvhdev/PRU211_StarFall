using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] List<Vector2> spawnPoints;
    [SerializeField] GameObject prefab;
    private Quaternion[] rotations = { Quaternion.Euler(0, 0, -90),
                                         Quaternion.Euler(0, 0, 0),
                                         Quaternion.Euler(0, 0, 90),
                                         Quaternion.Euler(0, 0, 180)};
    public float spawnRate = 2;
    public float startTime = 100;
    public float currentTime;
    private float nextSpawnableTime;

    void Start()
    {
        for (int i = -19; i < 9; i++)
        {
            spawnPoints.Add(new Vector2(i, 20));
        }
        currentTime = startTime;
        nextSpawnableTime = startTime - spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        for(int i = 0; i <30; i++)
        {
            if(currentTime < nextSpawnableTime)
            {
                Spawn();
            }          
        }
    }

    private void Spawn()
    {
        Vector2 spawnPoint = spawnPoints[Random.Range(0,spawnPoints.Count)];
        Instantiate(prefab, spawnPoint, rotations[Random.Range(0, 3)]);
        nextSpawnableTime -= spawnRate;
    }
}
