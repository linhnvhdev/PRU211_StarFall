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
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        blockRotation = transform.rotation;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void LateUpdate()
    {     
        if (!checkBelow() && !isGrounded)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
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
        else
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    public bool checkBelow()
    {
        foreach (Transform tr in transform)
        {
            Collider2D collider = Physics2D.OverlapCircle(new Vector2(tr.position.x, tr.position.y - 0.51f), .01f, enemyLayerMask);
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
