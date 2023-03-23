using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Level1 : MonoBehaviour
{

    public float levelTime = 10;
    public float currentTime;
    public EnemyControllerLv1 enemyController;
    private Vector2[] spawnPointRandom;
    private int numSpawnPointRandom = 0;
    public float spawnRate;
    private float nextSpawnableTime;
    public float landMineWaveTime;
    public float landMineSpawnRate;
    public float nextLandMineSpawnableTime;
    public float defaultSpeed;
    public float fallSpeedScale = 1.2f;
    public float currentSpeedScale = 1f;
    public float timeToIncreaseSpeed = 10f;
    private float nextTimeToIncreaseSpeed;
    public float speedLimitScale = 5;
    public GameObject Player;
    List<GameObject> enemyPrefabs;
    public LevelPointManager levelPointManager;
    private bool IsIncreaseSpeedWave = false;
    public LayerMask enemyLayerMask;
    public GameOverScreen GameOverScreen;
    int maxPlasform = 0;
    private bool gameOver = false;
    public GameWinScreen GameWinScreen;

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
        nextTimeToIncreaseSpeed = levelTime - timeToIncreaseSpeed;
        nextLandMineSpawnableTime = landMineWaveTime;
        // Set spawnPoint
        spawnPointRandom = new Vector2[27];
        for (int i = (int)enemyController.SpawnZoneTopLeft.position.x; i <= enemyController.SpawnZoneTopRight.position.x; i++)
        {
            //DebugPoint(new Vector2(i, enemyController.SpawnZoneBottomLeft.position.y));
            spawnPointRandom[numSpawnPointRandom] = new Vector2(i, enemyController.SpawnZoneBottomLeft.position.y);
            numSpawnPointRandom++;
            //DebugPoint(spawnPointRandom[numSpawnPointRandom]);
        }
        enemyController._spawnPoint = spawnPointRandom;

        foreach (var obj in enemyController._enemyPrefabs)
        {
            obj.GetComponent<EnemyMovement>().speed = defaultSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOver) 
            return;
        if (FindObjectOfType<Player>() != null)
        {
            Player = FindObjectOfType<Player>().gameObject;
        }
        currentTime -= Time.deltaTime;
        if (gameOver || IsGameOver())
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
       
        SpawnEnemy();
        
       
    }

    private void GameWin()
    {
        GameWinScreen.Setup(levelPointManager.totalPoint);
        levelPointManager.GameOver(false);
        gameOver = true;
    }

   

    private void IncreaseFallSpeed()
    {
        if (currentSpeedScale > speedLimitScale) fallSpeedScale = 1;
        currentSpeedScale *= fallSpeedScale;
        nextTimeToIncreaseSpeed -= timeToIncreaseSpeed;
        if (spawnRate > 1) spawnRate -= 0.5f;
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
        if(Player != null)
        {
            if (Player.GetComponent<Player>().currentHealth <= 0)
            {
                Destroy(Player);
                return true;
            }
        }      
        return false;
    }

    void GameOver()
    {
        GameOverScreen.Setup(levelPointManager.totalPoint);
        gameOver=true;
    }

    void SpawnEnemy()
    {
        int prefabIndex = Random.Range(0, enemyController._enemyPrefabs.Length - 1);
        int spawnPointindex = Random.Range(0, spawnPointRandom.Length);
        int rotation = Random.Range(0, 4);
        if (currentTime <= nextSpawnableTime)
        {
            bool spawnOk = enemyController.SpawnEnemyDefaultScaleSpeed(prefabIndex, spawnPointindex, rotation, currentSpeedScale);
            if (spawnOk) nextSpawnableTime = currentTime - spawnRate;
        }
    }

    void DebugPoint(Vector2 point)
    {
        Debug.Log($"x: {point.x}, y: {point.y}");
    }
}
