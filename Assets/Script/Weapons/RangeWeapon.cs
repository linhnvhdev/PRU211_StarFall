using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangeWeapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Rigidbody2D rb;
    private Weapon weapon;
    public float bulletForce = 20f;

    // Start is called before the first frame update
    void Start()
    {
        weapon = GetComponent<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle - 135;
        firePoint.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        if (!weapon.CanAttack) return;
        weapon.Attack();
        GameObject bullet = Instantiate(bulletPrefab,firePoint.position,firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        bullet.GetComponent<Weapon>().Damage = weapon.Damage;
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
