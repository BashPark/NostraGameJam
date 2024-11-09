using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ProgressManager : MonoBehaviour
{
    [SerializeField] private float maxProgress;
    private float currentProgress;
    [SerializeField] Image AssetProgressBar;
    void Start()
    {
        currentProgress = maxProgress;
    }

    // Update is called once per frame
    void Update()
    {
        increaseProgress(0.1f);
    }
    public void decreaseProgress(float decreaseAmt) { 
        currentProgress-=decreaseAmt;
       AssetProgressBar.fillAmount = currentProgress / maxProgress;
    }
    public void increaseProgress(float increaseAmt)
    {
        currentProgress+=increaseAmt;
        AssetProgressBar.fillAmount = currentProgress / maxProgress;
    }

}
