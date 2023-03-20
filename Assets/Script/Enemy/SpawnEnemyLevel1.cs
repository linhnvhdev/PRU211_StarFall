using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyLevel1 : MonoBehaviour

{
    [SerializeField] List<Vector2> spawnPoints;
    [SerializeField] GameObject[] prefab;
    [SerializeField] float speed;
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
        if (currentTime < nextSpawnableTime)
        {
            if (currentTime < 95)
            {
                spawnRate = 0.5f;
                Spawn();
            }
            else
            {
                Spawn();
            }



        }


    }

    private void Spawn()
    {
        int prefabIndex = Random.Range(0, prefab.Length);
        Vector2 spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        GameObject block = Instantiate(prefab[prefabIndex], spawnPoint, rotations[Random.Range(0, 3)]);
        nextSpawnableTime -= spawnRate;

    }
}

