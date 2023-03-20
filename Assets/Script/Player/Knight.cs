using Assets.Script.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour, IPlayerSkill
{
    public float timer = 0;


    public void UseSkill()
    {
        timer = 0;
        Debug.Log("Using knight skill");
        
    }

    IEnumerator ActivateSkill()
    {
        GetComponent<Health>().currentHealth += 3;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Debug.Log("Knigh time: " + timer);
    }
}
