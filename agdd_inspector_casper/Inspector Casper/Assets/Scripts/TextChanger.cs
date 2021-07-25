using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;

public class TextChanger : MonoBehaviour
{
    public string textToDisplay;
    private bool triggered = false;
    public float displayTimer = 5;

    private GameObject speechBubble;
    private SpriteRenderer speechSpriteRenderer;
    private TextMeshPro textBox;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;
        if (!other.gameObject.CompareTag("Player")) return;
        speechBubble = GameObject.FindGameObjectWithTag("SpeechBubble");
        speechSpriteRenderer = speechBubble.GetComponent<SpriteRenderer>();
        speechSpriteRenderer.enabled = true;
        textBox = speechBubble.GetComponentInChildren<TextMeshPro>();
        textBox.text = textToDisplay;
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (triggered) return;
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Wait());
        }
    }
    
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(displayTimer);
        if (speechSpriteRenderer != null)
        {
            speechSpriteRenderer.enabled = false;
            textBox.text = "";
        }
        triggered = true;
        
    }
}
