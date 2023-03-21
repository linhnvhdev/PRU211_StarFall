using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static Enum;

public class Lootbag : MonoBehaviour
{
    public GameObject droppedItemPrefab;
    public List<Loot> lootList = new List<Loot>();

    Loot GetDroppedItem()
    {
        int randomNumber = Random.Range(1, 101); //1-100
        List<Loot> possibleItems = new List<Loot>();
        foreach (Loot item in lootList)
        {
            if (randomNumber <= item.dropChance)
            {
                possibleItems.Add(item);
            }
        }
        if (possibleItems.Count > 0)
        {
            Loot droppedItem = possibleItems[0];
            foreach (Loot item in possibleItems)
            {
                if (item.dropChance < droppedItem.dropChance)
                {
                    droppedItem = item;
                }
            }
            return droppedItem;
        }
        return null;
    }

    public void InstantiateLoot(Vector3 spawnPosition)
    {
        Loot droppedItem = GetDroppedItem();       
        if (droppedItem != null)
        {
            GameObject lootGameObject = Instantiate(droppedItemPrefab, spawnPosition, Quaternion.identity);
            string name = droppedItem.lootName;
            lootGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.lootSprite;
            ItemType itemType = lootGameObject.GetComponent<ConsumableScript>().itemType;
            switch (name)
            {
                case "HP1":
                    itemType = ItemType.HP1;
                    break;
                case "HP2":
                    itemType = ItemType.HP2;
                    break;
                case "HP3":
                    itemType = ItemType.HP3;
                    break;
                case "SHIELD":
                    itemType = ItemType.SHIELD;
                    break;
                case "COIN":
                    itemType = ItemType.COIN;
                    break;
                case "BOMB":
                    itemType = ItemType.BOMB;
                    break;
            }
        }
    }
}