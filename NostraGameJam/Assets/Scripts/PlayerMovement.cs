using System;
using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        Joystick = GameObject.FindWithTag("Joystick").GetComponent<VariableJoystick>();
    }

    private void FixedUpdate()
    {
        // Player Movement
        Rigidbody.velocity = new Vector3(Joystick.Horizontal * moveSpeed, Rigidbody.velocity.y, Joystick.Vertical * moveSpeed);

        // Character Turn
        if(Joystick.Horizontal != 0 ||  Joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(Rigidbody.velocity);

            // Turn Running Animation on

            // For Monkey
            if(MonkeyAnimator.gameObject.activeInHierarchy)
            {
                MonkeyAnimator.SetBool("isRunning", true);
            }


            // For Caveman
            if (CavemanAnimator.gameObject.activeInHierarchy)
            {
                CavemanAnimator.SetBool("isRunning", true);

            }

            // For Farmer
            if (FarmerAnimator.gameObject.activeInHierarchy)
            {
                FarmerAnimator.SetBool("isRunning", true);

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

    public void playerAction()
    {
        // For Monkey
        if (MonkeyAnimator.gameObject.activeInHierarchy)
        {
            MonkeyAnimator.SetTrigger("Pick");

        }
           
        // For CavemanPick
        if (CavemanAnimator.gameObject.activeInHierarchy)
        {
            if(GameManager.instance.currentTask == "Berries")
            {
                CavemanAnimator.SetTrigger("Pick");

            }
           

        }

        // For CavemanSwing
        if (CavemanAnimator.gameObject.activeInHierarchy)
        {
            if (GameManager.instance.currentTask == "Trees")
            {
                CavemanAnimator.SetTrigger("Swing");

            }
                

        }

        // For Farmer
        if (FarmerAnimator.gameObject.activeInHierarchy)
        {
            FarmerAnimator.SetTrigger("Swing");

        }
           

    }
   
  

}
