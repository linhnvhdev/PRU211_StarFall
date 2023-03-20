using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackMageBullet : MonoBehaviour
{
    public Bomb bombCore;
    public float range = 3;
    public LayerMask enemyLayerMask;
    public LevelPointManager lvpointManager;
    public PlayerLevelController levelController;

    // Start is called before the first frame update
    void Start()
    {
        lvpointManager = FindObjectOfType<LevelPointManager>();
        levelController = FindObjectOfType<PlayerLevelController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!(collision.gameObject.tag == "Enemy")) return;
        //var enemy = collision.GetComponent<EnemyObject>();

        var enemyList = Physics2D.OverlapCircleAll((Vector2)transform.position, range, enemyLayerMask);
        UnityEngine.Debug.Log("buum hit " + enemyList.Length);
        foreach (var collider in enemyList)
        {
            var enemy = collider.gameObject.GetComponent<EnemyObject>();
            //Debug.Log(enemy.gameObject.name);
            if(enemy.gameObject.GetComponent<EnemyObject>().curentHealth <= bombCore.damage)
            {
                
                if (lvpointManager != null)
                {
                    lvpointManager.totalPoint += enemy.score;
                }
                
                if (levelController != null)
                    levelController.exp += enemy.score;
            }
        }
        bombCore.currentTime = 0f;
    }
}
