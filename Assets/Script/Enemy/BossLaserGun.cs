using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BossLaserGun : MonoBehaviour
{
    public GameObject LaserBeam;
    public float currentTime;
    public int range = 2;
    public int damage = 2;
    public LayerMask enemyLayerMask;
    public LayerMask playerLayerMask;
    private GameObject countdownText;
    public float durationTime;

    void Start()
    {
        currentTime = durationTime;
        enemyLayerMask = LayerMask.GetMask("Enemy");
        UnityEngine.Debug.Log("Start");
        // Countdown text
        countdownText = new GameObject("myText");
        countdownText.AddComponent<TextMeshPro>();
        countdownText.AddComponent<MeshRenderer>();
        UnityEngine.Debug.Log("before textmesh");
        TextMeshPro textMeshComponent = countdownText.GetComponent<TextMeshPro>();
        MeshRenderer meshRendererComponent = countdownText.GetComponent<MeshRenderer>();
        textMeshComponent.text = currentTime.ToString("F1");
        textMeshComponent.color = Color.black;
        textMeshComponent.fontSize = 5;
        textMeshComponent.sortingOrder = 10;
        textMeshComponent.alignment = TextAlignmentOptions.Center;
        UnityEngine.Debug.Log("aftertextmesh");

        // add bomb range:
        var lineRender = gameObject.AddComponent<LineRenderer>();
        UnityEngine.Debug.Log("LineRender");
        lineRender.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
        lineRender.sortingOrder = 10;
        var drawCircle = gameObject.AddComponent<DrawCircleController>();
        drawCircle.range = range;
    }
    // Update is called once per frame
    //void Update()
    //{
    //    if (isActive)
    //    {
    //        timer -= Time.deltaTime;

    //        if (timer <= 0.0f)
    //        {
    //            isActive = false;
    //            timer = duration;
    //            laserBeam.SetActive(false);
    //        }
    //    }
    //}

    // Method to activate the laser beam
    //public void ActivateLaserBeam()
    //{
    //    isActive = true;
    //    laserBeam.SetActive(true);
    //}

    //// Method to deactivate the laser beam
    //public void DeactivateLaserBeam()
    //{
    //    isActive = false;
    //    laserBeam.SetActive(false);
    //}
}
