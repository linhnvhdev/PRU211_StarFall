using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UIElements;
using static Enum;

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
    public Player player;
    Animator animator;
    private string currentState;
    //Animation States
    const string PLAYER_IDLE = "Player_Idle";
    const string PLAYER_RUN = "Player_Run";
    const string PLAYER_JUMP = "Player_Jump";
    const string PLAYER_SWORD = "Player_Sword";
    // Start is called before the first frame update
    void Start()
    {
        rangeWeapon = GetComponent<RangeWeapon>();
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        speed = player.speed;
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
            var rangeweapon = GetComponentInChildren<RangeWeapon>();
            if (rangeweapon != null)
            {
                rangeweapon.Offset = -45;
            }

        }
        else if (horizontal < -0.5f)
        {
            transform.localScale = new Vector2(scale.x * 1, scale.y * 1);
            var rangeweapon = GetComponentInChildren<RangeWeapon>();
            if (rangeweapon != null)
            {
                rangeweapon.Offset = -135;
            }
        }

        if (IsGrounded() || IsOnBlock())
        {
            if (horizontal != 0)
            {
                ChangeAnimationState(PLAYER_RUN);
            }
            else
            {
                ChangeAnimationState(PLAYER_IDLE);
            }
        }
        if (!IsGrounded() && !IsOnBlock())
        {
            ChangeAnimationState(PLAYER_JUMP);
        }
    }
    private void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        currentState = newState;
        animator.Play(newState);
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
            ItemType itemType = collision.gameObject.GetComponent<ConsumableScript>().itemType;
            switch (itemType)
            {
                case ItemType.HP1:
                    Debug.Log("+1 hp");
                    player.currentHealth += 1;
                    if (player.currentHealth > player.startingHealth)
                    {
                        player.currentHealth = player.startingHealth;
                    }

                    break;
                case ItemType.HP2:
                    Debug.Log("+2 hp");
                    player.currentHealth += 2;
                    if (player.currentHealth > player.startingHealth)
                    {
                        player.currentHealth = player.startingHealth;
                    }

                    break;
                case ItemType.HP3:
                    Debug.Log("+3 hp");
                    player.currentHealth += 3;
                    if (player.currentHealth > player.startingHealth)
                    {
                        player.currentHealth = player.startingHealth;
                    }

                    break;
                case ItemType.SHIELD:
                    Debug.Log("shield");
                    float activeTime = 10;
                    float passiveHealth = 2;
                    StartCoroutine(ActivateShield(activeTime, passiveHealth));

                    break;
                case ItemType.BOMB:
                    Vector2 bombLocation = collision.transform.position;
                    var enemyList = Physics2D.OverlapCircleAll(bombLocation, 2, enemyLayer);
                    Debug.Log("buum bomb hit " + enemyList.Length);
                    foreach (var enemy in enemyList)
                    {
                        Destroy(enemy.gameObject);
                    }
                    break;
                case ItemType.UPGRADE:
                    //Upgrade weapon
                    GetComponentInChildren<SwitchingWeapon>().UpgradeWeapon();
                    break;
            }
            // Destroy item which is comsumed
            Destroy(collision.gameObject);
        }

    }
    IEnumerator DestroyItem(GameObject item, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(item);
    }
    IEnumerator ActivateShield(float activeTime, float passiveHealth)
    {
        float healthBefore = player.currentHealth;
        player.IncreaseHealth(passiveHealth);
        yield return new WaitForSeconds(activeTime);
        if (player.currentHealth > healthBefore)
        {
            player.SetHealth(healthBefore);
        }
        Debug.Log("after go back: " + player.currentHealth);
    }
}
