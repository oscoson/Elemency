using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float playerHealth;
    [SerializeField] private float playerDamage;
    [SerializeField] private float playerMoveSpeed;
    [SerializeField] private float playerJumpSpeed;
    private Vector2 moveInput;
    private Rigidbody2D playerRB;
    private Animator playerAnimator;


    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        Run();
        FlipSprite();
    }
    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * playerMoveSpeed, playerRB.velocity.y);
        playerRB.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(playerRB.velocity.x) > Mathf.Epsilon;
        playerAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
        
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(playerRB.velocity.x) > Mathf.Epsilon;

        if(playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2 (Mathf.Sign(playerRB.velocity.x), 1f);
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if(value.isPressed)
        {
            playerRB.velocity += new Vector2(0f, playerJumpSpeed);
        }
    }
}
