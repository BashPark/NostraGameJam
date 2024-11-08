using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody Rigidbody;
    [SerializeField] private VariableJoystick Joystick;
    [SerializeField] private Animator Animator;
    [SerializeField] private float moveSpeed;

    private void FixedUpdate()
    {
        // Player Movement
        Rigidbody. velocity = new Vector3(Joystick.Horizontal * moveSpeed, Rigidbody.velocity.y, Joystick.Vertical * moveSpeed);

        // Character Turn
        if(Joystick.Horizontal != 0 ||  Joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(Rigidbody.velocity);

            // Turn Running Animation on

        }
        else
        {
            // Turn Running Animation off

        }


    }
   
  

}
