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
    public float Offset;
    public float OffsetFirePoint = -90;


    // Start is called before the first frame update
    void Start()
    {
        weapon = GetComponent<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ= Mathf.Atan2(difference.y,difference.x)*Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + Offset);
        firePoint.transform.rotation = Quaternion.Euler(0f, 0f, rotZ + OffsetFirePoint);
        //transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
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
