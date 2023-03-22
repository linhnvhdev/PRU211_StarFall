using Assets.Script.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunner : MonoBehaviour, IPlayerType
{
    private Player player;
    public float passiveHealthIncrease = -1;
    public float passiveSpeedIncrease = 3;

    public float activeTime = 5;
    public float attackSpeedIncrease = 2;

    public void UseSkill()
    {
        StartCoroutine(ActivateSkill());
    }
    IEnumerator ActivateSkill()
    {
        var weapons = FindObjectsOfType<Weapon>();
        foreach (var weapon in weapons)
        {
            weapon.SetAttackSpeed(weapon.AttackSpeed * attackSpeedIncrease);
        }
        yield return new WaitForSeconds(activeTime);
        foreach (var weapon in weapons)
        {
            weapon.SetAttackSpeed(weapon.AttackSpeed /= attackSpeedIncrease);
        }
    }

    public void SetBaseStat()
    {
        player = GetComponent<Player>();
        player.IncreaseMaxhealth(passiveHealthIncrease);
        player.speed += (passiveSpeedIncrease);
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
