using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressDetection : MonoBehaviour
{
    [SerializeField] private ProgressManager progressManager;
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private HungerManager hungerManager;
    [SerializeField] private Animator animator;

    private void Start()
    {
        inventoryManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>();
        hungerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<HungerManager>();
        animator = GetComponent<Animator>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerHand") || other.gameObject.CompareTag("Pickaxe") || other.gameObject.CompareTag("Club"))
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

                    if (progressManager.currentJobProgress == 2)
                    {
                        animator.SetTrigger("Interact1");
                    }
                    else if (progressManager.currentJobProgress == 1)
                    {
                        animator.SetTrigger("Interact2");
                    }
                    else if (progressManager.currentJobProgress == 0)
                    {
                        animator.SetTrigger("Interact3");
                    }

                    AudioManager.instance.PlayClip(AudioManager.instance.eatAudio,true,0.5f);


                }

            }
            else if (gameObject.CompareTag("StickStone") || gameObject.CompareTag("Trees") || gameObject.CompareTag("Mine"))
            {
                // Check if progress has changed
                if (originalJobProgress != progressManager.currentJobProgress)
                {
                    inventoryManager.addInventory(1f);

                    if (progressManager.currentJobProgress == 2)
                    {
                        animator.SetTrigger("Interact1");
                    }
                    else if (progressManager.currentJobProgress == 1)
                    {
                        animator.SetTrigger("Interact2");
                    }
                    else if (progressManager.currentJobProgress == 0)
                    {
                        animator.SetTrigger("Interact3");
                    }

                    if(gameObject.CompareTag("StickStone"))
                    {
                        AudioManager.instance.PlayClip(AudioManager.instance.pickupAudio, true, 0.5f);
                    }
                    else if(gameObject.CompareTag("Trees"))
                    {
                        AudioManager.instance.PlayClip(AudioManager.instance.woodcutAudio, true, 0.5f);
                    }
                    else if(gameObject.CompareTag("Mine"))
                    {
                        AudioManager.instance.PlayClip(AudioManager.instance.miningAudio, true, 0.5f);
                    }

                }
            }





        }
    }
}
