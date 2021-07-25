using UnityEngine;

public class ObjectSlowmotionTrigger : MonoBehaviour
{
 private void OnCollisionEnter2D(Collision2D other)
 {
  if (other.gameObject.CompareTag("Ball"))
  {
   TimeManager.Instance.DoSlowmotion();
  }
 }
}
