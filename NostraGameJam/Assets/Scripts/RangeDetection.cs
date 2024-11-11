using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RangeDetection : MonoBehaviour
{

    [SerializeField] private Animator animator;

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

            // Anims
            animator.SetBool("JobInteract", true);
            
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.deleteCurrentTask();

            // Anims
            animator.SetBool("JobInteract", false);
        }
    }

    private void OnDestroy()
    {



    }
}
