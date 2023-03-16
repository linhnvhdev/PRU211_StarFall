using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField]
    private float startingHealth;
    public float currentHealth { get; private set; }
    // Start is called before the first frame update
    private void Awake()
    {
        currentHealth = startingHealth;
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
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.E)){
    //        TakeDamage(1);
    //    }
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            TakeDamage(0.5f);
            Destroy(collision.gameObject);

        }


    }
}
 