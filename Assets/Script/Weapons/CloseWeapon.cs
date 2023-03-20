using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CloseWeapon : MonoBehaviour
{
    public bool IsAttack = true;
    public Transform AttackPoint;
    private Weapon weapon;
    public float attackRange = 2;
    public LayerMask enemyLayer;
     private Vector2 mousePosition;
    public float Offset;


    void Start()
    {
        weapon = GetComponent<Weapon>();
        //this.GetComponent<CircleCollider2D>().radius = attackRange;
    }

    // Update is called once per frame
    void Update()
    {
      
        // attack if press mouse
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
        
    }

    

    

    public void Attack()
    {
        transform.Rotate(180f, 0f, 0f);
         Collider2D[] collider=Physics2D.OverlapCircleAll(AttackPoint.position, attackRange,enemyLayer);  
        foreach(Collider2D c in collider)
        {
            if (!(c.gameObject.tag == "Enemy")) return;
            var enemy = c.GetComponent<EnemyObject>();
            enemy.IsHit(weapon.Damage);
            if(enemy.curentHealth <= 0)
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
         }
    }
}
