using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CloseWeapon : MonoBehaviour
{
    public bool IsAttack = true;
    public Transform AttackPoint;
    private Weapon weapon;
    public float attackRange = 1;
    public LayerMask enemyLayer;
    public List<Collider2D> enemyInRange;
    private Vector2 mousePosition;

    void Start()
    {
        weapon = GetComponent<Weapon>();
        //this.GetComponent<CircleCollider2D>().radius = attackRange;
    }

    // Update is called once per frame
    void Update()
    {
        DetectEnemy();

        // attack if press mouse
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    public void DetectEnemy()
    {
        var newEnemyInRange = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, enemyLayer).ToList();
        var enemyOutOfRange = enemyInRange.Except(newEnemyInRange);
        foreach (var enemy in enemyOutOfRange)
        {
            enemy.GetComponent<Enemy>().ExitTargeted();
        }
        enemyInRange = newEnemyInRange;
        mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Target enemy
        enemyInRange = enemyInRange.OrderBy(x => Vector2.Distance(x.gameObject.transform.position, mousePosition)).ToList();
        foreach (var enemy in enemyInRange)
        {
            //Debug.Log($"enemy {enemy.GetInstanceID()} {Vector2.Distance(enemy.gameObject.transform.position, mousePosition)}");
            enemy.GetComponent<Enemy>().ExitTargeted();
        }
        if (enemyInRange.Count > 0)
        {
            enemyInRange.ElementAt(0).GetComponent<Enemy>().IsTargeted();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(AttackPoint.transform.position, attackRange);
    }

    public void Attack()
    {
        if (!weapon.CanAttack) return;
        weapon.Attack();
        // Animation

        foreach(Collider2D enemy in enemyInRange)
        {
            var curEnemy = enemy.GetComponent<Enemy>();
            if (curEnemy.isTargeted)
                enemy.GetComponent<Enemy>().IsHit(weapon.Damage);
        }
    }
}
