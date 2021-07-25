using System;
using System.Collections;
using System.Collections.Generic;
using AGDDPlatformer;
using UnityEngine;
public class Spikes : MonoBehaviour
{
    public AudioSource source;
    public AudioClip death;


    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player1") && !other.gameObject.CompareTag("Player2")) return;
        source.PlayOneShot(death);
        other.gameObject.SetActive(false);
        StartCoroutine(PlayerKilled());
    }


    private static IEnumerator PlayerKilled()
    {
        yield return new WaitForSeconds(1);
        GameManager.instance.ResetLevel();
    }
}
