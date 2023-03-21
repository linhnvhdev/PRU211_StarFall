using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCharacter : MonoBehaviour
{
    [SerializeField] public GameObject[] _PlayerPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("The character player choosen: " + PlayerPrefs.GetString("chooseCharacter"));
        string characterName = PlayerPrefs.GetString("chooseCharacter");
        if (characterName == "Knight")
        {
            GameObject player = Instantiate(_PlayerPrefabs[0]);
        }
        if (characterName == "Mage")
        {
            GameObject player = Instantiate(_PlayerPrefabs[1]);
        }
        if (characterName == "Gunner")
        {
            GameObject player = Instantiate(_PlayerPrefabs[2]);
        }
        if (characterName == "Goblin")
        {
            GameObject player = Instantiate(_PlayerPrefabs[3]);
        }
    }

    
}
