using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelController : MonoBehaviour
{
    public int exp = 0;
    public int level = 1;
    public int nextLevelExp = 10;
    public float increaseExpRateByLevel = 1.5f;
    public Player player;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(exp >= nextLevelExp)
        {
            level++;
            exp = exp - nextLevelExp;
            nextLevelExp = (int) Mathf.Ceil(nextLevelExp * increaseExpRateByLevel);
            PowerUpPlayer();
        }
    }

    private void PowerUpPlayer()
    {
        // do something
        player.currentHealth++;
    }
}
