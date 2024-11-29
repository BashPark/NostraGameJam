using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using Unity.Burst.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody Rigidbody;
    [SerializeField] private VariableJoystick Joystick;
    [SerializeField] private float moveSpeed;

    [SerializeField] private Animator MonkeyAnimator;
    [SerializeField] private Animator CavemanAnimator;
    [SerializeField] private Animator FarmerAnimator;
    [SerializeField] private LayerMask jobMask;

    public RaycastHit hit;
    public bool isActioning = false;

    [SerializeField] private bool stopActioningCountdown = false;
    private Coroutine stopActionCoroutine;
    private float stateInfo;

    public bool canMove = false;

    [SerializeField] private EnemyHealthManager enemyHealthManager;
    [SerializeField] private EnemyAiMovement enemyAiMovement;

    public float enemyLastHit;

    private void Start()
    {
        Joystick = GameObject.FindWithTag("Joystick").GetComponent<VariableJoystick>();
        canMove = true;

        enemyLastHit = Time.time;
    }

    private void FixedUpdate()
    {


        if (canMove)
        {
            // Player Movement
            Rigidbody.velocity = new Vector3(Joystick.Horizontal * moveSpeed, Rigidbody.velocity.y, Joystick.Vertical * moveSpeed);



            if (Joystick.Horizontal > 0.01 || Joystick.Vertical > 0.01 || Joystick.Horizontal < -0.01 || Joystick.Vertical < -0.01)
            {
                // Character Turn
                transform.rotation = Quaternion.LookRotation(Rigidbody.velocity);




                // Turn Running Animation on

                // For Monkey
                if (MonkeyAnimator.gameObject.activeInHierarchy)
                {
                    MonkeyAnimator.SetBool("isRunning", true);
                    stopActioningCountdown = false;

                }


                // For Caveman
                if (CavemanAnimator.gameObject.activeInHierarchy)
                {
                    CavemanAnimator.SetBool("isRunning", true);
                    stopActioningCountdown = false;

                }

                // For Farmer
                if (FarmerAnimator.gameObject.activeInHierarchy)
                {
                    FarmerAnimator.SetBool("isRunning", true);
                    stopActioningCountdown = false;

                }


            }
            else
            {
                // Turn Running Animation off

                // For Monkey
                if (MonkeyAnimator.gameObject.activeInHierarchy)
                {
                    MonkeyAnimator.SetBool("isRunning", false);
                }


                // For Caveman
                if (CavemanAnimator.gameObject.activeInHierarchy)
                {
                    CavemanAnimator.SetBool("isRunning", false);

                }

                // For Farmer
                if (FarmerAnimator.gameObject.activeInHierarchy)
                {
                    FarmerAnimator.SetBool("isRunning", false);
                }

            }
        }


    }

    public void playerAction()
    {
        // Raycast detection for Jobs,Food, Enemy
        
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 5f, jobMask);
    
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 5f, Color.yellow);

        // Player attack enemy
        if (hit.collider != null )
        {
            print(hit.collider.tag);

            if (hit.transform.CompareTag("Enemy"))
            {
                enemyHealthManager = hit.transform.GetComponent<EnemyHealthManager>();
                enemyAiMovement = hit.transform.GetComponent<EnemyAiMovement>();

                enemyHealthManager.damageHealth(1f);
                enemyAiMovement.ApplyKnockback();

                enemyLastHit = Time.time;
            }
        }

        // Start action if countdown is not going on
        if (!stopActioningCountdown)
        {
            isActioning = true;

            // Stop any existing coroutine to avoid duplicates
            if (stopActionCoroutine != null)
            {
                StopCoroutine(stopActionCoroutine);
            }

            // Start the coroutine and keep a reference to it
            stopActionCoroutine = StartCoroutine(handleStopAction());

        }

       



        // For Monkey
        if (MonkeyAnimator.gameObject.activeInHierarchy)
        {
            MonkeyAnimator.SetTrigger("Swing");

        }

        // For Caveman ( Swing )
        if (CavemanAnimator.gameObject.activeInHierarchy)
        {
            CavemanAnimator.SetTrigger("Swing");

        }

        // For Farmer
        if (FarmerAnimator.gameObject.activeInHierarchy)
        {
            FarmerAnimator.SetTrigger("Swing");

        }

    }

    private IEnumerator handleStopAction()
    {
        stopActioningCountdown = true;
        canMove = false;

        // Wait for the Animator to update its state
        //yield return null;

        // Get the animation duration
        float animationDuration = GetAnimationDuration();

        // Wait for the animation to finish
        yield return new WaitForSeconds(animationDuration);

        // Stop the actionCountdown
        stopActioningCountdown = false;
        canMove = true;

        // Clear the coroutine reference after it finishes
        stopActionCoroutine = null;

    }

    private float GetAnimationDuration()
    {
        // Fetch the current animation state after the Animator has updated

        // For Monkey
        if (MonkeyAnimator.gameObject.activeInHierarchy)
        {
            stateInfo = 1.2f;

        }

        // For Caveman
        if (CavemanAnimator.gameObject.activeInHierarchy)
        {
            stateInfo = 2.1f;


        }

        // For Farmer
        if (FarmerAnimator.gameObject.activeInHierarchy)
        {
            stateInfo = 1.9f;

        }

        // Return the current animation clip's length
        return stateInfo > 0 ? stateInfo : 1.0f; // Fallback to 1 second

    }

}
