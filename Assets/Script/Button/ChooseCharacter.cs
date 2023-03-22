using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChooseCharacter : MonoBehaviour
{
    //public void SaveSelectedCharacter(GameObject selectedCharacterPrefab)

    public void Start()
    {
        Debug.Log("Check String Pref: " + PlayerPrefs.GetString("chooseCharacter"));
        PlayerPrefs.DeleteKey("chooseCharacter");
    }
    public void SaveSelectedCharacter()
    {
        string key = "chooseCharacter";
        string value = "";
        // if (button == black mage
        if (this.name == "ButtonKnight")
        {
            value = "Knight";
        }
        if (this.name == "ButtonMage")
        {
            value = "Mage";
        }
        if (this.name == "ButtonGunner")
        {
             value = "Gunner"; 
        }
        if (this.name == "ButtonGoblin")
        {
            value = "Goblin";
        }
        PlayerPrefs.SetString(key, value);
        Debug.Log("Choose character: " + value);
        SceneManager.LoadScene("GameMode");
    }
}
