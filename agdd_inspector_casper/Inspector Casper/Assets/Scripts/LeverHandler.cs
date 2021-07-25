using System.Collections;
using TMPro;
using UnityEngine;

public class LeverHandler : MonoBehaviour
{
    public GameObject platform = null;
    private MovingPlatform movingPlatform;
    private bool playerInRange;
    private bool interactable = true;
    public string textToDisplay;

    private void Start()
    {
        movingPlatform = platform.gameObject.GetComponentInChildren<MovingPlatform>();
    }

    private void Update()
    {
        if (platform == null) { return; }
        if (!interactable) { return; }
        if (playerInRange)
        {
            if (Input.GetKey("e"))
            {
                movingPlatform.Toggle();
                GetComponent<SpriteRenderer>().flipX = true;
                interactable = false;
                var speechBubble = GameObject.FindGameObjectWithTag("SpeechBubble");
                var speechSpriteRenderer = speechBubble.GetComponent<SpriteRenderer>();
                speechSpriteRenderer.enabled = true;
                var textBox = speechBubble.GetComponentInChildren<TextMeshPro>();
                textBox.text = textToDisplay;
                StartCoroutine(Wait(5, speechSpriteRenderer, textBox));
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
