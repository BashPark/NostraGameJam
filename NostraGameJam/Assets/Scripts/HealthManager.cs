using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private float maxHealth;
     private float currentHealth;
    public Image progressBar;
    void Start()
    {
        currentHealth = maxHealth;
     
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void damageHealth(float damage)
    {
        currentHealth -= damage;
        progressBar.fillAmount=currentHealth/maxHealth;
    }
    public void healHealth(float heal) {
        currentHealth += heal;
        progressBar.fillAmount = currentHealth / maxHealth;
    }
}
