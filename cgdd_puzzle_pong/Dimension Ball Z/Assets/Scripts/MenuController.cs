using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [CanBeNull] public GameObject gameName;
    [CanBeNull] public GameObject inputMenu;
    [CanBeNull] public GameObject mainMenu;
    [CanBeNull] public GameObject levelSelectDropdown;
    [CanBeNull] public GameObject bonusLevelDropdown;
    
    [CanBeNull] public TextMeshProUGUI levelInputField;
    private bool _backgroundStarted;
    
    private void Start()
    {
        if(!PlayerPrefs.HasKey("Slowmotion"))
            PlayerPrefs.SetInt("Slowmotion", 1);
        
        //TimeManager.Instance.Resume();
        if (PlayerPrefs.HasKey("Input"))
        {
            if (gameName)
            {
                gameName.gameObject.SetActive(true);
            }
            
            /*if (mainMenu)
            {
                mainMenu.gameObject.SetActive(true);
            }
            if (inputMenu)
            {
                inputMenu.gameObject.SetActive(false);
            }*/
        }

    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BallHell()
    {

        SceneManager.LoadScene("BallHell");
    }

    public void PortalInsanity()
    {
        SceneManager.LoadScene("Portal_Insanity");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void UserInputs(bool mouse)
    {
        if (mouse)
        {
            PlayerPrefs.SetInt("Input", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Input", 0);
        }
    }

    public void SelectLevel()
    {
        int.TryParse(levelInputField.text.Replace("\u200b", ""), out int sceneIndex);
        if (sceneIndex != 0)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }

    private string LevelSelect(string dropDownName)
    {
        var selectedLevel = "";
        switch (dropDownName)
        {
            case "LevelSelect":
                selectedLevel = levelSelectDropdown.GetComponent<TMP_Dropdown>().value.ToString();
                break;
            case "BonusLevel":
                selectedLevel = bonusLevelDropdown.GetComponent<TMP_Dropdown>()
                    .options[bonusLevelDropdown.GetComponent<TMP_Dropdown>().value].text;
                break;
        }

        return selectedLevel;
    }

    public void PlaySelectedLevel(string dropDownName)
    {
        switch (dropDownName)
        {
            case "LevelSelect":
                SceneManager.LoadScene(int.Parse(LevelSelect(dropDownName))+1);
                break;
            case "BonusLevel":
                SceneManager.LoadScene(LevelSelect(dropDownName).Replace(" ", ""));
                break;
            
        }
    }
}

