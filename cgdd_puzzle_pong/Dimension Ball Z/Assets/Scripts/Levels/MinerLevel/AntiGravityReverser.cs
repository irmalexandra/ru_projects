using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiGravityReverser : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.GetComponent<Rigidbody2D>().gravityScale *= -1;
    }
}
