using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Enum;

public class SpawnEnemyLv2 : MonoBehaviour
{
    [SerializeField] List<Vector2> spawnPoints;
    [SerializeField] GameObject[] prefab;
    [SerializeField] float speed;
    [SerializeField] LayerMask enemyLayerMask;
    [SerializeField] int minX = -19;
    [SerializeField] int maxX = 9;
    [SerializeField] int spawnHeight = 14;
    private Quaternion[] rotations = { Quaternion.Euler(0, 0, -90),
                                         Quaternion.Euler(0, 0, 0),
                                         Quaternion.Euler(0, 0, 90),
                                         Quaternion.Euler(0, 0, 180)};
    public float spawnRate = 3;
    public float startTime = 300;
    public float currentTime;
    private float nextSpawnableTime;
    public GameStateLv2 gameState;
    public int currentEnemy = 0;
    private GameObject Player;
    private bool gameOver = false;
    #region Prefabs
    private GameObject Iwood;
    private GameObject Lwood;
    private GameObject Jwood;
    private GameObject Owood;
    private GameObject Swood;
    private GameObject Zwood;
    private GameObject Twood;
    private GameObject Istone;
    private GameObject Lstone;
    private GameObject Jstone;
    private GameObject Ostone;
    private GameObject Sstone;
    private GameObject Zstone;
    private GameObject Tstone;
    private GameObject Iiron;
    private GameObject Liron;
    private GameObject Jiron;
    private GameObject Oiron;
    private GameObject Siron;
    private GameObject Ziron;
    private GameObject Tiron;
    private GameObject Igold;
    private GameObject Lgold;
    private GameObject Jgold;
    private GameObject Ogold;
    private GameObject Sgold;
    private GameObject Zgold;
    private GameObject Tgold;
    private GameObject Idiamond;
    private GameObject Ldiamond;
    private GameObject Jdiamond;
    private GameObject Odiamond;
    private GameObject Sdiamond;
    private GameObject Zdiamond;
    private GameObject Tdiamond;
    #endregion
    void Start()
    {
        #region Load Prefabs
        Iwood = Resources.Load("Blocks/I wood", typeof(GameObject)) as GameObject;
        Lwood = Resources.Load("Blocks/L wood", typeof(GameObject)) as GameObject;
        Jwood = Resources.Load("Blocks/J wood", typeof(GameObject)) as GameObject;
        Owood = Resources.Load("Blocks/O wood", typeof(GameObject)) as GameObject;
        Swood = Resources.Load("Blocks/Z wood", typeof(GameObject)) as GameObject;
        Zwood = Resources.Load("Blocks/S wood", typeof(GameObject)) as GameObject;
        Twood = Resources.Load("Blocks/T wood", typeof(GameObject)) as GameObject;
        Istone = Resources.Load("Blocks/I stone", typeof(GameObject)) as GameObject;
        Lstone = Resources.Load("Blocks/L stone", typeof(GameObject)) as GameObject;
        Jstone = Resources.Load("Blocks/J stone", typeof(GameObject)) as GameObject;
        Ostone = Resources.Load("Blocks/O stone", typeof(GameObject)) as GameObject;
        Sstone = Resources.Load("Blocks/S stone", typeof(GameObject)) as GameObject;
        Zstone = Resources.Load("Blocks/Z stone", typeof(GameObject)) as GameObject;
        Tstone = Resources.Load("Blocks/T stone", typeof(GameObject)) as GameObject;
        Iiron = Resources.Load("Blocks/I iron", typeof(GameObject)) as GameObject;
        Liron = Resources.Load("Blocks/L iron", typeof(GameObject)) as GameObject;
        Jiron = Resources.Load("Blocks/J iron", typeof(GameObject)) as GameObject;
        Oiron = Resources.Load("Blocks/O iron", typeof(GameObject)) as GameObject;
        Siron = Resources.Load("Blocks/S iron", typeof(GameObject)) as GameObject;
        Ziron = Resources.Load("Blocks/Z iron", typeof(GameObject)) as GameObject;
        Tiron = Resources.Load("Blocks/T iron", typeof(GameObject)) as GameObject;
        Igold = Resources.Load("Blocks/I gold", typeof(GameObject)) as GameObject;
        Lgold = Resources.Load("Blocks/L gold", typeof(GameObject)) as GameObject;
        Jgold = Resources.Load("Blocks/J gold", typeof(GameObject)) as GameObject;
        Ogold = Resources.Load("Blocks/O gold", typeof(GameObject)) as GameObject;
        Sgold = Resources.Load("Blocks/S gold", typeof(GameObject)) as GameObject;
        Zgold = Resources.Load("Blocks/Z gold", typeof(GameObject)) as GameObject;
        Tgold = Resources.Load("Blocks/T gold", typeof(GameObject)) as GameObject;
        Idiamond = Resources.Load("Blocks/I diamond", typeof(GameObject)) as GameObject;
        Ldiamond = Resources.Load("Blocks/J diamond", typeof(GameObject)) as GameObject;
        Jdiamond = Resources.Load("Blocks/L diamond", typeof(GameObject)) as GameObject;
        Odiamond = Resources.Load("Blocks/O diamond", typeof(GameObject)) as GameObject;
        Sdiamond = Resources.Load("Blocks/S diamond", typeof(GameObject)) as GameObject;
        Zdiamond = Resources.Load("Blocks/Z diamond", typeof(GameObject)) as GameObject;
        Tdiamond = Resources.Load("Blocks/T diamond", typeof(GameObject)) as GameObject;
        #endregion
        #region Load SpawnPoint
        for (int i = minX; i < maxX; i++)
        {
            spawnPoints.Add(new Vector2(i, spawnHeight));
        }
        #endregion

        currentTime = startTime;
        nextSpawnableTime = startTime - spawnRate;
        gameState = GameStateLv2.SPAWN_WAVE1;

        //Test
        SpawnWave1();
        //      
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<Player>() != null)
        {
            Player = FindObjectOfType<Player>().gameObject;
        }
        //Track time
        currentTime -= Time.deltaTime;
        if (gameOver || IsGameOver())
        {
            GameOver();
            return;
        }
        if (currentEnemy < 23)
        {
            gameState = GameStateLv2.SPAWN_WAVE1;
        }
        else if ((currentEnemy >= 23 && currentEnemy < 46) && currentTime < 200)
        {
            gameState = GameStateLv2.SPAWN_WAVE2;
        }
        else if (currentEnemy >= 46 && currentTime < 100)
        {
            gameState = GameStateLv2.SPAWN_WAVE3;
        }
        else
        {
            gameState = GameStateLv2.SPAWN_RANDOM;
        }
        switch (gameState)
        {
            case GameStateLv2.SPAWN_RANDOM:
                if (currentTime < nextSpawnableTime)
                {
                    SpawnRandom();
                }
                break;
            case GameStateLv2.SPAWN_WAVE1:
                SpawnWave1();
                break;
            case GameStateLv2.SPAWN_WAVE2:
                SpawnWave2();
                break;
            case GameStateLv2.SPAWN_WAVE3:
                SpawnWave3();
                break;
        }

    }

    private void SpawnWave1()
    {
        int[] spawnPointX = { -16, -12, -9, 5, 1, 9, -17, -14, -7, 6, 4, -7, -11, -13, -15, -18, 6, -1, -3, 3, 0, -11, 2 };
        GameObject[] enemies = { Jwood, Iwood, Zwood, Iwood, Jwood, Owood, Iwood, Ostone, Lwood, Iwood, Ostone, Twood, Iwood, Ostone, Iwood, Tstone, Jwood, Swood, Jwood, Ostone, Iwood, Jwood, Iwood };
        int[] rotateIndexs = { 1, 1, 1, 1, 1, 1, 2, 1, 2, 2, 1, 1, 2, 1, 1, 1, 3, 1, 0, 1, 2, 3, 1 };
        if (currentTime < nextSpawnableTime)
        {
            SpawnEnemy(enemies[currentEnemy], new Vector2(spawnPointX[currentEnemy], spawnHeight), rotateIndexs[currentEnemy]);
        }
    }
    private void SpawnWave2()
    {
        int[] spawnPointX = { -2, -5, -18, 3, 8, -3, -9, -11, -2, 7, 0, -15, -9, -11, 0, -5, -8, 9, 5, -3, 7, -6, -13 };
        GameObject[] enemies = { Twood, Istone, Zwood, Iwood, Zstone, Twood, Zstone, Iwood, Owood, Lstone, Iwood, Jwood, Lstone, Iwood, Iwood, Jstone, Jiron, Lwood, Liron, Istone, Siron, Oiron, Istone };
        int[] rotateIndex = { 1, 1, 2, 2, 1, 2, 1, 2, 1, 2, 2, 1, 2, 2, 2, 1, 1, 1, 1, 1, 2, 1, 2 };
        if (currentTime < nextSpawnableTime)
        {
            SpawnEnemy(enemies[currentEnemy - 23], new Vector2(spawnPointX[currentEnemy - 23], spawnHeight), rotateIndex[currentEnemy - 23]);
        }
    }

    private void SpawnWave3()
    {
        int[] spawnPointX = { -11, -18, 0, 6, 3, -10, -14, -5, 0, -3, -8, -5, -10, 0, -2, -8, -5, -7, -3, -2, -17, -8, 4 };
        GameObject[] enemies = { Iwood, Tiron, Iwood, Lgold, Ostone, Swood, Sgold, Jgold, Swood, Twood, Twood, Iwood, Jwood, Lwood, Iwood, Iwood, Iwood, Swood, Swood, Swood, Odiamond, Swood, Sdiamond };
        int[] rotateIndex = { 2, 2, 2, 1, 1, 1, 1, 1, 1, 3, 3, 1, 0, 2, 1, 1, 1, 2, 2, 2, 1, 2, 2 };
        if (currentTime < nextSpawnableTime)
        {
            SpawnEnemy(enemies[currentEnemy - 46], new Vector2(spawnPointX[currentEnemy - 46], spawnHeight), rotateIndex[currentEnemy - 23]);
        }
    }

    private bool SpawnCheck(Vector2 spawnPoint, float range, LayerMask layerMask)
    {
        var list = Physics2D.OverlapArea(new Vector2(spawnPoint.x - range, spawnPoint.y - range),
                                        new Vector2(spawnPoint.x + range, spawnPoint.y + range), layerMask);
        if (list == null)
        {
            return true;
        }
        return false;
    }

    private void SpawnEnemy(GameObject prefab, Vector2 spawnPoint, int rotationIndex)
    {
        if (SpawnCheck(spawnPoint, 3f, enemyLayerMask))
        {
            GameObject block = Instantiate(prefab, spawnPoint, rotations[rotationIndex]);
            currentEnemy++;
            EnemyObject[] list = block.GetComponentsInChildren<EnemyObject>();
            EnemyMovement blockMovement = block.GetComponent<EnemyMovement>();
            float childrenFallSpeed = 0;
            foreach (EnemyObject eo in list)
            {
                EnemyObject obj = eo.GetComponent<EnemyObject>();
                obj.SetEnemyDefaultData(obj.material);
                childrenFallSpeed = obj.fallSpeed;
                break;
            }
            blockMovement.speed = childrenFallSpeed + speed;
        }
        nextSpawnableTime = currentTime - spawnRate;
    }

    private void SpawnRandom()
    {
        int prefabIndex = Random.Range(0, prefab.Length);
        Vector2 spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        if (SpawnCheck(spawnPoint, 2f, enemyLayerMask))
        {
            GameObject block = Instantiate(prefab[prefabIndex], spawnPoint, rotations[Random.Range(0, 3)]);
            EnemyObject[] list = block.GetComponentsInChildren<EnemyObject>();
            EnemyMovement blockMovement = block.GetComponent<EnemyMovement>();
            float childrenFallSpeed = 0;
            foreach (EnemyObject eo in list)
            {
                EnemyObject obj = eo.GetComponent<EnemyObject>();
                obj.SetEnemyDefaultData(obj.material);
                childrenFallSpeed = obj.fallSpeed;
                break;
            }
            blockMovement.speed = childrenFallSpeed + speed;
        }
        nextSpawnableTime = currentTime - spawnRate;
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
        Debug.Log("game over");
        gameOver = true;
        //var bomb = GameObject.FindObjectOfType<BombCore>();
        //Destroy(bomb);
    }
}
