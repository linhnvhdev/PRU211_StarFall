using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField]
    private float startingHealth;
    public float currentHealth { get; private set; }
    // Start is called before the first frame update
    private void Awake()
    {
        currentHealth = startingHealth;
    }
    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        if (currentHealth > 0)
        {
            Debug.Log("Player hurt");
        }
        else
        {
            Debug.Log("Player died");
        }
    }
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.E)){
    //        TakeDamage(1);
    //    }
    //}
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
                    TakeDamage(enemy.damage);
                    Destroy(collision.transform.parent.gameObject);
                    break;
                case Enum.BlockType.STONE:
                    Debug.Log(enemyType + ":" + enemy.damage);

                    TakeDamage(enemy.damage);
                    Destroy(collision.transform.parent.gameObject);
                    break;
                case Enum.BlockType.IRON:
                    Debug.Log(enemyType + ":" + enemy.damage);

                    TakeDamage(enemy.damage);
                    Destroy(collision.transform.parent.gameObject);
                    break;
                case Enum.BlockType.GOLD:
                    Debug.Log(enemyType + ":" + enemy.damage);

                    TakeDamage(enemy.damage);
                    Destroy(collision.transform.parent.gameObject);
                    break;
                case Enum.BlockType.DIAMOND:
                    Debug.Log(enemyType + ":" + enemy.damage);

                    TakeDamage(enemy.damage);
                    Destroy(collision.transform.parent.gameObject);
                    break;


            }


        }


    }
}
