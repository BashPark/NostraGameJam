using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public string currentTask=null;
    public GameObject attackPanel;

    private void Awake()
    {
        
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void assignCurrentTask(string task)
    {
        currentTask = task;
        attackPanel.SetActive(true);
      
    }
    public void deleteCurrentTask()
    {
        currentTask=null;
        //attackPanel.SetActive(false);
       
    }
    void Start()
    {
        // Limit framerate to cinematic 24fps.
        QualitySettings.vSyncCount = 0; // Set vSyncCount to 0 so that using .targetFrameRate is enabled.
        Application.targetFrameRate = 60;

    }

   
    void Update()
    {
        
    }
}
