using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DepotDetection : MonoBehaviour
{
    public bool inDepotZone;
    [SerializeField] private InventoryManager inventoryManager;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inDepotZone = true;
            StartCoroutine(depotInventory());
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inDepotZone = false;

        }

    }

    IEnumerator depotInventory()
    {
        while(inDepotZone)
        {
            inventoryManager.removeInventory(1f);
            yield return new WaitForSeconds(1f);
        }

    }

}
