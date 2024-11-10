using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressDetection : MonoBehaviour
{
    [SerializeField] private ProgressManager progressManager;
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private HungerManager hungerManager;

    private void Start()
    {
        inventoryManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>();
        hungerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<HungerManager>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PlayerHand") || other.gameObject.CompareTag("Pickaxe") || other.gameObject.CompareTag("Club"))
        {
            // Variable to check progress
            float originalJobProgress = progressManager.currentJobProgress;

            // Decrease Progress
            progressManager.decreaseProgress(1f);

            if (gameObject.CompareTag("Banana") || gameObject.CompareTag("Berries") || gameObject.CompareTag("Farm"))
            {
                // Check if progress has changed
                if (originalJobProgress != progressManager.currentJobProgress)
                {
                    hungerManager.increaseHunger(1f);

                }
            }
            else if (gameObject.CompareTag("StickStone") || gameObject.CompareTag("Trees") || gameObject.CompareTag("Mine"))
            {
                // Check if progress has changed
                if (originalJobProgress != progressManager.currentJobProgress)
                {
                    inventoryManager.addInventory(1f);

                }
            }
            

           
            

        }
    }
}
