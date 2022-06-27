using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSlime : MonoBehaviour
{
    [Header("Attributes")]
    public EnemySlime slimeSO;
    public float slimeHealth;
    public float slimeDamage;
    public float slimeWalkSpeed;
    private Rigidbody2D slimeRB;

    [Header("Patrol AI")]
    [SerializeField] private bool mustPatrol;
    [SerializeField] private bool mustTurn;

    public Collider2D wallCollider;
    public Transform groundCheckPos;
    public LayerMask groundLayer;

    private void Start()
    {
        slimeRB = GetComponent<Rigidbody2D>();
        slimeHealth = slimeSO.health;
        slimeDamage = slimeSO.damage;
        slimeWalkSpeed = slimeSO.walkSpeed;
    }

    private void Update()
    {
        if (mustPatrol)
        {
            Patrol();
        }
    }

    private void FixedUpdate()
    {
        if(mustPatrol)
        {
            // If circle is touching ground, dont flip, else flip
            mustTurn = !(Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer));
        }
    }
    private void Patrol()
    {
        if(mustTurn || wallCollider.IsTouchingLayers(groundLayer))
        {
            Flip();
        }
        slimeRB.velocity = new Vector2(slimeWalkSpeed * Time.fixedDeltaTime, slimeRB.velocity.y);
    }

    private void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        slimeWalkSpeed *= -1;
        mustPatrol = true;
    }


}
