using UnityEngine;

public class MovementTutorial : MonoBehaviour
{
    public GameObject winPortal;

    private TMPro.TextMeshProUGUI _textBox;

    
    
    private void Start()
    {
        _textBox = GameObject.Find("ScoreObjective").GetComponent<TMPro.TextMeshProUGUI>();
        _textBox.color = Color.white;
    }

    private void Update()
    {
        if (ScoreTracking.CheckScore())
        {
            _textBox.text = "Good job!!! Now bounce the ball into the Green Win Portal to complete the level!";
        }
    }
}
