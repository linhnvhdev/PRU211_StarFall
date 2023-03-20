using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Weapon weapon;
    // Start is called before the first frame update
    void Start()
    {
        weapon = GetComponent<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!(collision.gameObject.tag == "Enemy")) return;
        var enemy = collision.GetComponent<EnemyObject>();
        enemy.IsHit(weapon.Damage);
        if (enemy.curentHealth <= 0)
        {
            var lvpointManager = FindObjectOfType<LevelPointManager>();
            if (lvpointManager != null)
            {
                lvpointManager.totalPoint += enemy.score;
            }
            var levelController = FindObjectOfType<PlayerLevelController>();
            if (levelController != null)
                levelController.exp += enemy.score;
        }
        Destroy(gameObject);
    }
}
