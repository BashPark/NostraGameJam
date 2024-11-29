using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    [SerializeField] private HealthManager healthManager;
    [SerializeField] private HungerManager hungerManager;


    [SerializeField] private float scaleMultiplier = 1f; // Multiplier for scaling (e.g., 1.1 will increase the scale by 10%)
    [SerializeField] private float increaseSpeed = 1f; // Speed of the scaling animation

    private Vector3 originalScale;
    private Vector3 targetScale;

    private bool inSafeZone;
    //private bool istakingSafeZoneHeal = false;

    //private Coroutine takeHealCoroutine;
    public float lastSafeZoneHealedTime;
    public float lastSafeZoneDamagedTime;


    private void Start()
    {
        healthManager = GameObject.FindWithTag("Player").GetComponent<HealthManager>();
        hungerManager = GameObject.FindWithTag("Player").GetComponent<HungerManager>();

        // Store the original scale as the baseline
        originalScale = transform.localScale;

        // Initialize currentScale and targetScale to the original scale
        targetScale = originalScale;

        lastSafeZoneHealedTime = Time.time;
        lastSafeZoneDamagedTime = Time.time;

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

    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            inSafeZone = true;
            Debug.Log("Entered safe zone");
        }

    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inSafeZone = false;
            Debug.Log("Exited safe zone");
        }
    }

    private void FixedUpdate()
    {
        // Heal or Damage Player
        if (inSafeZone)
        {
            TakeDangerZoneHeal();
        }
        else if (!inSafeZone)
        {
            TakeSafeZoneDamage();
        }

    }

    private void TakeDangerZoneHeal()
    {
        // Heal if enough time has passed
        if (Time.time >= lastSafeZoneHealedTime + 2f && Time.time >= healthManager.lastDamagedTime + 2f && hungerManager.currentHunger > 0)
        {
            lastSafeZoneHealedTime = Time.time;
            healthManager.healHealth(1f);
        }
    }

    private void TakeSafeZoneDamage()
    {
        // Damage if enough time has passed
        if (Time.time >= lastSafeZoneDamagedTime + 2f)
        {
            lastSafeZoneDamagedTime = Time.time;
            healthManager.damageHealth(1f);
        }
    }


}
