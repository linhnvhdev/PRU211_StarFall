using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public bool isGrounded = false;
    public float speed;
    public LayerMask enemyLayerMask;
    private List<GameObject> collisions = new List<GameObject>();
    private Quaternion blockRotation;

    // Start is called before the first frame update
    void Start()
    {
        blockRotation = transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {     
        if (!checkBelow() && !isGrounded)
        {
            if (blockRotation == Quaternion.Euler(0, 0, 90))
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
            }
            else if (blockRotation == Quaternion.Euler(0, 0, -90))
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
            else if (blockRotation == Quaternion.Euler(0, 0, 180))
            {
                transform.Translate(Vector2.up * speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector2.down * speed * Time.deltaTime);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    private bool checkBelow()
    {
        foreach (Transform tr in transform)
        {
            Collider2D collider = Physics2D.OverlapCircle(new Vector2(tr.position.x, tr.position.y - 0.51f), .01f, enemyLayerMask);
            //GameObject sth = new GameObject();
            //sth.transform.position = new Vector2(tr.position.x, tr.position.y - 1);
            //Instantiate(sth);
            //var lineRender = sth.gameObject.AddComponent<LineRenderer>();
            //UnityEngine.Debug.Log("LineRender");
            //lineRender.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
            //lineRender.sortingOrder = 10;
            //var drawCircle = sth.gameObject.AddComponent<DrawCircleController>();
            //drawCircle.range = 0.1f;
            if (collider == null)
            {
                //return false;
                continue;
            }
            if (collider.gameObject.transform.parent != tr.parent)
            {
                return true;
            }
        }
        return false;
    }
}
