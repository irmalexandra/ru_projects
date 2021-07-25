using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuHandler : MonoBehaviour
{
// Start is called before the first frame update    

    public GameObject pauseMenu;
    public GameObject settingsMenu;
    private bool _onScreen = false;
    
private void Update()
{
    if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape) )
    {
        onPauseToggle();
    }
}


public void onPauseToggle()
{
    if (_onScreen)
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        Time.timeScale = 1;
       

    }
    else
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    _onScreen = !_onScreen;
}

public void onExit()
{
        SceneManager.LoadScene(0);
    }
}
