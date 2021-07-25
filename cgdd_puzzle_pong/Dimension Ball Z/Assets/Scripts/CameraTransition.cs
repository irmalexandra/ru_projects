using UnityEngine;

public class CameraTransition : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Ball")) return;
        var camera = GameObject.FindWithTag("MainCamera");
        var newPosition = transform.parent.position;
        newPosition.z = -10;
        camera.transform.position = newPosition;

    }
}
