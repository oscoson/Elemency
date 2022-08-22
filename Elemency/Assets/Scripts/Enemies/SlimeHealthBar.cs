using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeHealthBar : MonoBehaviour
{
    public Image healthBar;
    public Image secondaryHealthBar;
    public Slime slime;
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private Transform healthBarPosition; 
    [SerializeField] private Vector3 offset;


    private void Start()
    {
        currentHealth = slime.slimeHealth;
        maxHealth = currentHealth;
    }

    private void Update()
    {
        currentHealth = slime.slimeHealth;
        healthBar.fillAmount = currentHealth / maxHealth;
        healthBarPosition.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);

    }

    private void FixedUpdate()
    {
        if (secondaryHealthBar.fillAmount > healthBar.fillAmount)
        {
            secondaryHealthBar.fillAmount -= 0.01f;
        }
    }
}
