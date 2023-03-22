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
        playerHealth = GameObject.FindObjectOfType<Player>();
        totalHealthBar.fillAmount = playerHealth.currentHealth / 10;
    }
    private void Update()
    {
        playerHealth = GameObject.FindObjectOfType<Player>();
        if (playerHealth == null) return;
        currentHealthBar.fillAmount = playerHealth.currentHealth / 10;
    }
}
