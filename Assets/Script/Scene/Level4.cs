using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class Level4 : MonoBehaviour
{

    public float levelTime = 10;
    public float currentTime;
    public EnemyControllerLv4 enemyController;
    public Transform[] BombSpawnPoint;
    private Vector2[] spawnPointRandom;
    private int numSpawnPointRandom = 0;

    public float spawnRate;
    private float nextSpawnableTime;

    public float bombWaveTime;
    public float bombSpawnRate = 40;
    private float nextBombSpawnableTime;
    public int bombPrefabIndex;

    public float landMineWaveTime;
    public float landMineSpawnRate;
    public float nextLandMineSpawnableTime;

    public float lastBombWave = 15;
    private bool IsDropBomb = true;

    public float defaultSpeed;

    public float fallSpeedScale = 1.2f;
    public float currentSpeedScale = 1f;
    public float timeToIncreaseSpeed = 10f;
    private float nextTimeToIncreaseSpeed;
    public float speedLimitScale = 5;
    public GameObject Player;
    List<GameObject> enemyPrefabs;
    public LevelPointManager levelPointManager;
    public GameOverScreen GameOverScreen;
    public GameWinScreen GameWinScreen;

    private bool IsIncreaseSpeedWave = false;

    public LayerMask enemyLayerMask;

    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectOfType<Player>() != null)
        {
            Player = FindObjectOfType<Player>().gameObject;
        }
        // Set time
        currentTime = levelTime;
        nextSpawnableTime = levelTime;
        levelPointManager.levelTime = levelTime;

        nextBombSpawnableTime = bombWaveTime;

        nextTimeToIncreaseSpeed = levelTime - timeToIncreaseSpeed;

        nextLandMineSpawnableTime = landMineWaveTime;

        

        // Set spawnPoint
        spawnPointRandom = new Vector2[27];
        for (int i = (int) enemyController.SpawnZoneTopLeft.position.x;i <= enemyController.SpawnZoneTopRight.position.x; i++)
        {
            //DebugPoint(new Vector2(i, enemyController.SpawnZoneBottomLeft.position.y));
            spawnPointRandom[numSpawnPointRandom] = new Vector2(i, enemyController.SpawnZoneBottomLeft.position.y);
            numSpawnPointRandom++;
            //DebugPoint(spawnPointRandom[numSpawnPointRandom]);
        }
        enemyController._spawnPoint = spawnPointRandom;
        bombPrefabIndex = enemyController._enemyPrefabs.Length - 1; // last in spawn point

        foreach(var obj in enemyController._enemyPrefabs)
        {
            obj.GetComponent<EnemyMovement>().speed = defaultSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<Player>() != null)
        {
            Player = FindObjectOfType<Player>().gameObject;
        }
        currentTime -= Time.deltaTime;
        if (gameOver) return;
        if (IsGameOver())
        {
            GameOver();
            return;
        }

        if (currentTime <= 0)
        {
            GameWin();
        }

        if (currentTime <= nextTimeToIncreaseSpeed)
        {
            levelPointManager.IncreasePointByTime(1);
            IncreaseFallSpeed();

        }
        if(currentTime <= nextLandMineSpawnableTime)
        {
            TurnAEnemyToBomb();
        }
        SpawnEnemy();
        if(currentTime <= nextBombSpawnableTime && IsDropBomb)
        {
            DropBombRandom();
        }
        if(currentTime <= lastBombWave && IsDropBomb)
        {
            levelPointManager.IncreasePointByTime(20);
            DropBombLastWave();
        }
    }

    private void GameWin()
    {
        levelPointManager.GameOver(false);
        gameOver = true;
        GameWinScreen.Setup(levelPointManager.totalPoint);
    }

    private void TurnAEnemyToBomb()
    {
        //Debug.Log("player pos:");
        //DebugPoint((Vector2)Player.transform.position);
        if (currentTime > nextLandMineSpawnableTime) return;
        var obj = Physics2D.OverlapCircle((Vector2)Player.transform.position, 3,enemyLayerMask);
        if (obj == null) return;
        Debug.Log("detect " + obj.gameObject.name);
        if (obj.gameObject.GetComponent<EnemyObject>() != null && obj.gameObject.GetComponent<Bomb>() == null)
        {
            var bomb = obj.gameObject.AddComponent<Bomb>();
            bomb.range = 6;
            bomb.damageToPlayer = 3;
            bomb.damage = 3;
            
        }
        nextLandMineSpawnableTime -= landMineSpawnRate;
        levelPointManager.IncreasePointByTime(2);
    }

    private void IncreaseFallSpeed()
    {
        if (currentSpeedScale > speedLimitScale) fallSpeedScale = 1;
        currentSpeedScale *= fallSpeedScale;
        nextTimeToIncreaseSpeed -= timeToIncreaseSpeed;
        if(spawnRate > 1f) spawnRate -= 0.5f;
    }

    private void DropBombRandom()
    {
        Vector2 curSpawnPoint = (Vector2)BombSpawnPoint[Random.Range(0, BombSpawnPoint.Length)].position;
        //DebugPoint(curSpawnPoint);
        if (currentTime <= nextBombSpawnableTime)
        {
            bool spawnOk = enemyController.SpawnEnemyDefaultScaleSpeed(bombPrefabIndex, curSpawnPoint, 1, currentSpeedScale);
            if(spawnOk)
            {
                nextBombSpawnableTime = currentTime - bombSpawnRate;
                levelPointManager.IncreasePointByTime(5);
            }
                
        }
    }

    private void DropBombLastWave()
    {
        foreach(var bombPoint in BombSpawnPoint)
        {
            Vector2 curSpawnPoint = (Vector2)bombPoint.position;
            enemyController.SpawnEnemyDefaultScaleSpeed(bombPrefabIndex, curSpawnPoint, 1, currentSpeedScale);
            IsDropBomb = false;
        }
    }

    bool IsGameOver()
    {
        var list = FindObjectsOfType<EnemyMovement>().ToList();
        foreach(var enemy in list)
        {
            if (!enemy.checkBelow() && !enemy.isGrounded) continue;
            foreach(Transform enemyChild in enemy.transform)
            {
                if (enemyChild.position.y >= 11.5)
                    return true;
            }
        }
        if(Player.GetComponent<Player>().currentHealth <= 0)
        {
            Destroy(Player);
            return true;
        }
        return false;
    }

    void GameOver()
    {
        Debug.Log("game over");
        gameOver = true;
        GameOverScreen.Setup(levelPointManager.totalPoint);
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
            bool spawnOk = enemyController.SpawnEnemyDefaultScaleSpeed(prefabIndex, spawnPointindex, rotation,currentSpeedScale);
            if(spawnOk) nextSpawnableTime = currentTime - spawnRate;
        }
    }

    void DebugPoint(Vector2 point)
    {
        Debug.Log($"x: {point.x}, y: {point.y}");
    }
}
