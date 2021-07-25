using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    
    public TMPro.TextMeshProUGUI fpsDisplay;
    private const float HudRefreshRate = 1f;
    private float _timer;
    private void Update()
    {
        if (Time.unscaledTime > _timer)
        {
            var fps = (int)(1f / Time.unscaledDeltaTime);
            fpsDisplay.text = "FPS: " + fps;   
            _timer = Time.unscaledTime + HudRefreshRate;
        }
    }
}
