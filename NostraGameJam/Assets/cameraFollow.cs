using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    [SerializeField] private Camera Maincam;

    private void Update()
    {
        transform.position = Maincam.transform.position;
    }
}
