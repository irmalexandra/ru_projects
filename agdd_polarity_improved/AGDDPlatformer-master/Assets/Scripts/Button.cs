using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] to_destroy;
    public GameObject button_display;
    public GameObject button_face;
    public bool toggle;
    public float cooldown_time;
    
    private bool _playerInRange;
    private float _cooldown = 0f;
    private bool _pressed;
    private SpriteRenderer _button_face_renderer;
    private Color _original_button_color;
    
    public AudioSource source;
    public AudioClip buttonToggle;
    public AudioClip buttonPress;
    
    void Start()
    {
        _playerInRange = false;
        _pressed = false;
        _button_face_renderer = button_face.GetComponent<SpriteRenderer>();
        _original_button_color = _button_face_renderer.color;

    }

    private void Update()
    {
        if (_playerInRange)
        {
            // display tutorial
            if (!toggle && !_pressed)
            {
                button_display.SetActive(true);
            }
            else if (toggle)
            {
                button_display.SetActive(true);
            }
            if (_cooldown > 0)
            {
                _cooldown -= Time.deltaTime;
            }
            
            if (!Input.GetKey("e")) return;
            // e pressed
            
            if (!toggle && !_pressed)
            {
                source.PlayOneShot(buttonPress);
                foreach (var item in to_destroy)
                {
                    item.SetActive(!item.activeInHierarchy);
                }
                Color new_color = new Color(_original_button_color.r - 0.5f, _original_button_color.g, _original_button_color.b);
                _button_face_renderer.color = new_color;
                _pressed = true;
                button_display.SetActive(false);
            }
            else if(toggle)
            {
                if (_cooldown <= 0)
                {
                    source.PlayOneShot(buttonToggle);
                    foreach (var item in to_destroy)
                    {
                        item.SetActive(!item.activeInHierarchy);
                    }

                    _cooldown = cooldown_time;
                }
       
            }


        }
        else
        {
            button_display.SetActive(false);
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        _playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _playerInRange = false;
    }
}
