using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Slime", menuName = "Slime")]
public class EnemySlime : ScriptableObject
{
    public float health;
    public float walkSpeed;
    public float damage;
}
