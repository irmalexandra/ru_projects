using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;

public class GhostSense : MonoBehaviour
{
    public string textToDisplay;
    private PlayerController playerController;
    private bool tutorial = true;
    
    private GameObject speechBubble;
    private SpriteRenderer speechSpriteRenderer;
    private TextMeshPro textBox;

    private IEnumerator Wait(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        playerController._nervous = false;
        if (tutorial)
        {
            speechSpriteRenderer.enabled = false;
            textBox.text = "";
            tutorial = false;
        }
    }

    private IEnumerator HeartbeatCoroutine(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        HeartbeatController.PlayHeartbeat("Stop");
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        playerController = GameManager.instance.getPlayerController();
        if (!playerController) { return; }
        if (!other.gameObject.CompareTag("Enemy")) { return; }
        if (!(other is BoxCollider2D)) return;
        if (!tutorial) return;
       
        speechBubble = GameObject.FindGameObjectWithTag("SpeechBubble");
        speechSpriteRenderer = speechBubble.GetComponent<SpriteRenderer>();
        speechSpriteRenderer.enabled = true;
        textBox = speechBubble.GetComponentInChildren<TextMeshPro>();
        textBox.text = textToDisplay;
        SoundManager.PlaySoundEffect("GhostSighting");
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!playerController) { return; }
        if (!other.gameObject.CompareTag("Enemy")) { return; }
        if (!(other is BoxCollider2D)) return;

        playerController._nervous = true;
        HeartbeatController.PlayHeartbeat("Start");

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other is BoxCollider2D)
            {
                if (playerController._nervous)
                {
                    StartCoroutine(tutorial ? Wait(5) : Wait(2));
                    StartCoroutine(HeartbeatCoroutine(1));
                }
            }
        }
    }
}
