using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalBallMagic : MonoBehaviour
{
    [Header("Attributes")]
    public ElementalBall elementalBallSO;
    [SerializeField] private int destroyTime = 5;
    private Rigidbody2D magicRB;
    private Player player;
    private float xSpeed;
    void Start()
    {
        magicRB = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        xSpeed = player.transform.localScale.x * elementalBallSO.speed;
        transform.localScale = new Vector2(player.GetComponent<Transform>().localScale.x, 1f);
        StartCoroutine(DestructionTime(destroyTime));
    }


    void Update()
    {
        magicRB.velocity = new Vector2(xSpeed, 0f);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }

    private IEnumerator DestructionTime(int waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        Destroy(gameObject);
    }
}
