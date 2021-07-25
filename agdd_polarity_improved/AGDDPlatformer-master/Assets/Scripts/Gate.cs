using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;


    private void Start()
    {
        // Physics2D.IgnoreLayerCollision(9,8);
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        // Debug.Log(GetComponent<Collider2D>());
        // Debug.Log(player.GetComponent<Collider2D>());
    }

 
  

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D other)
    {

    }
}


