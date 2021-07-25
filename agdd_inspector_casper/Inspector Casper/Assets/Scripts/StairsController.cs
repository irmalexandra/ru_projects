using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsController : MonoBehaviour
{
    // Start is called before the first frame update

    private PlatformEffector2D _effector;
    public float position1;
    public float position2;
    private bool _fixing = false;
    private float _original_position;
    private float _wait_time = 0.5f;
    private float _timer;
    void Start()
    {
        _effector = GetComponent<PlatformEffector2D>();
        _original_position = _effector.rotationalOffset;
        _timer = 0;
    }

    public void flip_effector()
    {
        _effector.rotationalOffset = position2;
        _timer = _wait_time;
    }

    private void Update()
    {
        if (_original_position != _effector.rotationalOffset)
        {
            if (_timer < 0)
            {
                _effector.rotationalOffset = position1;
            }
            else
            {
                _timer -= Time.deltaTime;
            }
        }
    }


    /*private IEnumerator fix_platform()
    {
        _fixing = true;
        yield return new WaitForSeconds(0.5f);
       
        _fixing = false;

    }*/
}
