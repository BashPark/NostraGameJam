using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    [SerializeField] private HealthManager healthManager;
    private bool inSafeZone;

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        Debug.Log("Player has entered safe zone");
    //    }

    //}


    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        Debug.Log("Player has exited safe zone");
    //    }

    //}

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
