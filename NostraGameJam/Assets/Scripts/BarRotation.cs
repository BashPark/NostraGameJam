using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarRotation : MonoBehaviour
{
   
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
       
    }
}
