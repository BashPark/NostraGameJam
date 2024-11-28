using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShrineManager : MonoBehaviour
{
    [SerializeField] private Image shrineDepotBar;
    public float maxShrineDepotProgress;
    [SerializeField] private TextMeshProUGUI shrineDepotText;

    public float currentShrineDepotProgress;

    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private SafeZone safeZone;
    [SerializeField] private LevelPortal levelPortal;

    [SerializeField] private Animator animator;

    void Start()
    {
        inventoryManager = GameObject.FindWithTag("Player").GetComponent<InventoryManager>();
        safeZone = GameObject.FindWithTag("Safezone").GetComponent<SafeZone>();
        levelPortal = GameObject.FindWithTag("Portal").GetComponent<LevelPortal>();

        currentShrineDepotProgress = 0;
        updateShrineDepotProgress();

        if (currentShrineDepotProgress == 0)
        {
            animator.SetBool("minProg", true);
        }

    }

    // Update is called once per frame
    void Update()
    {


    }

    public void decreaseShrineDepotProgress(float Amt)
    {
        if (currentShrineDepotProgress > 0)
        {
            currentShrineDepotProgress -= Amt;
            updateShrineDepotProgress();

            if(currentShrineDepotProgress ==0)
            {
                animator.SetBool("minProg", true);
            }

        }

    }

    public void increaseShrineDepotProgress(float Amt)
    {
        if (currentShrineDepotProgress < maxShrineDepotProgress && inventoryManager.currentInventory > 0)
        {
            currentShrineDepotProgress += Amt;

            inventoryManager.removeInventory(1f);
            
            updateShrineDepotProgress();
            //checkDone();

            safeZone.increaseSafeZoneSize();
            levelPortal.checkAndActivatePortal();

            if (currentShrineDepotProgress != 0)
            {
                animator.SetBool("minProg", false);
            }

        }

    }

    private void updateShrineDepotProgress()
    {
        // Clamp Progress 0-max
        currentShrineDepotProgress = Mathf.Clamp(currentShrineDepotProgress, 0, maxShrineDepotProgress);

        // Set Bar
        shrineDepotBar.fillAmount = currentShrineDepotProgress / maxShrineDepotProgress;

        // Set text
        shrineDepotText.text = currentShrineDepotProgress.ToString() + " / " + maxShrineDepotProgress.ToString();

    }

    private void checkDone()
    {
        // Check Done
        if (currentShrineDepotProgress >= maxShrineDepotProgress)
        {
            // Handle Done
            Debug.Log("Level up");

        }

    }

}
