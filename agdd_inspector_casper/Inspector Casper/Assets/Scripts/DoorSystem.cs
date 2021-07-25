using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class DoorSystem : MonoBehaviour
{
    public GameObject door;
    private bool playerInRange;
    public string keyName;
    private bool unlocked;
    public Sprite openDoorSprite;


    private void Update()
    {
        if (playerInRange)
        {
            
            if (Input.GetKey("e"))
            {
                var keys = GameManager.instance.getPlayerController().GetKeys();
                if (keys.ContainsKey(keyName))
                {
                    if (keys[keyName])
                    {
                        door.gameObject.GetComponent<Collider2D>().enabled = false;
                        if (!unlocked)
                        {
                            SoundManager.PlaySoundEffect("DoorCreak");
                        }
                        unlocked = true;
                        door.GetComponent<SpriteRenderer>().sprite = openDoorSprite;
                    }
                }
                else
                {
                    var speechBubble = GameObject.FindGameObjectWithTag("SpeechBubble");
                    var speechSpriteRenderer = speechBubble.GetComponent<SpriteRenderer>();
                    speechSpriteRenderer.enabled = true;
                    var textBox = speechBubble.GetComponentInChildren<TextMeshPro>();
                    
                    
                    switch (keyName)
                    {
                        case "RedKey":
                            textBox.text = "This door is locked. I need to find a key. Maybe on the other side of the castle?";
                            break;
                        default:
                            textBox.text = "This door is locked. There must be a key around here somewhere.";
                            break;
                    }
                    StartCoroutine(Wait(5, speechSpriteRenderer, textBox));
                }
            }
        }
    }

    private IEnumerator Wait(int seconds, SpriteRenderer speechSpriteRenderer, TextMeshPro textBox)
    {
        yield return new WaitForSeconds(seconds);
        
        speechSpriteRenderer.enabled = false;
        textBox.text = "";
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            GameManager.instance.getPlayerController().showInteractiveButton(true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            GameManager.instance.getPlayerController().showInteractiveButton(false);
        }
    }
}
