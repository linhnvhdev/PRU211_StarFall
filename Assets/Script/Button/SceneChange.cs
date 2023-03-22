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
    public void changeSettingsScene()
    {
        SceneManager.LoadScene("SettingScene");
    }

    public void changeCampaignLevel4Scene()
    {
        SceneManager.LoadScene("Level 4");
    }
    public void changeCampaignLevel3Scene()
    {
        SceneManager.LoadScene("Level3");
    }
    public void changeCampaignLevel1Scene()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void changeCampaignLevel2Scene()
    {
        SceneManager.LoadScene("Level 2");
    }
    public void chooseCharacterScene()
    {
        SceneManager.LoadScene("Choose Character");
    }
    public void chooseGuide()
    {
        SceneManager.LoadScene("Guide");
    }
}
