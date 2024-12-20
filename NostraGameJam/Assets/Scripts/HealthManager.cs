using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private float maxHealth;
    [SerializeField] private TextMeshProUGUI healthText;

    private float currentHealth;

    [SerializeField] private Animator animator;

    public float lastDamagedTime;


    void Start()
    {
        currentHealth = maxHealth;
        updateHealth();

        // Start anim
        if (currentHealth == maxHealth)
        {
            animator.SetBool("healthMax", true);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            damageHealth(1f);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void damageHealth(float damage)
    {
        if (currentHealth > 0)
        {
            lastDamagedTime = Time.time; // Track the time of the damage event

            // Deduct health
            currentHealth -= damage;

            // Display health
            updateHealth();

            // Check Death
            checkDeath();

            // Anims
            if (currentHealth < maxHealth)
            {
                animator.SetBool("healthMax", false);
            }

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

            // Anims
            if (currentHealth == maxHealth)
            {
                animator.SetBool("healthMax", true);
            }
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
            AudioManager.instance.PlayClip(AudioManager.instance.deathAudio, true, 0.5f);
            // Handle death
            Destroy(gameObject);

            SceneManager.LoadScene("0.7Death");

        }


    }
}
