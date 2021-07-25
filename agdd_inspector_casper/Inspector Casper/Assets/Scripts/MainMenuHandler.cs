using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public void onPlay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }


    public void onQuit()
    {
        Application.Quit();
    }
}
