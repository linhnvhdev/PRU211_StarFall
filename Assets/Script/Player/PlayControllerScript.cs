using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayControllerScript : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || isFacingRight && horizontal > 0f)
        {
            isFacingRight= false;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale= localScale;
        }
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
