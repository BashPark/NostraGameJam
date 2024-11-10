using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HungerManager : MonoBehaviour
{
    [SerializeField] private Image hungerBar;
    public float maxHunger;
    [SerializeField] private TextMeshProUGUI hungerText;

    public float currentHunger;

    [SerializeField] private float hungerDecreaseAmount = 1f; // Amount of hunger to decrease each time
    [SerializeField] private float hungerDecreaseInterval = 5f; // Time in seconds between each hunger decrease

    [SerializeField] private HealthManager healthManager;
    [SerializeField] private Animator animator;

    void Start()
    {
        healthManager = GameObject.FindWithTag("Player").GetComponent<HealthManager>();

        currentHunger = maxHunger;
        updateHunger();

        // Start the automatic hunger decrease coroutine
        StartCoroutine(DecreaseHungerOverTime());

        // Start Anims
        if (currentHunger == maxHunger)
        {
            animator.SetBool("maxHunger", true);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void reduceHunger(float Amt)
    {
        if (currentHunger > 0)
        {
            // Deduct Hunger
            currentHunger -= Amt;

            // Display Hunger
            updateHunger();

            // Check Starve
            if ( currentHunger <= 0 )
            {
                StartCoroutine(handleStarving());

            }

            if (currentHunger != maxHunger)
            {
                animator.SetBool("maxHunger", false);
            }

        }

    }


    public void increaseHunger(float Amt)
    {
        if (currentHunger < maxHunger)
        {
            // Add health
            currentHunger += Amt;

            // Display health
            updateHunger();

            if(currentHunger == maxHunger)
            {
                animator.SetBool("maxHunger", true);
            }

        }

    }

    private void updateHunger()
    {
        // Clamp health 0-max
        currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);

        // Set Bar
        hungerBar.fillAmount = currentHunger / maxHunger;

        // Set text
        hungerText.text = currentHunger.ToString() + " / " + maxHunger.ToString();

    }
    private IEnumerator DecreaseHungerOverTime()
    {
        while (currentHunger > 0)
        {
            yield return new WaitForSeconds(hungerDecreaseInterval);
            reduceHunger(hungerDecreaseAmount);
        }
    }


    private IEnumerator handleStarving()
    {
        // Check death
        while (currentHunger <= 0)
        {
                healthManager.damageHealth(1f);
                yield return new WaitForSeconds(2f);

        }


    }

    

}
