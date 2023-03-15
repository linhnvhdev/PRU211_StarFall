using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayControllerScript : MonoBehaviour
{
    private float horizontal;
    private float speed = 3f;
    private float jumpingPower = 20f;
    private bool isFacingLeft = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask enemyLayer;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && IsGrounded() )
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        else if (Input.GetButtonDown("Jump") && IsOnBlock())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        //if (mousePos.x < this.transform.position.x && isFacingLeft)
        //{
        //    Flip();
        //}
        //else if (mousePos.x > this.transform.position.x  && !isFacingLeft)
        //{
        //    Flip();
        //}
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.8f, 0.19f),CapsuleDirection2D.Horizontal,0, groundLayer);
    }
    private bool IsOnBlock()
    {
        //return Physics2D.OverlapCircle(groundCheck.position, 0.2f, enemyLayer);
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.8f, 0.19f), CapsuleDirection2D.Horizontal, 0, enemyLayer);

    }
    private void Flip()
    {
         
            isFacingLeft = false;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
        //transform.Rotate(0f, -180f, 0f);
         
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
