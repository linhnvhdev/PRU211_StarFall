using Assets.Script.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float startingHealth;
    public float currentHealth;
    public float speed = 6f;
    public float jumpingPower = 20f;
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
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
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
    }

    // Update is called once per frame
    void Update()
    {

    }
}
