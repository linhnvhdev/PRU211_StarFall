using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchingWeapon : MonoBehaviour
{
    int totalWeapon = 1;
    public int currentWeaponIndex ;
    public GameObject[] weapons;
    public GameObject weaponHolder;
    public GameObject currentWeapon;
    // Start is called before the first frame update
    void Start()
    {
        totalWeapon = weaponHolder.transform.childCount;
        weapons= new GameObject[totalWeapon];
        for(int i =0; i < totalWeapon; i++)
        {
            weapons[i] = weaponHolder.transform.GetChild(i).gameObject;
            weapons[i].SetActive(false);
        }
        weapons[0].SetActive(true);
        currentWeapon = weapons[0];
        currentWeaponIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // switching to the next weapon
        if(Input.GetKeyUp(KeyCode.N))
        {
            if(currentWeaponIndex < totalWeapon -1 )
            {
                weapons[currentWeaponIndex].SetActive(false);
                currentWeaponIndex += 1;
                weapons[currentWeaponIndex].SetActive(true);
                currentWeapon= weapons[currentWeaponIndex];
            }
        }
        // switching back  to the previous weapon
        if (Input.GetKeyUp(KeyCode.M))
        {
            if (currentWeaponIndex >0)
            {
                weapons[currentWeaponIndex].SetActive(false);
                currentWeaponIndex -= 1;
                currentWeapon= weapons[currentWeaponIndex];
                weapons[currentWeaponIndex].SetActive(true);
            }
        }
    }
}
