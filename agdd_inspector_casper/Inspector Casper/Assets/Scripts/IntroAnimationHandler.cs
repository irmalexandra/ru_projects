using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroAnimationHandler : MonoBehaviour
{
    public void LoadMainScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainLevel");        
    }

    public void LoadCredits()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Credits");
    }
    
    public void FadeText()
    {
        var textList = GetComponentsInChildren<Text>();
        foreach (var text in textList)
        {
            StartCoroutine(FadeTextToFullAlpha(5f, text));
        }
    }
    
    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
