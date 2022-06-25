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


    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Run();
    }
    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * playerMoveSpeed, playerRB.velocity.y);
        playerRB.velocity = playerVelocity;
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
