using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Level4 : MonoBehaviour
{

    public float levelTime = 10;
    public float currentTime;
    public EnemyControllerLv4 enemyController;
    public Transform[] BombSpawnPoint;
    private Vector2[] spawnPointRandom;
    private int numSpawnPointRandom = 20;

    public float spawnRate;
    public float nextSpawnableTime;

    public float bombWaveTime;
    public float bombSpawnRate = 40;
    public float nextBombSpawnableTime;
    public int bombPrefabIndex;

    public float lastBombWave = 15;
    private bool IsDropBomb = true;

    public float fallSpeedScale = 1.1f;
    public float currentSpeedScale = 1f;
    public float timeToIncreaseSpeed = 10f;
    public float nextTimeToIncreaseSpeed;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        // Set time
        currentTime = levelTime;
        nextSpawnableTime = levelTime - spawnRate;

        nextBombSpawnableTime = bombWaveTime;

        nextTimeToIncreaseSpeed = levelTime - timeToIncreaseSpeed;

        // Set spawnPoint
        spawnPointRandom = new Vector2[numSpawnPointRandom];
        for (int i = 0;i < numSpawnPointRandom; i++)
        {
            spawnPointRandom[i] = new Vector2(((int)Random.Range(enemyController.SpawnZoneTopLeft.position.x, enemyController.SpawnZoneTopRight.position.x) ),
                                              ((int)Random.Range(enemyController.SpawnZoneBottomLeft.position.y, enemyController.SpawnZoneTopLeft.position.y)) + 0.5f);
            DebugPoint(spawnPointRandom[i]);
        }
        enemyController._spawnPoint = spawnPointRandom;
        bombPrefabIndex = enemyController._enemyPrefabs.Length - 1; // last in spawn point
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        if (IsGameOver())
        {
            GameOver();
            return;
        }
        if(currentTime <= nextTimeToIncreaseSpeed)
        {
            IncreaseFallSpeed();
            TurnAEnemyToBomb();
        }
        SpawnEnemy();
        if(currentTime <= bombWaveTime && IsDropBomb)
        {
            DropBombRandom();
        }
        if(currentTime <= lastBombWave && IsDropBomb)
        {
            DropBombLastWave();
        }
    }

    private void TurnAEnemyToBomb()
    {
        DebugPoint((Vector2)Player.transform.position);
        var obj = Physics2D.OverlapCircle((Vector2)Player.transform.position, 2);
        if (obj == null) return;
        Debug.Log(obj.gameObject.name);
        if (obj.gameObject.GetComponent<EnemyObject>() != null && obj.gameObject.GetComponent<Bomb>() == null)
        {
            var bomb = obj.gameObject.AddComponent<Bomb>();
        }
    }

    private void IncreaseFallSpeed()
    {
        currentSpeedScale *= fallSpeedScale;
        nextTimeToIncreaseSpeed -= timeToIncreaseSpeed;
    }

    private void DropBombRandom()
    {
        Vector2 curSpawnPoint = (Vector2)BombSpawnPoint[Random.Range(0, BombSpawnPoint.Length)].position;
        DebugPoint(curSpawnPoint);
        if (currentTime <= nextBombSpawnableTime)
        {
            enemyController.SpawnEnemyDefault(bombPrefabIndex, curSpawnPoint, 1,0.5f);
            nextBombSpawnableTime = currentTime - bombSpawnRate;
        }
    }

    private void DropBombLastWave()
    {
        foreach(var bombPoint in BombSpawnPoint)
        {
            Vector2 curSpawnPoint = (Vector2)bombPoint.position;
            enemyController.SpawnEnemyDefault(bombPrefabIndex, curSpawnPoint, 1,0.5f);
            IsDropBomb = false;
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

    void SpawnEnemy()
    {
        int prefabIndex = Random.Range(0, enemyController._enemyPrefabs.Length-1);
        int spawnPointindex = Random.Range(0, spawnPointRandom.Length);
        int rotation = Random.Range(0, 4);
        if (currentTime <= nextSpawnableTime)
        {
            enemyController.SpawnEnemyDefaultScaleSpeed(prefabIndex, spawnPointindex, rotation,currentSpeedScale);
            nextSpawnableTime = currentTime - spawnRate;
        }
    }

    void DebugPoint(Vector2 point)
    {
        Debug.Log($"x: {point.x}, y: {point.y}");
    }
}
