using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayControllerScript : MonoBehaviour
{    // test
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Consumable"))
        {
            string itemType = collision.gameObject.GetComponent<ConsumableScript>().itemType;
            if (itemType.Equals("hp1"))
            {
                Debug.Log("add 1 health");
            }
            else if (itemType.Equals("coin"))
            {
                //+2 hp
                Debug.Log("add 1 coin");
            }
            else if (itemType.Equals("shield"))
            {

                Debug.Log("add 1 shield");
            }
            // Destroy item which is comsumed
            Destroy(collision.gameObject);
        }
    }
}
