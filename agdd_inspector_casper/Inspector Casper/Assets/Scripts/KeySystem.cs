using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySystem : MonoBehaviour
{
    // Start is called before the first frame update
    

    private bool playerInRange;
    public string keyName;
    private void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKey("e"))
            {
                var playerController = GameManager.instance.getPlayer().GetComponent<PlayerController>();
                gameObject.SetActive(false);
                var keySprite = gameObject.GetComponent<SpriteRenderer>().sprite;
                var keyColor = gameObject.GetComponent<SpriteRenderer>().color;
                if (keyName == "FinalKey")
                {
                    GameObject.FindGameObjectWithTag("drawnText").gameObject.GetComponent<BoxCollider2D>().enabled =
                        true;
                    playerController.hunted = true;
                    
                    playerController.flashController.cooldownTimer = 2.5f;
                }
                playerController.takeKey(keyName, keySprite, keyColor);
            }
        }
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
            GameManager.instance.getPlayer().GetComponent<PlayerController>().showInteractiveButton(false);
        }
    }
}
