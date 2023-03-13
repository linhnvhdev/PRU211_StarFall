using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3 : MonoBehaviour
{
    [SerializeField] private EnemyObject[] _enemyPrefabs;
    [SerializeField] private Vector2[] _spawnPoint;
    [SerializeField] Transform SpawnZoneTopLeft;
    [SerializeField] Transform SpawnZoneTopRight;
    [SerializeField] Transform SpawnZoneBottomLeft;
    [SerializeField] Transform SpawnZoneBottomRight;
    [SerializeField] LayerMask enemyLayerMask;

    public float levelTime = 10;

    public float spawnRate;
    private float timer;
    public float currentTime;
    private float nextSpawnableTime;
    private Quaternion[] rotations = { Quaternion.Euler(0, 0, -90),
                                         Quaternion.Euler(0, 0, 0),
                                         Quaternion.Euler(0, 0, 90),
                                         Quaternion.Euler(0, 0, 180)};
    

    void Start()
    {
        currentTime = levelTime;
        nextSpawnableTime = levelTime - spawnRate;
    }


    void Update()
    {
        currentTime -= Time.deltaTime;
        SpawnEnemyDefault((int)Random.Range(0, _enemyPrefabs.Length), (int)Random.Range(0, _spawnPoint.Length), (int)Random.Range(0, rotations.Length));
    }

    public bool SpawnCheck(Vector2 spawnPoint, float range, LayerMask layerMask)
    {
        var list = Physics2D.OverlapArea(new Vector2(spawnPoint.x - range, spawnPoint.y - range),
                                        new Vector2(spawnPoint.x + range, spawnPoint.y + range), layerMask);
        if (list == null)
        {
            return true;
        }
        return false;
    }

    //Spawn enemy theo các thuộc tính default mà t đã đặt
    private void SpawnEnemyDefault(int prefabIndex, int spawnPointIndex, int rotationIndex)
    {
        Vector2 spawnPoint = new Vector2((int)Random.Range(SpawnZoneTopLeft.position.x, SpawnZoneTopRight.position.x),
                                         (int)Random.Range(SpawnZoneBottomLeft.position.y, SpawnZoneTopLeft.position.y));
        if (SpawnCheck(spawnPoint, 2f, enemyLayerMask))
        {
            EnemyObject enemy = Instantiate(_enemyPrefabs[prefabIndex], _spawnPoint[spawnPointIndex], rotations[rotationIndex]);
            enemy.SetEnemyDefaultData(_enemyPrefabs[prefabIndex].GetComponent<EnemyObject>().material);
            enemy.GetComponent<Rigidbody2D>().gravityScale = enemy.fallSpeed;
            nextSpawnableTime -= spawnRate;
        }
        else
        {
            Debug.Log("Unable to spawn");
        }
    }

    //Spawn enemy theo các thuộc tính custom
    private void SpawnEnemyCustom(int prefabIndex, int spawnPointIndex, int rotationIndex, int health, int damage, int score, float fallSpeed)
    {
        if (SpawnCheck(_spawnPoint[0], 2f, enemyLayerMask))
        {
            EnemyObject enemy = Instantiate(_enemyPrefabs[prefabIndex], _spawnPoint[spawnPointIndex], rotations[rotationIndex]);
            enemy.SetEnemyCustomData(health, damage, score, fallSpeed);
            enemy.GetComponent<Rigidbody2D>().gravityScale = enemy.fallSpeed;
            nextSpawnableTime -= spawnRate;
        }
        else
        {
            Debug.Log("Unable to spawn");
        }
    }
}
