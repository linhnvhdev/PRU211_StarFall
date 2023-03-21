using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameWinScreen : MonoBehaviour
{
    public Text pointsText;
    public void Setup(int score)
    {
        gameObject.SetActive(true);
        pointsText.text = score.ToString() + "POINTS";
    }
    public void NextLvButton()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void ReTryButton()
    {
        SceneManager.LoadScene("Level 2");
    }
    public void ExitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
