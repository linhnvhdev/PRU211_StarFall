using Assets.Script.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackMage : MonoBehaviour, IPlayerType
{
    private Player player;
    public float passiveHealthIncrease = -1;
    public float passiveSpeedIncrease = 3;
    public float Offset = 0;
    public GameObject skillBulletPrefab;
    public float bulletForce = 20f;
    public Transform firePoint;


    public void SetBaseStat()
    {
        player = GetComponent<Player>();
        player.currentHealth += (passiveHealthIncrease);
        player.speed += (passiveSpeedIncrease);
    }
    public void UseSkill()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(0f, 0f, rotZ + Offset);
        Debug.Log("Use black mage skill");
        GameObject bullet = Instantiate(skillBulletPrefab, transform.position, transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
