using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotencyCollector : MonoBehaviour
{
    private ParticleSystem potency;
    private List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();
    private Transform collector;
    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        potency = GetComponent<ParticleSystem>();
        collector = GameObject.FindGameObjectWithTag("Collector").transform;
        potency.trigger.AddCollider(collector);
    }

    private void OnParticleTrigger()
    {
        int triggeredParticles = potency.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);


        for (int i = 0; i < triggeredParticles; i++)
        {
            player.potencyAmount++;
            ParticleSystem.Particle oneParticle = particles[i];
            oneParticle.remainingLifetime = 0;
            particles[i] = oneParticle;
        }
        potency.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);
    }
}
