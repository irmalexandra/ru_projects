using System;
using System.Collections.Generic;
using UnityEngine;


public class BoomerController : MonoBehaviour
{
    private List<GameObject> _pieces;
    public float timer;
    private float _timer;
    private bool _collided;
    private bool _exploded;
    public GameObject boomMaker;

    private void Start()
    {
        _pieces = new List<GameObject>();
        foreach (Transform child in transform)
        {
            _pieces.Add(child.gameObject);
        }
        
    }



    private void Update()
    {
        if (!_collided) return;
        if (!_exploded)
        {
            Instantiate(boomMaker, transform);
            _exploded = true;
        }

        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
            Destroy(boomMaker);
        }
        else
        {
            Destroy(gameObject);
        }
    }



    public void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Ball")) return;
        //Destroy(other.gameObject);
        //TimeManager.Instance.DoSlowmotion();
        _collided = true;
        
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        _timer = timer;
        
        foreach (var piece in _pieces)
        {
            piece.SetActive(true);
        }

        
        
    }
}

