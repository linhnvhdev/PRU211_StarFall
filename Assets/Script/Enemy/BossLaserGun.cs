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
    public float range = 3;
    public LayerMask enemyLayerMask;
    public LayerMask playerLayerMask;
    private GameObject countdownText;
    public int damageToPlayer = 2;
    public bool isHit = false;

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
        float width = GetComponent<SpriteRenderer>().bounds.size.x;
        Debug.Log("Laser width:" + width);
        float height = GetComponent<SpriteRenderer>().bounds.size.y;
        Debug.Log("Laser height:" + height);
        Vector2 center = transform.position;
        Debug.Log("Laser center:" + center);
        Vector2 pointA = new Vector2(center.x - (float)(range + 0.5), center.y + height);
        Vector2 pointB = new Vector2(center.x + (float)(range + 0.5), center.y - height);
        var player = Physics2D.OverlapArea(pointA, pointB);
        
    }

    public void Update()
    {
        UnityEngine.Debug.Log("Process turn off laser");
        currentTime -= Time.deltaTime;
        UnityEngine.Debug.Log(currentTime);
        float width = GetComponent<SpriteRenderer>().bounds.size.x;
        Debug.Log("Laser width:" + width);
        float height = GetComponent<SpriteRenderer>().bounds.size.y;
        Debug.Log("Laser height:" + height);
        Vector2 center = transform.position;
        Debug.Log("Laser center:" + center);
        Vector2 pointA = new Vector2(center.x - (float)(range + 0.5), center.y + height);
        Vector2 pointB = new Vector2(center.x + (float)(range + 0.5), center.y - height);
        var player = Physics2D.OverlapArea(pointA, pointB,playerLayerMask);
        if (player != null && !isHit)
        {
            player.GetComponentInChildren<Player>().TakeDamage(damageToPlayer);
            isHit = true;
        }
        if (currentTime <= 0)
        {
            UnityEngine.Debug.Log("start turn off laser");
            TurnOffLaser();
        }
    }
    private void TurnOffLaser()
    {
        Destroy(gameObject);
    }
}
