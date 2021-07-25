using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.SceneManagement;

public class DoorHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public string doorName;
    private bool _inRange = false;
    private bool _exiting;
    public GameObject exit;
    private bool _canTeleport = true;


    private void FixedUpdate()
    {
        if (_inRange)
        {
            if (Input.GetKey(KeyCode.E))
            {
                if (_canTeleport)
                {
                    var playerController = GameManager.instance.getPlayer().GetComponent<PlayerController>();
                    if (doorName == "KeyChamber" && playerController.hunted)
                    {
                        MusicManager.Instance.PlayPart2();
                        GameManager.instance.flipGhostType();
                    } else if (doorName == "FinalDoor" && playerController.hasFinalKey)
                    {
                        if (GameManager.instance.hasDied)
                        {
                            SceneManager.LoadScene("EndingB");

                        }
                        else
                        {
                            SceneManager.LoadScene("EndingA");
                        }
                    }
                    playerController.transform.position = exit.transform.position;
                    
                }
            }
        }
    }
    
    private IEnumerator TeleportCooldownCoroutine(PlayerController script)
    {

        _canTeleport = false;
        yield return new WaitForSeconds(0.5f);
        _canTeleport = true;
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_canTeleport)
        {
            return;
        }
        if (other.CompareTag("Player"))
        {
            var playerScript = other.GetComponent<PlayerController>();
            StartCoroutine(TeleportCooldownCoroutine(playerScript));
            _inRange = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (_canTeleport)
            {
                var playerScript = other.GetComponent<PlayerController>();

                if (doorName == "FinalDoor")
                {
                    if (playerScript.hasFinalKey)
                    {
                        playerScript.showInteractiveButton(true);
                    }
                }
                else 
                {
                    playerScript.showInteractiveButton(true);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var playerScript = other.GetComponent<PlayerController>();
            playerScript.showInteractiveButton(false);
            _inRange = false;
        }
    }
}
