
using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform _target;
    
    public float smoothSpeed = 0.125f;

    public Vector3 offset;

    private void Start()
    {
        _target = GameManager.instance.getPlayer().transform;
    }
    
    private void FixedUpdate()
    {
        Vector3 desiredPosition = _target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
