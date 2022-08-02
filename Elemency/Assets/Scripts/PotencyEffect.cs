using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotencyEffect : MonoBehaviour
{
    [SerializeField] private float burstTime;
    private Player player;
    private ParticleSystem potencyParticles;

    // Potency Particle Effect Functionality Outline
    // Upon Enemy death, instantiate the particle system
    // Allow Particle Effect to burst out of dead enemy for a certain amount of time -> Coroutine
    // Individual particles then get absorbed by player
    // They get absorbed through constantly heading towards the player location through an update loop
    // Destroying after colliding with player or after certain amount of time


    private void Start()
    {
        player = FindObjectOfType<Player>();
        potencyParticles = GetComponent<ParticleSystem>();
    }

    private void FixedUpdate()
    {

    }
}
