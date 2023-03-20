using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Enum;

public class EnemyObject : MonoBehaviour
{
    public int maxHealth;
    public int curentHealth;
    public int damage;
    public int score;
    public float fallSpeed;
    public BlockType material;
    public bool isTargeted;

    public void SetEnemyDefaultData(BlockType material)
    {
        switch (material)
        {
            case BlockType.WOOD:
                this.maxHealth = 1;
                this.curentHealth = maxHealth;
                this.damage = 1;
                this.score = 1;
                this.fallSpeed = 1f;               
                this.material = BlockType.WOOD;
                break;
            case BlockType.STONE:
                this.maxHealth = 2;
                this.curentHealth = maxHealth;
                this.damage = 1;
                this.score = 2;
                this.fallSpeed = 2f;
                this.material = BlockType.STONE;
                break;
            case BlockType.IRON:
                this.maxHealth = 3;
                this.curentHealth = maxHealth;
                this.damage = 2;
                this.score = 6;
                this.fallSpeed = 3f;
                this.material = BlockType.IRON;
                break;
            case BlockType.GOLD:
                this.maxHealth = 4;
                this.curentHealth = maxHealth;
                this.damage = 2;
                this.score = 8;
                this.fallSpeed = 4f;
                this.material = BlockType.GOLD;
                break;
            case BlockType.DIAMOND:
                this.maxHealth = 5;
                this.curentHealth = maxHealth;
                this.damage = 3;
                this.score = 15;
                this.fallSpeed = 5f;
                this.material = BlockType.DIAMOND;
                break;
        }
    }

    void Start()
    {
        if(material != BlockType.CUSTOM)
            SetEnemyDefaultData(material);
        foreach (Transform child in transform)
        {
            child.gameObject.AddComponent<EnemyObject>();
            child.gameObject.GetComponent<EnemyObject>().SetEnemyDefaultData(material);
        }
    }

    public void SetEnemyCustomData(int maxHealth, int damage, int score, float fallSpeed)
    {
        this.maxHealth = maxHealth;
        this.curentHealth = maxHealth;
        this.damage = damage;
        this.score = score;
        this.fallSpeed = fallSpeed;
        this.material = BlockType.CUSTOM;
    }

    public void IsHit(int damage)
    {
        curentHealth -= damage;
        Debug.Log("curhealth: " + curentHealth);
        if (curentHealth <= 0)
        {
            Destroy();
        }
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    public void OnDestroy()
    {
        var lvpointManager = FindObjectOfType<LevelPointManager>();
        if(lvpointManager != null)
        {
            lvpointManager.totalPoint += score;
        }
    }
}
