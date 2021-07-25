using UnityEngine;

public class ReverseGravity : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Ball")) return;
        gameObject.GetComponent<Rigidbody2D>().gravityScale *= -1;



    }
}
