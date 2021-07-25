using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySystem : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject button_display;

    private GameObject _player_in_range;
    private bool _taken;
    private SpriteRenderer self_sprite_renderer;
    private BoxCollider2D self_box_collider;

    private void Start()
    {
       
        _taken = false;
        self_sprite_renderer = GetComponent<SpriteRenderer>();
        self_box_collider = GetComponent<BoxCollider2D>();

    }

    private void Update()
    {
        if (_player_in_range && !_taken)
        {
            button_display.SetActive(true);
            
            if (Input.GetKey("e"))
            {
               
                var key_transform = transform;
                //key_transform.parent = _player_in_range.transform;
                self_sprite_renderer.sortingOrder = 2;
                _taken = true;
                //key_transform.position = _player_in_range.transform.position;
                key_transform.localScale = new Vector3(0.5f, 0.5f, 0f);
                foreach (Transform child in  _player_in_range.transform)
                {
                    if (child.gameObject.CompareTag("Player_hand"))
                    {
                        key_transform.parent = child.transform;
                        key_transform.position = child.position;
                        key_transform.rotation = child.transform.rotation;
                        /*transform.parent.gameObject.SetActive(false);
                        child.gameObject.SetActive(false);*/
                    }
                }
                self_box_collider.enabled = false;
                transform.Rotate(Vector3.right, 45f);

            }
            
        }
        else
        {
            button_display.SetActive(false);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        _player_in_range = other.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _player_in_range = null;
    }
    
    
}
