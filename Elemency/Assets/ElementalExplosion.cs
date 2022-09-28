using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalExplosion : MonoBehaviour
{
    private ParticleSystem particles;
    private Player player;
    private Renderer pr;

    private void Start()
    {
        particles = GetComponent<ParticleSystem>();
        player = FindObjectOfType<Player>();
        pr = GetComponent<ParticleSystem>().GetComponent<Renderer>();
        SetElement();
    }


    private void SetElement()
    {
        var trail = particles.trails;
        switch(player.currentMagicIndex)
        {
            case 0:
                particles.tag = "FireMagic";
                pr.material.color = new Color(251, 242, 54);
                trail.colorOverTrail = new Color(237, 47, 44);
                break;
            
            case 1:
                particles.tag = "WaterMagic";
                pr.material.color = new Color(129, 212, 250);
                trail.colorOverTrail = new Color(0, 153, 251);
                break;
            default:
                break;
        }
    }




}
