using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
    public void changeGameModeScene()
    {
        SceneManager.LoadScene("GameMode");
    }
    public void changeStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }
    public void changeCampaignScene()
    {
        SceneManager.LoadScene("CampaignModeScene");
    }

    public void changeCampaignLevel4Scene()
    {
        SceneManager.LoadScene("Level 4");
    }
    public void changeCampaignLevel3Scene()
    {
        SceneManager.LoadScene("Level3");
    }

}
