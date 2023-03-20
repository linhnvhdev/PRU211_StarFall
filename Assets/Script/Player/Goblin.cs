using Assets.Script.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour, IPlayerType
{
    public float skillHealthIncrease = 3;
    public float activeTime = 3;

    private Player player;
    public float passiveHealthIncrease = 1;
    public float attackSpeedIncrease = 2;
    public float speedIncrease = 3;

    public void UseSkill()
    {
        StartCoroutine(ActivateSkill());
    }
    public void SetBaseStat()
    {
        player = GetComponent<Player>();
        player.IncreaseHealth(passiveHealthIncrease);
    }

    IEnumerator ActivateSkill()
    {
        var weapons = FindObjectsOfType<Weapon>();
        foreach (var weapon in weapons)
        {
            weapon.SetAttackSpeed(weapon.AttackSpeed * attackSpeedIncrease);
        }
        player.speed += (speedIncrease);
        yield return new WaitForSeconds(activeTime);
        // Set back
        foreach (var weapon in weapons)
        {
            weapon.SetAttackSpeed(weapon.AttackSpeed /= attackSpeedIncrease);
        }
        player.speed -= (speedIncrease);
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
