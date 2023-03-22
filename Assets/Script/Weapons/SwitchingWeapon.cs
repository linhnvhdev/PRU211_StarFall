using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchingWeapon : MonoBehaviour
{

    int totalWeapon = 1;
    int totalWeaponIcon = 1;
    public int currentWeaponIndex;
    public GameObject[] weapons;
    public GameObject[] weaponsIcons;
    public GameObject weaponHolder;
    public GameObject currentWeapon;
    public GameObject weaponIconHolder;
    // Start is called before the first frame update
    void Start()
    {
        totalWeapon = weaponHolder.transform.childCount;
        weaponIconHolder = GameObject.Find("WeaponMiniIcon");
        totalWeaponIcon = weaponIconHolder.transform.childCount;
        weaponsIcons = new GameObject[totalWeaponIcon];
        weapons = new GameObject[totalWeapon];
        for (int i = 0; i < totalWeapon; i++)
        {
            weapons[i] = weaponHolder.transform.GetChild(i).gameObject;
            weapons[i].SetActive(false);
        }
        for (int i = 0; i < totalWeaponIcon; i++)
        {
            weaponsIcons[i] = weaponIconHolder.transform.GetChild(i).gameObject;
            weaponsIcons[i].SetActive(false);
        }
        weapons[0].SetActive(true);
        currentWeapon = weapons[0];
        for (int i = 0; i < totalWeaponIcon; i++)
        {
            if (weaponsIcons[i].name.ToLower().Equals(currentWeapon.name.ToLower()))
            {
                weaponsIcons[i].SetActive(true);

            }
        }
        currentWeaponIndex = 0;
    }
    public void UpgradeWeapon()
    {
        if (currentWeaponIndex < totalWeapon - 1)
        {
            Debug.Log("Upgrade");
            weapons[currentWeaponIndex].SetActive(false);
            for (int i = 0; i < totalWeaponIcon; i++)
            {
                if (weaponsIcons[i].name.ToLower().Equals(weapons[currentWeaponIndex].name.ToLower()))
                {
                    weaponsIcons[i].SetActive(false);

                }
            }
            currentWeaponIndex += 1;
            currentWeapon = weapons[currentWeaponIndex];
            weapons[currentWeaponIndex].SetActive(true);
            for (int i = 0; i < totalWeaponIcon; i++)
            {
                if (weaponsIcons[i].name.ToLower().Equals(currentWeapon.name.ToLower()))
                {
                    weaponsIcons[i].SetActive(true);

                }
            }
        }
    }


}
