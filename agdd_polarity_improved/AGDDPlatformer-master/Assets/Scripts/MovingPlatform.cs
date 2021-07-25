using System;
using System.Collections;
using System.Collections.Generic;
using AGDDPlatformer;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pos1;
    public Transform pos2;
    public Transform startPos;
    public float stop_timer;
    public float speed;

    private float _timer;
    private bool _stop;
    public Vector3 nextPos;

    // Start is called before the first frame update
    void Start()
    {
        nextPos = startPos.position;
        _timer = 0;
        stop_timer++;

    }

    public bool GetStop()
    {
        return _stop;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(_timer >= 0)
        {
            _timer -= Time.deltaTime;

        }
        if (_timer <= 1)
        {
            _stop = false;
            transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
        }

        if (transform.position == pos1.position && _timer < 0)
        {
            _timer = stop_timer;
            _stop = true;
            nextPos = pos2.position;

        }    
        if (transform.position == pos2.position && _timer < 0)
        {
            _timer = stop_timer;
            _stop = true;
            nextPos = pos1.position;
        }
        
        



    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
}
