using UnityEngine;

public class RevealGhosts : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Enemy")) { return; }
        if (!other.gameObject.GetComponent<SpriteRenderer>().enabled && other is BoxCollider2D)
        {
            other.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Enemy")) { return; }
        if (other.gameObject.GetComponent<SpriteRenderer>().enabled && other is BoxCollider2D)
        {
            other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
