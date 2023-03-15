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
        transform.Rotate(0f, -180f, 0f);
    }
}
