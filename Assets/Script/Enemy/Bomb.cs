using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Bomb : MonoBehaviour
{
    public GameObject BombCore;
    public float ExploseTime = 10;
    public float currentTime;
    public int range = 2;
    public int damage = 10;
    public int damageToPlayer = 2;
    public LayerMask enemyLayerMask;
    public LayerMask playerLayerMask;
    private GameObject countdownText;
    public float bombSpeed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = ExploseTime;
        enemyLayerMask = LayerMask.GetMask("Enemy");
        playerLayerMask = LayerMask.GetMask("Player");
        UnityEngine.Debug.Log("Start");
        // Countdown text
        countdownText = new GameObject("myText");
        countdownText.AddComponent<TextMeshPro>();
        countdownText.AddComponent<MeshRenderer>();
        UnityEngine.Debug.Log("before textmesh");
        TextMeshPro textMeshComponent = countdownText.GetComponent <TextMeshPro>();
        MeshRenderer meshRendererComponent = countdownText.GetComponent<MeshRenderer>();
        textMeshComponent.text = currentTime.ToString("F1");
        textMeshComponent.color = Color.white;
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
    void Update()
    {
        UnityEngine.Debug.Log("In update");
        currentTime -= Time.deltaTime;
        countdownText.GetComponent<TextMeshPro>().text = currentTime.ToString("F1");
        countdownText.transform.position = transform.position;
        if (currentTime <= 0)
        {
            Explose();
        }
        if (BombCore.IsDestroyed())
        {
            //Destroy(countdownText);
            Destroy(gameObject);
        }
    }

    private void Explose()
    {
        var enemyList = Physics2D.OverlapCircleAll((Vector2) transform.position,range, enemyLayerMask);
        UnityEngine.Debug.Log("buum hit " + enemyList.Length);
        foreach (var enemy in enemyList)
        {
            Debug.Log(enemy.gameObject.name);
            enemy.gameObject.GetComponent<EnemyObject>().IsHit(damage);
        }
        var player = Physics2D.OverlapCircle((Vector2)transform.position, range, playerLayerMask);
        if(player != null)
            player.GetComponentInChildren<Health>().TakeDamage(damageToPlayer);
        Destroy(countdownText);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Destroy(countdownText);
    }
}
