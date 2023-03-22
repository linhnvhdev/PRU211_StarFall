using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Player playerHealth;
    [SerializeField]
    private Image totalHealthBar;
    [SerializeField]
    private Image currentHealthBar;

    private void Start()
    {
        if (FindObjectOfType<Player>() != null)
        {
            playerHealth = GameObject.FindObjectOfType<Player>();
            totalHealthBar.fillAmount = playerHealth.currentHealth / 10;
        }
    }
    private void Update()
    {
        if (FindObjectOfType<Player>() != null)
        {
           
            currentHealthBar.fillAmount = playerHealth.currentHealth / 10;
        }
    }
}
