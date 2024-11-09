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
        //print(currentTask);
    }
    public void deleteCurrentTask()
    {
        currentTask=null;
        attackPanel.SetActive(false);
        //print(currentTask);
    }
    void Start()
    {
    }

   
    void Update()
    {
        
    }
}
