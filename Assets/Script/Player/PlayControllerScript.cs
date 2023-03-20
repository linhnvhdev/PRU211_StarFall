using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayControllerScript : MonoBehaviour
{
    private float horizontal;
    public float speed = 6f;
    public float jumpingPower = 20f;
    public bool isFacingLeft = true;
    public Vector2 scale;
      private RangeWeapon rangeWeapon;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask enemyLayer;

    // Start is called before the first frame update
      void Start()
    {
        rangeWeapon = GetComponent<RangeWeapon>();
       }
    // Update is called once per frame
    void Update() 
    {
 
        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        else if (Input.GetButtonDown("Jump") && IsOnBlock())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        else if (horizontal > 0.5f)
        {
            transform.localScale = new Vector2(scale.x * -1, scale.y * 1);

        }
        else if (horizontal < -0.5f)
        {
            transform.localScale = new Vector2(scale.x * 1, scale.y * 1);
 
        }



    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.8f, 0.19f), CapsuleDirection2D.Horizontal, 0, groundLayer);
    }
    private bool IsOnBlock()
    {
        //return Physics2D.OverlapCircle(groundCheck.position, 0.2f, enemyLayer);
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.8f, 0.19f), CapsuleDirection2D.Horizontal, 0, enemyLayer);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Consumable"))
        {
            string itemType = collision.gameObject.GetComponent<ConsumableScript>().itemType;
            if (itemType.Equals("hp1"))
            {
                Debug.Log("add 1 health");
            }
            else if (itemType.Equals("coin"))
            {
                //+2 hp
                Debug.Log("add 1 coin");
            }
            else if (itemType.Equals("shield"))
            {

                Debug.Log("add 1 shield");
            }
            // Destroy item which is comsumed
            Destroy(collision.gameObject);
        }

    }
}
