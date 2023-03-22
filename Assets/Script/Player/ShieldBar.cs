using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBar : MonoBehaviour
{
    [SerializeField]
    private Player playerShield;
    [SerializeField]
    private Image totalShieldBar;
    [SerializeField]
    private Image currentShieldBar;
    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectOfType<Player>() != null)
        {
            playerShield = GameObject.FindObjectOfType<Player>();
            totalShieldBar.fillAmount = playerShield.shield / 10;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<Player>() != null)
        {
            currentShieldBar.fillAmount = playerShield.shield / 10;

        }
    }
}
