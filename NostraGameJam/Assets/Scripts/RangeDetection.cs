using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RangeDetection : MonoBehaviour
{
   
  
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.assignCurrentTask(gameObject.tag);

            if (other.gameObject.CompareTag("Pickaxe"))
            {
                Debug.Log("Hit with pickaxe");
            }
            
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.deleteCurrentTask();
        }
    }

    private void OnDestroy()
    {
        GameManager.instance.deleteCurrentTask();

    }
}
