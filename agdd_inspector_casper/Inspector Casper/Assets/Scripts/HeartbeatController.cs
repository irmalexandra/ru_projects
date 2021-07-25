using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartbeatController : MonoBehaviour
{
    private static AudioClip _shutterSound;
    private static AudioClip _shutterSoundEcho;
    private static AudioClip _deathSound;
    private static AudioClip _flashRechargeSound;
    private static AudioClip _ghostAware;
    private static AudioClip _heartbeat;
    private static AudioClip _scream;
    private static AudioClip _keyPickup;

    private static AudioSource _audioSource;
    void Start()
    {
        _heartbeat = Resources.Load<AudioClip>("Audio/Sounds/SingleHeartbeat");

        _audioSource = GetComponent<AudioSource>();
    }
    
    public static void PlayHeartbeat(string toggle)
    {
        switch (toggle)
        {
            case "Start":
                if (_audioSource.isPlaying) { return; }
                _audioSource.clip = _heartbeat;
                _audioSource.Play();
                break;
            case "Stop":
                _audioSource.Stop();
                break;
        }
    }


}
