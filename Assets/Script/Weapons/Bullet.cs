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
        if (enemy.IsDestroyed())
        {
            var lvPointManager = FindObjectOfType<LevelPointManager>();
            if (lvPointManager != null)
            {
                lvPointManager.IncreasePoint(enemy.score);
            }
        }
        Destroy(gameObject);
    }
}
