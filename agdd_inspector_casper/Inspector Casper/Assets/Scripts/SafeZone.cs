using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var script = other.gameObject.GetComponent<PlayerController>();
            if (script)
            {
                script.insideSafeZone = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var script = other.gameObject.GetComponent<PlayerController>();
            if (script)
            {
                script.insideSafeZone = false;
            }
        }
    }
}
