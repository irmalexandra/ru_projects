using UnityEngine;


public class ScoreTracking : MonoBehaviour
{
    private TMPro.TextMeshProUGUI _leftScoreText;
    private TMPro.TextMeshProUGUI _rightScoreText;
    private TMPro.TextMeshProUGUI _scoreObjectiveText;

    private static int _leftScore;
    private static int _rightScore;

    public int sLimit;

    private static int _scoreLimit;

    public GameObject WinPortal;

    private bool _playSound = true;
    private void Start()
    {
        _scoreLimit = sLimit;
        _leftScoreText = GameObject.Find("LeftScore").GetComponent<TMPro.TextMeshProUGUI>();
        _rightScoreText = GameObject.Find("RightScore").GetComponent<TMPro.TextMeshProUGUI>();
        _scoreObjectiveText = GameObject.Find("ScoreObjective").GetComponent<TMPro.TextMeshProUGUI>();
        _scoreObjectiveText.text = $"Increase each sides score to {_scoreLimit} to unlock the Win Portal";
    }

    private void FixedUpdate()
    {
        
        _leftScoreText.text = _leftScore.ToString();
        _rightScoreText.text = _rightScore.ToString();
        if (!CheckScore()) return;
        WinPortal.SetActive(true);
        if (!_playSound) return;
        SoundManager.PlaySoundEffect("PositiveFeedback");
        _playSound = false;

    }

    public static void AddScore(bool left)
    {
        if (left)
        {
            _leftScore += 1;
        }
        else
        {
            _rightScore += 1;
        }
    }

    public static bool CheckScore()
    {
        return _leftScore >= _scoreLimit && _rightScore >= _scoreLimit;
    }

    public static void ResetScore()
    {
        _leftScore = 0;
        _rightScore = 0;
    }
}
