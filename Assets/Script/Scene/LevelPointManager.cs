using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPointManager : MonoBehaviour
{
    public int totalPoint = 0;
    public int PointWinGame = 100;
    public int PointByTime = 4;

    public float currentTime;
    public float levelTime;
    public float nextIncreasePointTime;
    public float pointIncreaseRate = 1;

    public bool isGameEnd = false;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = levelTime;
        nextIncreasePointTime = currentTime - pointIncreaseRate;
    }

    public void IncreasePointByTime(int point)
    {
        PointByTime += point;
    }

    public void SetPointByTime(int point)
    {
        PointByTime = point;
    }

    public void GameOver(bool isGameOver)
    {
        if (!isGameOver)
            totalPoint += PointWinGame;
        isGameEnd = true;
    }

    public void IncreasePoint(int point)
    {
        totalPoint += point;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameEnd) return;
        currentTime -= Time.deltaTime;
        if(currentTime <= nextIncreasePointTime)
        {
            totalPoint += PointByTime;
            nextIncreasePointTime = currentTime - pointIncreaseRate;
        }
    }
}
