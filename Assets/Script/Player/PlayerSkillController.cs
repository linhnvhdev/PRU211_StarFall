using Assets.Script.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillController : MonoBehaviour
{
    public bool IsActive = true;
    public bool CanActivate = true;
    public float timer;
    public float SkillCooldown;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer >= SkillCooldown)
        {
            CanActivate = true;
        }
        else
        {
            CanActivate = false;
        }
        if (CanActivate && Input.GetKeyUp(KeyCode.T))
        {
            UseSkill();
        }
        timer += Time.deltaTime;
    }

    public void UseSkill()
    {
        Debug.Log("Use Skill");
        GetComponent<IPlayerSkill>().UseSkill();
        timer = 0;
    }
}
