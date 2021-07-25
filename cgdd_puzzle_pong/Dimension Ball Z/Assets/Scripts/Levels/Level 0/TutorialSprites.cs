using UnityEngine;

public class TutorialSprites : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject mouseAndKeyboard;
    public GameObject KeyboardOnly;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("Input"))
        {
            if (PlayerPrefs.GetInt("Input") == 1)
            {
                KeyboardOnly.SetActive(false);
            }
            else
            {
                mouseAndKeyboard.SetActive(false);
            }
        }
        else
        {
            KeyboardOnly.SetActive(false);
        }
    }
}
