using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomerController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Ball"))
        {
            JointMotor2D newDirection = GetComponent<SliderJoint2D>().motor;
            newDirection.motorSpeed *= -1;
            GetComponent<SliderJoint2D>().motor = newDirection;
        }
    }
}
