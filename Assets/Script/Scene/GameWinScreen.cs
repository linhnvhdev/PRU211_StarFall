using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Enum;

public class GameWinScreen : MonoBehaviour
{
    public TextMeshProUGUI pointsText;
    public void Setup(int score)
    {
        gameObject.SetActive(true);
        pointsText.text = score.ToString() + " POINTS";
    }
    public void NextLvButton()
    {
        ;
        if (SceneManager.GetActiveScene().buildIndex == (int)SceneIndex.Level1)
        {
           
            SceneManager.LoadScene((int)SceneIndex.Level2);
        }
        else if (SceneManager.GetActiveScene().buildIndex == (int)SceneIndex.Level2)
        {
            
            SceneManager.LoadScene((int)SceneIndex.Level4);
        }
        else if (SceneManager.GetActiveScene().buildIndex == (int)SceneIndex.Level4)
        {
            
            SceneManager.LoadScene((int)SceneIndex.Level3);
        }
    }
    public void ReTryButton()
    {
        Debug.Log("Rerey");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ExitButton()
    {
        SceneManager.LoadScene("StartScene");
    }
}
