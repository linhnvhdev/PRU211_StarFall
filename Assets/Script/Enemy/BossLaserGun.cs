using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
public class BossLaserGun : MonoBehaviour
{
    public float durationTime = 10;
    public float currentTime;
    public int damage = 2;
    public LayerMask enemyLayerMask;
    public LayerMask playerLayerMask;
    private GameObject countdownText;
    

    void Start()
    {
        currentTime = durationTime;
        enemyLayerMask = LayerMask.GetMask("Enemy");
        UnityEngine.Debug.Log("Start create Laser Object");
        // add laser range:
        var lineRender = gameObject.AddComponent<LineRenderer>();
        UnityEngine.Debug.Log("LineRender");
        lineRender.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
        lineRender.sortingOrder = 10;
        var drawRectangle = gameObject.AddComponent<DrawRectangleController>();
    }

    public void Update()
    {
        UnityEngine.Debug.Log("Process turn off laser");
        currentTime -= Time.deltaTime;
        UnityEngine.Debug.Log(currentTime);
      //  countdownText.GetComponent<TextMeshPro>().text = currentTime.ToString("F1");
      //  countdownText.transform.position = transform.position;
        if (currentTime <= 0)
        {
            UnityEngine.Debug.Log("start turn off laser");
            TurnOffLaser();
        }
        
        //if (durationTime <= 0)
        //{
        //    Destroy(countdownText);
        //    Destroy(gameObject);
        //}
    }
    private void TurnOffLaser()
    {
        
        Destroy(gameObject);
    }
}
