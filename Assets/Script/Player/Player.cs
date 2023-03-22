using Assets.Script.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float startingHealth;
    public float currentHealth;
    public float speed = 6f;
    public float jumpingPower = 20f;
    public float shield = 0;
    public IPlayerType type;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        type = GetComponent<IPlayerType>();
        type.SetBaseStat();
    }

    public void TakeDamage(float _damage)
    {

        if (_damage <= shield)
        {
            shield -= _damage;
        }
        else
        {
            currentHealth -= (_damage - shield);
            shield = 0;
        }

        if (currentHealth > 0)
        {
            Debug.Log("Player hurt");
        }
        else
        {
            Debug.Log("Player died");
        }
    }

    public void SetHealth(float health)
    {
        currentHealth = health;
    }

    public void IncreaseHealth(float health)
    {
        currentHealth += health;
        if (currentHealth > startingHealth) currentHealth = startingHealth;
    }

    public void IncreaseMaxhealth(float health)
    {
        currentHealth += health;
        startingHealth += health;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
