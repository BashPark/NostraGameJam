using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ProgressManager : MonoBehaviour
{
    [SerializeField] private Image jobProgressBar;
    public float maxJobProgress;
    [SerializeField] private TextMeshProUGUI jobProgressText;

    public float currentJobProgress;

    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private JobSpawnManager jobSpawnManager;

    void Start()
    {
        inventoryManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>();
        jobSpawnManager = GameObject.FindGameObjectWithTag("JobSpawnManager").GetComponent<JobSpawnManager>();

        currentJobProgress = 3;
        updateProgress();

    }


    // Update is called once per frame
    void Update()
    {
     
    }

    public void decreaseProgress() {
        if (currentJobProgress > 0)
        {
            currentJobProgress -= 1;
            updateProgress();
            checkDone();
        }

    }

    public void increaseProgress(float increaseAmt)
    {
        if (currentJobProgress < maxJobProgress)
        {
            currentJobProgress += increaseAmt;
            updateProgress();

        }

    }

    private void updateProgress()
    {
        // Clamp Progress 0-max
        currentJobProgress = Mathf.Clamp(currentJobProgress, 0, maxJobProgress);

        // Set Bar
        jobProgressBar.fillAmount = currentJobProgress / maxJobProgress;

        // Set text
        jobProgressText.text = currentJobProgress.ToString() + " / " + maxJobProgress.ToString();

    }

    private void checkDone()
    {
        // Check Done
        if (currentJobProgress <= 0)
        {
            // Handle Done
            jobSpawnManager.HandleObjectCollected(gameObject);

            Destroy(gameObject);

            GameManager.instance.deleteCurrentTask();

        }

    }

}
