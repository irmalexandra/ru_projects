using UnityEngine;

public class WinPortalController : MonoBehaviour
{
    
    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Ball"))
        {
            SoundManager.PlaySoundEffect("PortalSoundEffect");
            Destroy(collision.collider.gameObject); 
            GameManager.Instance.TriggerLevelCompleteMenu();
            LevelTimer.StopTimer();
        }
    }
}
