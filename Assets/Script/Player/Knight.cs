using Assets.Script.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour, IPlayerType
{

    public float skillHealthIncrease = 3;
    public float activeTime = 3;

    private Player player;
    public float passiveHealthIncrease = 1;

    public void UseSkill()
    {
        Debug.Log("Using knight skill");
        StartCoroutine(ActivateSkill());
    }
    public void SetBaseStat()
    {
        player = GetComponent<Player>();
        player.IncreaseMaxhealth(passiveHealthIncrease);
    }

    IEnumerator ActivateSkill()
    {
        player.shield += skillHealthIncrease;
        yield return new WaitForSeconds(activeTime);
        player.shield -= skillHealthIncrease;
        if (player.shield < 0)
        {
            player.shield = 0;
        }
        Debug.Log("after go back: " + player.currentHealth);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
