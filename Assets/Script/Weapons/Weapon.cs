using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int Damage = 1;
    public float AttackSpeed = 1;
    public string Rank = "normal";
    // Main weapon
    public bool IsActive = true;
    public bool CanAttack = true;
    private float timer;
    private float attackCooldown;


    // Start is called before the first frame update
    void Start()
    {
        attackCooldown = 1f / AttackSpeed;
        timer = attackCooldown;
    }

    public void SetAttackSpeed(float speed)
    {
        AttackSpeed = speed;
        attackCooldown = 1f / AttackSpeed;
        timer = attackCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        CheckCooldown();
    }

    void CheckCooldown()
    {
        if (timer >= attackCooldown)
        {
            CanAttack = true;
        }
        else
        {
            CanAttack = false;
        }
        timer += Time.deltaTime;
    }

    public void Attack()
    {
        // reset cooldown
        timer = 0;
    }
}
