using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool isPause;
    public GameObject pausePanel;
    void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            if(isPause)
                ResumeGame();
            
            else
                PauseGame();

        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        isPause = true;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPause = false;
        pausePanel.SetActive(false);
    }
}
