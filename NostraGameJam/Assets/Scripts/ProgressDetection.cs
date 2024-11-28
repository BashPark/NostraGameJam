using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class ProgressDetection : MonoBehaviour
{
    [SerializeField] private ProgressManager progressManager;
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private HungerManager hungerManager;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Animator animator;
    [SerializeField] private SphereCollider sCollider;


    // Variable to check progress
    private float originalJobProgress;

    private void Start()
    {
        inventoryManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>();
        hungerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<HungerManager>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        progressManager = GetComponent<ProgressManager>();
        animator = GetComponent<Animator>();
        sCollider = GetComponent<SphereCollider>();

        originalJobProgress = progressManager.currentJobProgress;
    }

    private void FixedUpdate()
    {
        if (playerMovement.hit.collider == sCollider && playerMovement.isActioning)
        {

            if(playerMovement.hit.transform.CompareTag("Banana") || playerMovement.hit.transform.CompareTag("Berries") || playerMovement.hit.transform.CompareTag("Farm"))
            {
                if (hungerManager.currentHunger != hungerManager.maxHunger)
                {
                    

                    // Check if progress has changed
                    //if (originalJobProgress != progressManager.currentJobProgress)
                    //{
                        hungerManager.increaseHunger(1f);

                        if (progressManager.currentJobProgress == 3)
                        {
                                animator.SetTrigger("Interact1");
                        }
                        else if (progressManager.currentJobProgress == 2)
                        {
                            animator.SetTrigger("Interact2");
                        }
                        else if (progressManager.currentJobProgress == 1)
                        {
                            animator.SetTrigger("Interact3");
                        }

                        // Play eat audio
                        AudioManager.instance.PlayClip(AudioManager.instance.eatAudio, true, 0.5f);

                    // Decrease Hunger
                    progressManager.Invoke("decreaseProgress", 0.7f);

                    //}

                }
            }
            else if (playerMovement.hit.transform.CompareTag("StickStone") || playerMovement.hit.transform.CompareTag("Trees") || playerMovement.hit.transform.CompareTag("Mine"))
            {
                if (inventoryManager.currentInventory != inventoryManager.maxInventory)
                {
                   

                    // Check if progress has changed
                    //if (originalJobProgress != progressManager.currentJobProgress)
                    //{
                        inventoryManager.addInventory(1f);

                        if (progressManager.currentJobProgress == 3)
                        {
                            animator.SetTrigger("Interact1");
                        }
                        else if (progressManager.currentJobProgress == 2)
                        {
                            animator.SetTrigger("Interact2");
                        }
                        else if (progressManager.currentJobProgress == 1)
                        {
                            animator.SetTrigger("Interact3");
                        }

                        // Play job audio ( needs change )
                        if (gameObject.CompareTag("StickStone"))
                        {
                            AudioManager.instance.PlayClip(AudioManager.instance.pickupAudio, true, 0.5f);
                        }
                        else if (gameObject.CompareTag("Trees"))
                        {
                            AudioManager.instance.PlayClip(AudioManager.instance.woodcutAudio, true, 0.5f);
                        }
                        else if (gameObject.CompareTag("Mine"))
                        {
                            AudioManager.instance.PlayClip(AudioManager.instance.miningAudio, true, 0.5f);
                        }

                    // Decrease Progress
                    progressManager.Invoke("decreaseProgress", 0.7f);

                    //}
                }
            }

            playerMovement.isActioning = false;
        }
        
    }
        //private void OnTriggerEnter(Collider other)
        //{
        //    if (other.gameObject.CompareTag("PlayerHand") || other.gameObject.CompareTag("Pickaxe") || other.gameObject.CompareTag("Club"))
        //    {

        //        if ((gameObject.CompareTag("Banana") || gameObject.CompareTag("Berries") || gameObject.CompareTag("Farm")) && hungerManager.currentHunger != hungerManager.maxHunger)
        //        {
        //            // Decrease Hunger
        //            progressManager.decreaseProgress(1f);

        //            // Check if progress has changed
        //            if (originalJobProgress != progressManager.currentJobProgress)
        //            {
        //                hungerManager.increaseHunger(1f);

        //                if (progressManager.currentJobProgress == 2)
        //                {
        //                    animator.SetTrigger("Interact1");
        //                }
        //                else if (progressManager.currentJobProgress == 1)
        //                {
        //                    animator.SetTrigger("Interact2");
        //                }
        //                else if (progressManager.currentJobProgress == 0)
        //                {
        //                    animator.SetTrigger("Interact3");
        //                }

        //                AudioManager.instance.PlayClip(AudioManager.instance.eatAudio, true, 0.5f);


        //            }

        //        }
        //        else if (gameObject.CompareTag("StickStone") || gameObject.CompareTag("Trees") || gameObject.CompareTag("Mine"))
        //        {
        //            // Decrease Progress
        //            progressManager.decreaseProgress(1f);

        //            // Check if progress has changed
        //            if (originalJobProgress != progressManager.currentJobProgress)
        //            {
        //                inventoryManager.addInventory(1f);

        //                if (progressManager.currentJobProgress == 2)
        //                {
        //                    animator.SetTrigger("Interact1");
        //                }
        //                else if (progressManager.currentJobProgress == 1)
        //                {
        //                    animator.SetTrigger("Interact2");
        //                }
        //                else if (progressManager.currentJobProgress == 0)
        //                {
        //                    animator.SetTrigger("Interact3");
        //                }

        //                if (gameObject.CompareTag("StickStone"))
        //                {
        //                    AudioManager.instance.PlayClip(AudioManager.instance.pickupAudio, true, 0.5f);
        //                }
        //                else if (gameObject.CompareTag("Trees"))
        //                {
        //                    AudioManager.instance.PlayClip(AudioManager.instance.woodcutAudio, true, 0.5f);
        //                }
        //                else if (gameObject.CompareTag("Mine"))
        //                {
        //                    AudioManager.instance.PlayClip(AudioManager.instance.miningAudio, true, 0.5f);
        //                }

        //            }
        //        }





        //    }
        //}
    }
