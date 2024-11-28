using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPortal : MonoBehaviour
{
    [SerializeField] private GameObject portalGate;
    [SerializeField] private ShrineManager shrineManager;
    [SerializeField] private BoxCollider gateCollider;


    private void Start()
    {

       shrineManager= GameObject.FindWithTag("Shrine").GetComponent<ShrineManager>();
        portalGate.SetActive(false);
        gateCollider.isTrigger = false;
    }

    private void Update()
    {

    }

    public void checkAndActivatePortal()
    {
        if (shrineManager.currentShrineDepotProgress >= shrineManager.maxShrineDepotProgress)
        {
            portalGate.SetActive(true);
            gateCollider.isTrigger = true;

        }

    }

}
