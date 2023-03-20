using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideEnemy : MonoBehaviour
{
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = this.GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            var enemy = collision.GetComponent<EnemyObject>();
            var enemyType = enemy.material;
            switch (enemyType)
            {
                case Enum.BlockType.WOOD:
                    Debug.Log(enemyType + ":" + enemy.damage);
                    player.TakeDamage(enemy.damage);
                    Destroy(collision.transform.parent.gameObject);
                    break;
                case Enum.BlockType.STONE:
                    Debug.Log(enemyType + ":" + enemy.damage);

                    player.TakeDamage(enemy.damage);
                    Destroy(collision.transform.parent.gameObject);
                    break;
                case Enum.BlockType.IRON:
                    Debug.Log(enemyType + ":" + enemy.damage);

                    player.TakeDamage(enemy.damage);
                    Destroy(collision.transform.parent.gameObject);
                    break;
                case Enum.BlockType.GOLD:
                    Debug.Log(enemyType + ":" + enemy.damage);

                    player.TakeDamage(enemy.damage);
                    Destroy(collision.transform.parent.gameObject);
                    break;
                case Enum.BlockType.DIAMOND:
                    Debug.Log(enemyType + ":" + enemy.damage);

                    player.TakeDamage(enemy.damage);
                    Destroy(collision.transform.parent.gameObject);
                    break;
                case Enum.BlockType.CUSTOM:
                    Debug.Log(enemyType + ":" + enemy.damage);
                    var bomb = collision.transform.parent.gameObject.GetComponent<Bomb>();
                    if (bomb == null)
                    {
                        player.TakeDamage(enemy.damage);
                        Destroy(collision.transform.parent.gameObject);
                    }
                    else
                    {
                        player.TakeDamage(bomb.damageToPlayer);
                    }
                        
                    break;
            }


        }


    }
}
