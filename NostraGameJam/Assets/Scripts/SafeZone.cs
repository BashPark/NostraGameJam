using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    [SerializeField] private HealthManager healthManager;

    [SerializeField] private float scaleMultiplier = 1f; // Multiplier for scaling (e.g., 1.1 will increase the scale by 10%)
    [SerializeField] private float increaseSpeed = 1f; // Speed of the scaling animation

    private Vector3 originalScale;
    private Vector3 targetScale;

    private bool inSafeZone;


    private void Start()
    {
        // Store the original scale as the baseline
        originalScale = transform.localScale;

        // Initialize currentScale and targetScale to the original scale
        targetScale = originalScale;


    }

    private void Update()
    {
        // Smoothly interpolate towards the target scale, affecting only x and z
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, increaseSpeed * Time.deltaTime);

    }

    public void increaseSafeZoneSize()
    {

        // Multiply the current scale by the scaleMultiplier to make the safe zone grow exponentially
        targetScale = new Vector3(targetScale.x + scaleMultiplier, originalScale.y, targetScale.z + scaleMultiplier);


        //transform.localScale = targetScale;
        // Smoothly interpolate towards the target scale, affecting only x and z
        //transform.localScale = Vector3.Lerp(transform.localScale, targetScale, increaseSpeed * Time.deltaTime);

        // Multiply the current scale by the scaleMultiplier to make the safe zone grow
        //targetScale = new Vector3(targetScale.x + scaleMultiplier, originalScale.y, targetScale.z + scaleMultiplier);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Bool for coroutine while loop
            inSafeZone = true;

            StartCoroutine(takeSafeZoneHeal());
            //Debug.Log("Player has entered safe zone");
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Bool for coroutine while loop
            inSafeZone = false;

            StartCoroutine(takeDangerZoneDamage());
            //Debug.Log("Player has exited safe zone");
        }
    }

    IEnumerator takeDangerZoneDamage()
    {
        while (!inSafeZone)
        {
            healthManager.damageHealth(1f);
            yield return new WaitForSeconds(1f);

        }

    }

    IEnumerator takeSafeZoneHeal()
    {
        while (inSafeZone)
        {
            yield return new WaitForSeconds(1f);

            if (inSafeZone)
            {
                healthManager.healHealth(1f);

            }

            //healthManager.healHealth(1f);
            //yield return new WaitForSeconds(1f);

        }

    }

}
