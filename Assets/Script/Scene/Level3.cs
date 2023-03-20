using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
public class Level3 : MonoBehaviour
{
    public float levelTime = 10;
    public float currentTime;
    public EnemyControllerLv3 enemyController;
    private Vector2[] spawnPointRandom;
    public Transform[] LaserSpawnPoint;
    public Transform[] countDonwTextPoint;
    public int laserAndCountDownTextPointIndex;
    public GameObject[] countDonwTextPointObject;
    private int numSpawnPointRandom = 20;

    public float spawnRate;
    public float nextSpawnableTime;

    public float defaultSpeed;

    public float LaserSpawnRate = 10;
    public float baseLaserChargeTime = 3;
    public float LaserChargeTime;
    public float nextLaserSpawnableTime;
    public float nextChargeCountDownTime;
    public int LaserPrefabIndex;
    public bool hasCreatedCountDownText = false;

    public float fallSpeedScale = 1.1f;
    public float currentSpeedScale = 1f;
    public float timeToIncreaseSpeed = 10f;
    public float nextTimeToIncreaseSpeed;
    public GameObject Player;

   // private BossLaserGun laserBeamController;

    public LevelPointManager levelPointManager;
    private bool gameOver = false;
    void Start()
    {
        // Set time
        currentTime = levelTime;
        nextSpawnableTime = levelTime - spawnRate;
        nextChargeCountDownTime = nextLaserSpawnableTime + baseLaserChargeTime;
        nextTimeToIncreaseSpeed = levelTime - timeToIncreaseSpeed;
        LaserPrefabIndex = enemyController._enemyPrefabs.Length - 1;
        LaserChargeTime = baseLaserChargeTime;
        // Set spawnPoint
        spawnPointRandom = new Vector2[numSpawnPointRandom];
        for (int i = 0; i < numSpawnPointRandom; i++)
        {
            spawnPointRandom[i] = new Vector2(((int)Random.Range(enemyController.SpawnZoneTopLeft.position.x, enemyController.SpawnZoneTopRight.position.x)),
                                              ((int)Random.Range(enemyController.SpawnZoneBottomLeft.position.y, enemyController.SpawnZoneTopLeft.position.y)) + 0.5f);
            DebugPoint(spawnPointRandom[i]);
        }
        enemyController._spawnPoint = spawnPointRandom;
        foreach (var obj in enemyController._enemyPrefabs)
        {
            if (obj.GetComponent<EnemyMovement>() != null)
            {
                obj.GetComponent<EnemyMovement>().speed = defaultSpeed;
            }
        }
        UnityEngine.Debug.Log("before textmesh");
    }
        void Update()
    {
        currentTime -= Time.deltaTime;
        if (gameOver || IsGameOver())
        {
            GameOver();
            return;
        }
        if (currentTime <= nextTimeToIncreaseSpeed)
        {
            levelPointManager.IncreasePointByTime(1);
            IncreaseFallSpeed();
        }
        SpawnEnemy();
        if (currentTime <= nextChargeCountDownTime)
        {
            LaserChargeTime -= Time.deltaTime;
            ChargeLaser();
            ShootLaserRandom();
        }
    }
    private void GameWin()
    {
        levelPointManager.GameOver(false);
        gameOver = true;
    }
    public void ChargeLaser()
    {
        // Countdown text
        if (!hasCreatedCountDownText)
        {
            UnityEngine.Debug.Log("create new countdownText");
            laserAndCountDownTextPointIndex = Random.Range(0, LaserSpawnPoint.Length);
            countDonwTextPointObject[laserAndCountDownTextPointIndex] = new GameObject("countdownText");
            countDonwTextPointObject[laserAndCountDownTextPointIndex].AddComponent<TextMeshPro>();
            countDonwTextPointObject[laserAndCountDownTextPointIndex].AddComponent<MeshRenderer>();
            hasCreatedCountDownText = true;
        }
        TextMeshPro textMeshComponent = countDonwTextPointObject[laserAndCountDownTextPointIndex].GetComponent<TextMeshPro>();
        MeshRenderer meshRendererComponent = countDonwTextPointObject[laserAndCountDownTextPointIndex].GetComponent<MeshRenderer>();
        textMeshComponent.text = LaserChargeTime.ToString("F1");
        textMeshComponent.color = Color.yellow;
        textMeshComponent.fontSize = 10;
        textMeshComponent.sortingOrder = 10;
        textMeshComponent.alignment = TextAlignmentOptions.Bottom;
        textMeshComponent.fontWeight = FontWeight.Bold;
        countDonwTextPointObject[laserAndCountDownTextPointIndex].GetComponent<TextMeshPro>().text = LaserChargeTime.ToString("F1");
        textMeshComponent.transform.position = countDonwTextPoint[laserAndCountDownTextPointIndex].transform.position;
        UnityEngine.Debug.Log("aftertextmesh");
    }

    private void ShootLaserRandom()
    {
        Vector2 curSpawnPoint = (Vector2)LaserSpawnPoint[laserAndCountDownTextPointIndex].position;
        Debug.Log("#####################################");
        Debug.Log(LaserSpawnPoint.Length);
        Debug.Log(Random.Range(0, LaserSpawnPoint.Length));
        DebugPoint(curSpawnPoint);

     //   ChargeLaser();
        if (currentTime <= nextLaserSpawnableTime)
        {
            Destroy(countDonwTextPointObject[laserAndCountDownTextPointIndex]);
            enemyController.SpawnLaser(LaserPrefabIndex, curSpawnPoint, 0, 0f);
            nextLaserSpawnableTime = currentTime - LaserSpawnRate;
            nextChargeCountDownTime = nextLaserSpawnableTime + baseLaserChargeTime;
            hasCreatedCountDownText = false;
            LaserChargeTime = baseLaserChargeTime;
        }
    }

    private void IncreaseFallSpeed()
    {
        currentSpeedScale *= fallSpeedScale;
        nextTimeToIncreaseSpeed -= timeToIncreaseSpeed;
        spawnRate -= 0.2f;
    }
    void SpawnEnemy()
    {
        int prefabIndex = Random.Range(0, enemyController._enemyPrefabs.Length);
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
        var list = FindObjectsOfType<EnemyMovement>().ToList();
        foreach (var enemy in list)
        {
            if (!enemy.checkBelow() && !enemy.isGrounded) continue;
            foreach (Transform enemyChild in enemy.transform)
            {
                if (enemyChild.position.y >= 11.5)
                    return true;
            }
        }
        if (Player.GetComponent<Player>().currentHealth <= 0)
        {
            Destroy(Player);
            return true;
        }
        return false;
    }

    void GameOver()
    {
        gameOver = true;
        Debug.Log("game over");
    }
    void DebugPoint(Vector2 point)
    {
        Debug.Log( " Current Point"+ $"x: {point.x}, y: {point.y}");
    }
}
