using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSlime : MonoBehaviour
{
    public EnemySlime slimeSO;
    public float slimeHealth;
    public float slimeDamage;
    public float slimeWalkSpeed;


    private void Start()
    {
        slimeHealth = slimeSO.health;
        slimeDamage = slimeSO.damage;
        slimeWalkSpeed = slimeSO.walkSpeed;
    }
}
