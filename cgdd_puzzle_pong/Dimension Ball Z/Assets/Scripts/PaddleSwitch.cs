using UnityEngine;

public class PaddleSwitch : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            GameManager.Instance.SwitchPaddle(transform.gameObject.GetComponentsInChildren<PaddleController>());
        }
    }
}
