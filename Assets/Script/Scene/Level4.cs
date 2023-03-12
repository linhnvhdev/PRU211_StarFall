using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Level4 : MonoBehaviour
{
    public Transform SpawnZoneTopLeft;
    public Transform SpawnZoneTopRight;
    public Transform SpawnZoneBottomLeft;
    public Transform SpawnZoneBottomRight;

    public float levelTime = 10;
    public float currentTime;

    public List<GameObject> enemyPrefabs;
    public float spawnRate = 1;
    private float nextSpawnableTime;
    public LayerMask enemyLayerMask;

    private Quaternion[] rotations = { Quaternion.Euler(0, 0, -90), 
                                         Quaternion.Euler(0, 0, 0),
                                         Quaternion.Euler(0, 0, 90),
                                         Quaternion.Euler(0, 0, 180)};

    // Start is called before the first frame update
    void Start()
    {
        currentTime = levelTime;
        nextSpawnableTime = levelTime - spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        if(currentTime <= 0 )
        {
            GameOver();
            return;
        }

        SpawnEnemy();
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
        int blockToSpawn = (int) Random.Range(0, enemyPrefabs.Count);
        if(currentTime <= nextSpawnableTime)
        {
            int checkLimit = 5;
            float range = 2f;
            while (true)
            {
                Vector2 spawnPoint = new Vector2((int) Random.Range(SpawnZoneTopLeft.position.x, SpawnZoneTopRight.position.x),
                                         (int) Random.Range(SpawnZoneBottomLeft.position.y, SpawnZoneTopLeft.position.y));
                //Vector2 spawnPoint = new Vector2(-13, 8);
                DebugPoint(spawnPoint);
                var list = Physics2D.OverlapArea(new Vector2(spawnPoint.x - range, spawnPoint.y - range),
                                        new Vector2(spawnPoint.x + range, spawnPoint.y + range), enemyLayerMask);
                // Not Detect enemy
                //DebugPoint(new Vector2(spawnPoint.x - range, spawnPoint.y - range));
                //DebugPoint(new Vector2(spawnPoint.x + range, spawnPoint.y + range));
                //Debug.Log($"x: {spawnPoint.x}, y: {spawnPoint.y}");
                if (list == null)
                {
                    // rotate enemy
                    int rotation = Random.Range(0, rotations.Length);
                    Debug.Log($"rotation: {rotation}");
                    GameObject spawn = Instantiate(enemyPrefabs[blockToSpawn], (Vector3)spawnPoint, rotations[rotation]);
                    nextSpawnableTime -= spawnRate;
                    break;
                }
                else
                {
                    checkLimit--;
                    if (checkLimit <= 0)
                        break;
                }
            }
        }
    }

    void DebugPoint(Vector2 point)
    {
        Debug.Log($"x: {point.x}, y: {point.y}");
    }
}
