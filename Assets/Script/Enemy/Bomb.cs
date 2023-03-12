using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Bomb : MonoBehaviour
{
    public GameObject BombCore;
    public float ExploseTime = 10;
    public float currentTime;
    public int range = 2;
    public int damage = 100;
    public LayerMask enemyLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = ExploseTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        if(currentTime <= 0)
        {
            Explose();
        }
        if (BombCore.IsDestroyed())
        {
            Destroy(gameObject);
        }
    }

    private void Explose()
    {
        var list = Physics2D.OverlapAreaAll(new Vector2(transform.position.x - range, transform.position.y - range),
                                        new Vector2(transform.position.x + range, transform.position.y + range), enemyLayerMask);
        Debug.Log("buum hit " + list.Length);
        foreach (var enemy in list)
        {
            Debug.Log(enemy.gameObject.GetInstanceID());
            enemy.gameObject.GetComponent<Enemy>().IsHit(damage);
        }
        Destroy(gameObject);
    }
}
