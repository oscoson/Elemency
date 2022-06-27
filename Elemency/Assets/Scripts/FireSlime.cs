using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSlime : MonoBehaviour
{
    public EnemySlime slimeSO;
    public float slimeHealth;
    public float slimeDamage;
    public float slimeWalkSpeed;
    private Rigidbody2D slimeRB;


    private void Start()
    {
        slimeRB = GetComponent<Rigidbody2D>();
        slimeHealth = slimeSO.health;
        slimeDamage = slimeSO.damage;
        slimeWalkSpeed = slimeSO.walkSpeed;
    }

    private void Update()
    {
        slimeRB.velocity = new Vector2(slimeWalkSpeed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Platforms")
        {
            slimeWalkSpeed = -slimeWalkSpeed;
            FlipSprite();
        }
    }

    private void FlipSprite()
    {
        transform.localScale = new Vector2(-(transform.localScale.x), 0.2257035f);
    }
}
