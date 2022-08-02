using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Base Attributes")]
    [SerializeField] private bool playerAlive = true;
    public float maxHealth;
    public float playerHealth;
    [SerializeField] private float playerDamage;
    [SerializeField] private float playerMoveSpeed;
    [SerializeField] private float playerJumpSpeed;
    private PlayerInput playerInputController;

    [Header("Magic")]
    public float magicPower = 40f;
    [SerializeField] private GameObject[] elementalBalls = new GameObject[4];
    public int currentMagicIndex = 0;
    public int potencyAmount;

    [Header("Damage Taken Times/Statuses")]
    [SerializeField] private bool playerHurt = false;
    [SerializeField] private bool invincibility = false;
    [SerializeField] private Vector2 hurtSpeed = new Vector2(2.5f, 2.5f);
    [SerializeField] private float hurtTime = 1f;
    [SerializeField] private float burnDamage = 0.1f;

    [Header("Transform Checks/Spawns")]
    public Transform groundCheck;
    public Transform magicSpawn;
    private Vector2 moveInput;
    private Rigidbody2D playerRB;
    private CapsuleCollider2D playerCollider;
    private Animator playerAnimator;

    [Header("Misc")]
    private MagicIconSwitch iconSwitch;


    private void Awake()
    {
        playerInputController = GetComponent<PlayerInput>();
    }
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        maxHealth = playerHealth;
        iconSwitch = FindObjectOfType<MagicIconSwitch>();

    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (!playerAlive || playerHurt)
        {
            return;
        }
        if (playerAnimator.GetBool("isBurning"))
        {
            takeDamage(burnDamage);
        }
        Run();
        FlipSprite();
        IsJumping();
    }
    void Run()
    {
        playerRB.velocity = new Vector2(moveInput.x * playerMoveSpeed, playerRB.velocity.y);

        bool playerHasHorizontalSpeed = Mathf.Abs(playerRB.velocity.x) > Mathf.Epsilon;
        if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
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

    void OnFire(InputValue value)
    {
        if (!playerAlive || playerHurt)
        {
            return;
        }

        Instantiate(elementalBalls[currentMagicIndex], magicSpawn.position, transform.rotation);

    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!playerAlive || playerHurt)
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
            playerRB.velocity += new Vector2(0f, playerJumpSpeed);

        }
    }

    void OnChangeMagic(InputValue index)
    {

        if (Keyboard.current[Key.C].isPressed)
        {
            Debug.Log("C");
            if (currentMagicIndex == 0)
            {
                currentMagicIndex = elementalBalls.Length - 1;
            }
            else
            {
                currentMagicIndex -= 1;
            }
        }
        else if (Keyboard.current[Key.V].isPressed)
        {
            Debug.Log("V");
            if (currentMagicIndex == elementalBalls.Length - 1)
            {
                currentMagicIndex = 0;
            }
            else
            {
                currentMagicIndex += 1;
            }
        }
        iconSwitch.IconChange(currentMagicIndex);
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
        if (playerHealth <= 0)
        {
            FindObjectOfType<GameManager>().PlayerDeath();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject collisionObject = other.gameObject;

        if (collisionObject.tag == "Hazard")
        {
            playerAnimator.SetBool("isBurning", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        GameObject collisionObject = other.gameObject;
        if (collisionObject.tag == "Hazard")
        {
            playerAnimator.SetBool("isBurning", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        GameObject collisionObject = other.gameObject;

        if (collisionObject.tag == "Enemy" && !invincibility)
        {
            takeDamage(10f);
            playerRB.velocity = hurtSpeed;
            StartCoroutine(HurtTime(hurtTime));

        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        GameObject collisionObject = other.gameObject;
        if (collisionObject.tag == "Enemy" && !invincibility)
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
        playerHurt = false;
        playerAnimator.SetBool("isHurt", playerHurt);
        yield return new WaitForSecondsRealtime(waitTime);
        invincibility = false;
    }
}
