using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathText : MonoBehaviour
{
    private TextMeshProUGUI youDied;
    private TextMeshProUGUI respawn;

    private float youDiedTextSize;
    private float respawnTextSize;
    private Color youDiedOriginalColor;
    private Color respawnOriginalColor;
    
    // Start is called before the first frame update
    void Start()
    {
        youDied = GameObject.FindWithTag("YouDiedText").GetComponent<TextMeshProUGUI>();
        respawn = GameObject.FindWithTag("RespawnText").GetComponent<TextMeshProUGUI>();
      
        youDiedTextSize = youDied.fontSize;
        respawnTextSize = respawn.fontSize;
        youDiedOriginalColor = youDied.color;
        respawnOriginalColor = respawn.color;
    }

    // Update is called once per frame
    void Update()
    {
        youDied.color = Color.Lerp(youDiedOriginalColor, Color.red, Mathf.PingPong(Time.time, 1));
        respawn.color = Color.Lerp(respawnOriginalColor, Color.red, Mathf.PingPong(Time.time, 1));
        youDied.fontSize = Mathf.Lerp(youDiedTextSize, youDiedTextSize + 5, Mathf.PingPong(Time.time, 1));
        respawn.fontSize = Mathf.Lerp(respawnTextSize, respawnTextSize + 5, Mathf.PingPong(Time.time, 1));
    }
}
