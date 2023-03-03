using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int curHealth;
    public bool isTargeted;

    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void IsHit(int damage)
    {
        curHealth -= damage;
        Debug.Log("curhealth: " + curHealth);
        if(curHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void IsTargeted()
    {
        Debug.Log("I'm being targeted");
        this.GetComponent<SpriteRenderer>().color = Color.green;
        isTargeted = true;
    }

    public void ExitTargeted()
    {
        Debug.Log("I'm not being targeted");
        this.GetComponent<SpriteRenderer>().color = Color.red;
        isTargeted = false;
    }
}
