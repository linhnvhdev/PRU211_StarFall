using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWeapon : MonoBehaviour
{
    public bool IsAttack = true;
    private Weapon weapon;
    public int range = 1;

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
        Debug.Log("Hit!");
        if (!IsAttack || ! weapon.IsActive)
            return;
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().curHealth -= weapon.Damage;
            Debug.Log($"enemyHealth:{collision.gameObject.GetComponent<Enemy>().curHealth}");
        }
    }

    private void OnDrawGizmosSelected()
    {
        
    }
}
