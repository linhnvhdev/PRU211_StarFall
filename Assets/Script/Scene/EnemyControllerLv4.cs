using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerLv4 : MonoBehaviour
{
    [SerializeField] public GameObject[] _enemyPrefabs;
    [SerializeField] public Vector2[] _spawnPoint;
    [SerializeField] public Transform SpawnZoneTopLeft;
    [SerializeField] public Transform SpawnZoneTopRight;
    [SerializeField] public Transform SpawnZoneBottomLeft;
    [SerializeField] public Transform SpawnZoneBottomRight;
    [SerializeField] LayerMask enemyLayerMask;

    public Quaternion[] rotations = { Quaternion.Euler(0, 0, -90),
                                         Quaternion.Euler(0, 0, 0),
                                         Quaternion.Euler(0, 0, 90),
                                         Quaternion.Euler(0, 0, 180)};

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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

    public void SpawnEnemyDefault(int prefabIndex, int spawnPointIndex, int rotationIndex)
    {
        if (SpawnCheck(_spawnPoint[spawnPointIndex], 2f, enemyLayerMask))
        {
            GameObject enemy = Instantiate(_enemyPrefabs[prefabIndex], _spawnPoint[spawnPointIndex], rotations[rotationIndex]);
            enemy.GetComponent<EnemyObject>().SetEnemyDefaultData(_enemyPrefabs[prefabIndex].GetComponent<EnemyObject>().material);
            enemy.GetComponent<Rigidbody2D>().gravityScale = enemy.GetComponent<EnemyObject>().fallSpeed;
        }
        else
        {
            Debug.Log("Unable to spawn");
        }
    }

    public void SpawnEnemyDefaultScaleSpeed(int prefabIndex, int spawnPointIndex, int rotationIndex,float speedScale)
    {
        if (SpawnCheck(_spawnPoint[spawnPointIndex], 2f, enemyLayerMask))
        {
            GameObject enemy = Instantiate(_enemyPrefabs[prefabIndex], _spawnPoint[spawnPointIndex], rotations[rotationIndex]);
            enemy.GetComponent<EnemyObject>().SetEnemyDefaultData(_enemyPrefabs[prefabIndex].GetComponent<EnemyObject>().material);
            enemy.GetComponent<Rigidbody2D>().gravityScale = enemy.GetComponent<EnemyObject>().fallSpeed * speedScale;
        }
        else
        {
            Debug.Log("Unable to spawn");
        }
    }

    public void SpawnEnemyDefault(int prefabIndex, Vector2 spawnPoint, int rotationIndex,float fallspeed = 0.5f)
    {
        GameObject enemy = Instantiate(_enemyPrefabs[prefabIndex], spawnPoint, rotations[rotationIndex]);
        //enemy.SetEnemyDefaultData(_enemyPrefabs[prefabIndex].GetComponent<EnemyObject>().material);
        enemy.GetComponent<Rigidbody2D>().gravityScale = fallspeed;
    }

    public void SpawnEnemyCustom(int prefabIndex, int spawnPointIndex, int rotationIndex, int health, int damage, int score, float fallSpeed)
    {
        if (SpawnCheck(_spawnPoint[spawnPointIndex], 2f, enemyLayerMask))
        {
            GameObject enemy = Instantiate(_enemyPrefabs[prefabIndex], _spawnPoint[spawnPointIndex], rotations[rotationIndex]);
            //enemy.SetEnemyCustomData(health, damage, score, fallSpeed);
            enemy.GetComponent<Rigidbody2D>().gravityScale = fallSpeed;
        }
        else
        {
            Debug.Log("Unable to spawn");
        }
    }
}
