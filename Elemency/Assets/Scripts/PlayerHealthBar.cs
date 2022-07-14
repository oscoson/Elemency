using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthBar : MonoBehaviour
{
    private Image healthBar;
    public Image secondaryHealthBar;
    public float currentHealth;
    public float maxHealth;
    [SerializeField] private TextMeshProUGUI text;
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
        text.text = Mathf.Round((currentHealth * 100.0f) * 0.01f).ToString() + "/" + maxHealth.ToString();

    }

    private void FixedUpdate()
    {
        if (secondaryHealthBar.fillAmount > healthBar.fillAmount)
        {
            secondaryHealthBar.fillAmount -= 0.0035f;
        }
    }
}
