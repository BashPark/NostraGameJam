using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopAction : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    public void StopAction()
    {
        playerMovement.isActioning = false;
        //Debug.Log("Animataions Falsed");
    }
}
