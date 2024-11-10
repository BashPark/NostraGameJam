using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShrineManager : MonoBehaviour
{
    [SerializeField] private Image shrineDepotBar;
    [SerializeField] private float maxShrineDepotProgress;
    [SerializeField] private TextMeshProUGUI shrineDepotText;

    public float currentShrineDepotProgress;

    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private SafeZone safeZone;


    void Start()
    {
        currentShrineDepotProgress = 0;
        updateShrineDepotProgress();



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
        }

    }

    public void increaseShrineDepotProgress(float Amt)
    {
        if (currentShrineDepotProgress < maxShrineDepotProgress && inventoryManager.currentInventory > 0)
        {
            currentShrineDepotProgress += Amt;
            updateShrineDepotProgress();
            checkDone();

            safeZone.increaseSafeZoneSize();
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
        if (currentShrineDepotProgress >= 100)
        {
            // Handle Done
            Debug.Log("Level up");

        }

    }

}
