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
    public TextMeshProUGUI txtWeaponName;
    // Start is called before the first frame update
    void Start()
    {
        switchingWeapon = GameObject.FindObjectOfType<SwitchingWeapon>();
        playerLevelController = GameObject.FindObjectOfType<PlayerLevelController>();
        levelPointManager = GameObject.FindObjectOfType<LevelPointManager>();
    }

    // Update is called once per frame
    void Update()
    {
        txtWeaponName.text = switchingWeapon.currentWeapon.name.ToUpper();
        txtExp.text = playerLevelController.exp.ToString() + " / " + playerLevelController.nextLevelExp.ToString();
        txtLevel.text = playerLevelController.level.ToString();
        txtScore.text = levelPointManager.totalPoint.ToString();

    }
}
// score : total point trong levelPointMa
// level,exp : player level controller 
