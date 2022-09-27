using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    //Slime Script
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
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask enemyLayer;

    private Player player;

    [Header("Effects")]
    [SerializeField] private ParticleSystem potencyBurst;
    [SerializeField] private ParticleSystem deathEffect;

    private void Start()
    {
        slimeRB = GetComponent<Rigidbody2D>();
        slimeHealth = slimeSO.health;
        slimeDamage = slimeSO.damage;
        slimeWalkSpeed = slimeSO.walkSpeed;
        player = FindObjectOfType<Player>();
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
        if (mustPatrol)
        {
            // If circle is touching ground, dont flip, else flip
            mustTurn = !(Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer));
        }
    }

    private void Patrol()
    {
        if (mustTurn || wallCollider.IsTouchingLayers(groundLayer) || wallCollider.IsTouchingLayers(enemyLayer))
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

    private void takeDamage(float damage)
    {
        slimeHealth -= damage;
        if (slimeHealth <= 0)
        {
            Instantiate(potencyBurst, transform.position, Quaternion.identity);
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        GameObject collisionObject = other.gameObject;
        switch (collisionObject.tag)
        {
            case "FireMagic":
                switch (slimeSO.slimeType)
                {
                    case "FireSlime":
                        takeDamage(player.magicPower * 0.5f);
                        break;

                    case "WaterSlime":
                        takeDamage(player.magicPower * 0.25f);
                        break;
                    case "AirSlime":
                        takeDamage(player.magicPower * 1.5f);
                        break;
                }
                break;

            case "WaterMagic":
                switch (slimeSO.slimeType)
                {
                    case "FireSlime":
                        takeDamage(player.magicPower * 1.5f);
                        break;

                    case "WaterSlime":
                        takeDamage(player.magicPower * 0.5f);
                        break;
                    case "AirSlime":
                        takeDamage(player.magicPower * 0.25f);
                        break;
                }
                break;

            case "AirMagic":
                switch (slimeSO.slimeType)
                {
                    case "FireSlime":
                        takeDamage(player.magicPower * 0.25f);
                        break;

                    case "WaterSlime":
                        takeDamage(player.magicPower * 1.5f);
                        break;
                    case "AirSlime":
                        takeDamage(player.magicPower * 0.5f);
                        break;
                }
                break;
        }


    }

    private void OnParticleCollision(GameObject other)
    {
        GameObject collisionObject = other.gameObject;
        
        switch (collisionObject.tag)
        {
            case "FireMagic":
                switch (slimeSO.slimeType)
                {
                    case "FireSlime":
                        takeDamage(player.magicPower * 0.5f);
                        break;

                    case "WaterSlime":
                        takeDamage(player.magicPower * 0.25f);
                        break;
                    case "AirSlime":
                        takeDamage(player.magicPower * 1.5f);
                        break;
                }
                break;

            case "WaterMagic":
                switch (slimeSO.slimeType)
                {
                    case "FireSlime":
                        takeDamage(player.magicPower * 1.5f);
                        break;

                    case "WaterSlime":
                        takeDamage(player.magicPower * 0.5f);
                        break;
                    case "AirSlime":
                        takeDamage(player.magicPower * 0.25f);
                        break;
                }
                break;

            case "AirMagic":
                switch (slimeSO.slimeType)
                {
                    case "FireSlime":
                        takeDamage(player.magicPower * 0.25f);
                        break;

                    case "WaterSlime":
                        takeDamage(player.magicPower * 1.5f);
                        break;
                    case "AirSlime":
                        takeDamage(player.magicPower * 0.5f);
                        break;
                }
                break;
        }
    }


}
