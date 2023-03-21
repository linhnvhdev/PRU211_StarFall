using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerLv1 : MonoBehaviour
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
        var list = Physics2D.OverlapArea(new Vector2(spawnPoint.x - range, spawnPoint.y - 3),
                                        new Vector2(spawnPoint.x + range, spawnPoint.y + 3), layerMask);
        if (list == null)
        {
            return true;
        }
        return false;
    }

    public bool SpawnEnemyDefault(int prefabIndex, int spawnPointIndex, int rotationIndex)
    {
        if (SpawnCheck(_spawnPoint[spawnPointIndex], 2f, enemyLayerMask))
        {
            GameObject enemy = Instantiate(_enemyPrefabs[prefabIndex], _spawnPoint[spawnPointIndex], rotations[rotationIndex]);
            return true;
        }
        else
        {
            Debug.Log("Unable to spawn");
            return false;
        }
    }

    public bool SpawnEnemyDefaultScaleSpeed(int prefabIndex, int spawnPointIndex, int rotationIndex, float speedScale)
    {
        if (SpawnCheck(_spawnPoint[spawnPointIndex], 2f, enemyLayerMask))
        {
            GameObject enemy = Instantiate(_enemyPrefabs[prefabIndex], _spawnPoint[spawnPointIndex], rotations[rotationIndex]);
            //enemy.GetComponent<EnemyObject>().SetEnemyDefaultData(_enemyPrefabs[prefabIndex].GetComponent<EnemyObject>().material);
            enemy.GetComponent<EnemyMovement>().speed *= speedScale;
            return true;
        }
        else
        {
            Debug.Log("Unable to spawn");
            return false;
        }
    }

    public bool SpawnEnemyDefaultScaleSpeed(int prefabIndex, Vector2 spawnPoint, int rotationIndex, float speedScale)
    {
        if (SpawnCheck(spawnPoint, 2f, enemyLayerMask))
        {
            GameObject enemy = Instantiate(_enemyPrefabs[prefabIndex], spawnPoint, rotations[rotationIndex]);
            //enemy.GetComponent<EnemyObject>().SetEnemyDefaultData(_enemyPrefabs[prefabIndex].GetComponent<EnemyObject>().material);
            enemy.GetComponent<EnemyMovement>().speed *= speedScale;
            return true;
        }
        else
        {
            Debug.Log("Unable to spawn");
            return false;
        }
    }

    public bool SpawnEnemyDefault(int prefabIndex, Vector2 spawnPoint, int rotationIndex, float fallspeed = 3f)
    {
        if (SpawnCheck(spawnPoint, 2f, enemyLayerMask))
        {
            GameObject enemy = Instantiate(_enemyPrefabs[prefabIndex], spawnPoint, rotations[rotationIndex]);
            //enemy.SetEnemyDefaultData(_enemyPrefabs[prefabIndex].GetComponent<EnemyObject>().material);
            enemy.GetComponent<EnemyMovement>().speed = fallspeed;
            return true;
        }
        else
        {
            Debug.Log("Unable to spawn");
            return false;
        }
    }
}
