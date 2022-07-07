using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Base Attributes")]
    [SerializeField] private bool playerAlive = true;
    [SerializeField] private float playerHealth;
    [SerializeField] private float playerDamage;
    [SerializeField] private float playerMoveSpeed;
    [SerializeField] private float playerJumpSpeed;

    [Header("Damage Taken Times/Statuses")]
    [SerializeField] private bool playerHurt = false;
    [SerializeField] private bool invincibility = false;
    [SerializeField] private Vector2 hurtSpeed = new Vector2(2.5f, 2.5f);
    [SerializeField] private float hurtTime = 1f;

    [Header("Movement Checks")]
    public Transform groundCheck;
    private Vector2 moveInput;
    private Rigidbody2D playerRB;
    private CapsuleCollider2D playerCollider;
    private Animator playerAnimator;


    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        if(!playerAlive || playerHurt)
        {
            return;
        }
        Run();
        FlipSprite();
        IsJumping();
    }
    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * playerMoveSpeed, playerRB.velocity.y);
        playerRB.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(playerRB.velocity.x) > Mathf.Epsilon;
        if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            Debug.Log("OnGround");
            playerAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
        }

    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(playerRB.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerRB.velocity.x), 1f);
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if(!playerAlive || playerHurt)
        {
            return;
        }
        Collider2D groundChecker = Physics2D.OverlapCircle(groundCheck.position, 0.1f, LayerMask.GetMask("Ground"));

        if (!playerCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            // First, we check if the player is not touching the ground. If they are, we will return, ignoring the condition below
            return;
        }
        if (value.isPressed && groundChecker)
        {
            Debug.Log(groundChecker);
            playerRB.velocity += new Vector2(0f, playerJumpSpeed);

        }
    }

    void IsJumping()
    {
        Collider2D groundChecker = Physics2D.OverlapCircle(groundCheck.position, 0.1f, LayerMask.GetMask("Ground"));
        if (!groundChecker)
        {
            playerAnimator.SetBool("isJumping", true);
        }
        else
        {
            playerAnimator.SetBool("isJumping", false);
        }
    }

    void takeDamage(float damage)
    {
        playerHealth -= damage;
        if(playerHealth <= 0)
        {
            FindObjectOfType<GameManager>().PlayerDeath();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        GameObject collisionObject = other.gameObject;

        if(collisionObject.tag == "Enemy" && !invincibility)
        {
            takeDamage(10f);
            playerRB.velocity = hurtSpeed;
            StartCoroutine(HurtTime(hurtTime));

        }
    }

    private IEnumerator HurtTime(float waitTime)
    {
        //Note: See what happens if you get hit by Two Enemies -> Invincibility on so its okay
        playerHurt = true;
        invincibility = true;
        playerAnimator.SetBool("isHurt", playerHurt);
        yield return new WaitForSecondsRealtime(waitTime);
        playerRB.velocity = new Vector2(0f, 0f);
        yield return new WaitForSecondsRealtime(waitTime);
        invincibility = false;
        playerHurt = false;
        playerAnimator.SetBool("isHurt", playerHurt);
    }
}
