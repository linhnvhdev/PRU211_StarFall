using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public TextMeshProUGUI pointText;
    public LevelPointManager levelPointManager;
   public void ScoreCount(int totalPoint)
    {
        pointText.text = totalPoint.ToString();

    }
        
}
