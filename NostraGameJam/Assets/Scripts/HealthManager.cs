using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private float maxHealth;
    [SerializeField] private TextMeshProUGUI healthText;

    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        updateHealth();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void damageHealth(float damage)
    {
        if (currentHealth > 0)
        {
            // Deduct health
            currentHealth -= damage;

            // Display health
            updateHealth();

            // Check Death
            checkDeath();

        }
           
    }


    public void healHealth(float heal)
    {
        if (currentHealth < maxHealth)
        {
            // Add health
            currentHealth += heal;

            // Display health
            updateHealth();

        }
        
    }

    private void updateHealth()
    {
        // Clamp health 0-max
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Set Bar
        healthBar.fillAmount = currentHealth / maxHealth;
        
        // Set text
        healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();

    }


    private void checkDeath()
    {
        // Check death
        if (currentHealth <= 0)
        {
            // Handle death
            Destroy(gameObject);

        }


    }
}
