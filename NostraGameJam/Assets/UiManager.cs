using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

  public void gameStartButton()
    {
        SceneManager.LoadScene("0.2Tutorial");

    }

    public void startLevelButton()
    {
        SceneManager.LoadScene("1Level");

    }

    public void creditsButton()
    {
        SceneManager.LoadScene("0.5Credits");

    }

    public void exitButton()
    {
        Application.Quit();

    }

    public void pauseButton()
    {
        Time.timeScale = 0.0f;
        pausePanel.SetActive(true);

    }

    public void resumeButton()
    {
        Time.timeScale = 1.0f;
        pausePanel.SetActive(false);

    }

    public void mainMenuButton()
    {
        SceneManager.LoadScene("0.0MainMenu");

    }

    public void tryAgainButton()
    {
        SceneManager.LoadScene("1Level");

    }

}
