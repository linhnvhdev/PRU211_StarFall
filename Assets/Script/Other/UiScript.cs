using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiScript : MonoBehaviour
{
    private LevelPointManager levelPointManager;
    private SwitchingWeapon switchingWeapon;
    private PlayerLevelController playerLevelController;
    public TextMeshProUGUI txtLevel;
    public TextMeshProUGUI txtExp;
    public TextMeshProUGUI txtScore;
    public TextMeshProUGUI skillCooldownText;
    private PlayerSkillController playerSkillController;
    private float skillCooldown;
    private float timer;
     void Start()
    {
        switchingWeapon = GameObject.FindObjectOfType<SwitchingWeapon>();
        playerLevelController = GameObject.FindObjectOfType<PlayerLevelController>();
        levelPointManager = GameObject.FindObjectOfType<LevelPointManager>();
        playerSkillController = FindObjectOfType<PlayerSkillController>();
        skillCooldown = playerSkillController.SkillCooldown;
        timer = playerSkillController.timer;
            skillCooldownText.enabled = true;


    }

    // Update is called once per frame
    void Update()
    {
        txtExp.text = playerLevelController.exp.ToString() + " / " + playerLevelController.nextLevelExp.ToString();
        txtLevel.text = playerLevelController.level.ToString();
        txtScore.text = levelPointManager.totalPoint.ToString();
        timer += Time.time;
         //if(timer >= skillCooldown )
         if(playerSkillController.CanActivate== true )
        {
            skillCooldownText.enabled = true;

        }
        else /*if (timer < skillCooldown)*/
            skillCooldownText.enabled = false;








    }


}
 