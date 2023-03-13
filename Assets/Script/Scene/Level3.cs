using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3 : MonoBehaviour
{
    public float levelTime = 10;
    public float currentTime;
    public EnemyControllerLv4 enemyController;
    private Vector2[] spawnPointRandom;
    private int numSpawnPointRandom = 20;

    public float spawnRate;
    public float nextSpawnableTime;

    public float fallSpeedScale = 1.1f;
    public float currentSpeedScale = 1f;
    public float timeToIncreaseSpeed = 10f;
    public float nextTimeToIncreaseSpeed;
    public GameObject Player;

    private BossLaserGun laserBeamController;
    void Start()
    {
        laserBeamController = GameObject.Find("LaserSpawnPoint").GetComponent<BossLaserGun>();
        // Set time
        currentTime = levelTime;
        nextSpawnableTime = levelTime - spawnRate;

        nextTimeToIncreaseSpeed = levelTime - timeToIncreaseSpeed;

        // Set spawnPoint
        spawnPointRandom = new Vector2[numSpawnPointRandom];
        for (int i = 0; i < numSpawnPointRandom; i++)
        {
            spawnPointRandom[i] = new Vector2(((int)Random.Range(enemyController.SpawnZoneTopLeft.position.x, enemyController.SpawnZoneTopRight.position.x)),
                                              ((int)Random.Range(enemyController.SpawnZoneBottomLeft.position.y, enemyController.SpawnZoneTopLeft.position.y)) + 0.5f);
            DebugPoint(spawnPointRandom[i]);
        }
        enemyController._spawnPoint = spawnPointRandom;
    }
        void Update()
    {
        currentTime -= Time.deltaTime;
        if (IsGameOver())
        {
            GameOver();
            return;
        }
        if (currentTime <= nextTimeToIncreaseSpeed)
        {
            IncreaseFallSpeed();
        }
        SpawnEnemy();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            laserBeamController.ActivateLaserBeam();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            laserBeamController.DeactivateLaserBeam();
        }
    }
    private void IncreaseFallSpeed()
    {
        currentSpeedScale *= fallSpeedScale;
        nextTimeToIncreaseSpeed -= timeToIncreaseSpeed;
    }
    void SpawnEnemy()
    {
        int prefabIndex = Random.Range(0, enemyController._enemyPrefabs.Length - 1);
        int spawnPointindex = Random.Range(0, spawnPointRandom.Length);
        int rotation = Random.Range(0, 4);
        if (currentTime <= nextSpawnableTime)
        {
            enemyController.SpawnEnemyDefaultScaleSpeed(prefabIndex, spawnPointindex, rotation, currentSpeedScale);
            nextSpawnableTime = currentTime - spawnRate;
        }
    }
    bool IsGameOver()
    {
        if (currentTime <= 0)
            return true;
        return false;
    }

    void GameOver()
    {
        Debug.Log("game over");
        //var bomb = GameObject.FindObjectOfType<BombCore>();
        //Destroy(bomb);
    }
    void DebugPoint(Vector2 point)
    {
        Debug.Log($"x: {point.x}, y: {point.y}");
    }
}
