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

    Animator animator;
    Animator playerAnimator;

    public bool isAttackPressed;
    public bool isAttacking;

    string currentState;

    void Start()
    {
        weapon = GetComponent<Weapon>();
        //this.GetComponent<CircleCollider2D>().radius = attackRange;

        animator = GetComponent<Animator>();
        playerAnimator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isAttackPressed = Input.GetMouseButtonDown(0);
        // attack if press mouse
        if (isAttackPressed)
        {
            if (weapon.CanAttack)
            {
                weapon.Attack();
                ChangeAnimationState("Sword_Attack");
                Invoke("Attack", 0.3f);
                Invoke("AttackComplete", 1f);
            }                    
        }

    }
    private void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        currentState = newState;
        animator.Play(newState);
    }

    void AttackComplete()
    {
        ChangeAnimationState("New State");
    }



    public void Attack()
    {
        Debug.Log("Attack");
        Collider2D[] collider = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D c in collider)
        {
            if (!(c.gameObject.tag == "Enemy")) return;
            var enemy = c.GetComponent<EnemyObject>();
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
        }
    }
}
