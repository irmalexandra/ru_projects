using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class Platform : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 velocity;
    public Rigidbody2D body;
    public bool moving;
    public int direction;
    public double travel_distance;
    private double start_position;
    private double end_position;

    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
    //     {
    //         collision.collider.transform.SetParent(transform);
    //     }
    // }
    //
    // private void OnCollisionExit2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
    //     {
    //         collision.collider.transform
    //     }
    // }

    private void Start()
    {
        start_position = transform.position.x;
        end_position = start_position + travel_distance;
        body.velocity = velocity * (Time.deltaTime * direction);
        // Debug.Log(end_position);
    }

    private void FixedUpdate()
    {
        if (transform.position.x >= end_position)
        {
            body.velocity = velocity * (Time.deltaTime * direction * -1);
        }

        if (transform.position.x < start_position)
        {
            body.velocity = velocity * (Time.deltaTime * direction * -1);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        direction *= -1;
    }
}
