using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    private Image healthBar;
    public Image secondaryHealthBar;
    public float currentHealth;
    public float maxHealth;
    private Player player;

    private void Start()
    {
        healthBar = GetComponent<Image>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        currentHealth = player.playerHealth;
        maxHealth = player.maxHealth;
        healthBar.fillAmount = currentHealth / maxHealth;

    }

    private void FixedUpdate()
    {
        if (secondaryHealthBar.fillAmount > healthBar.fillAmount)
        {
            secondaryHealthBar.fillAmount -= 0.0025f;
        }
    }
}
