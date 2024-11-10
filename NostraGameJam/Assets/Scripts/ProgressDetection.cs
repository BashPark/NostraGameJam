using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressDetection : MonoBehaviour
{
    [SerializeField] private ProgressManager progressManager;
    [SerializeField] private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PlayerHand") || other.gameObject.CompareTag("Pickaxe") || other.gameObject.CompareTag("Club"))
        {
            // Variable to check progress
            float originalJobProgress = progressManager.currentJobProgress;
            
            // Decrease Progress
            progressManager.decreaseProgress(1f);

            // Check if progress has changed
            if (originalJobProgress != progressManager.currentJobProgress )
            {
                inventoryManager.addInventory(1f);

            }

        }
    }
}
