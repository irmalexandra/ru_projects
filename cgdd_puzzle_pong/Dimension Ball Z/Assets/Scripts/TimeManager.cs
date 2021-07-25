using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowdownFactor = 0.05f;
    public float slowdownLength = 2f;

    private bool _paused = true;

    private float _previousTimeScale = 1;

    public static TimeManager Instance;

    public void Start()
    {
        Instance = this;
    }

    void Update ()
    {
        if (!_paused)
        {
            Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        }
    }
    
    public void DoSlowmotion ()
    {
        if (!_paused)
        {
            Time.timeScale = slowdownFactor;
            Time.fixedDeltaTime = Time.timeScale * .02f;
        }
        
    }
    

    public void Pause()
    {
        _paused = true;
        _previousTimeScale = Time.timeScale;
        Time.timeScale = 0;
        
    }

    public void Resume()
    {
        _paused = false;
        Time.timeScale = _previousTimeScale;
        
        
    }

    public bool GetPaused()
    {
        return _paused;
    }
}
