using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
 
    [SerializeField] private Camera cam;
   

    void Update()
    {
        transform.rotation = cam.transform.rotation;
       
    }
}