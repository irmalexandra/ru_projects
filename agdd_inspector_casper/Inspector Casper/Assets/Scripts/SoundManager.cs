using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static AudioClip _shutterSound;
    private static AudioClip _shutterSoundEcho;
    private static AudioClip _deathSound;
    private static AudioClip _flashRechargeSound;
    private static AudioClip _flashRechargeSoundShort;
    private static AudioClip _ghostAware;
    private static AudioClip _scream;
    private static AudioClip _keyPickup;
    private static AudioClip _doorCreak;
    private static AudioClip _rechargeClick;
    private static AudioClip _firstGhostSighting;

    private static AudioSource _audioSource;
    void Start()
    {
        _shutterSound = Resources.Load<AudioClip>("Audio/Sounds/CameraShutter1");
        _shutterSoundEcho = Resources.Load<AudioClip>("Audio/Sounds/CameraShutter1Echo");
        _deathSound = Resources.Load<AudioClip>("Audio/Sounds/Death1");
        _flashRechargeSound = Resources.Load<AudioClip>("Audio/Sounds/FlashRecharge");
        _flashRechargeSoundShort = Resources.Load<AudioClip>("Audio/Sounds/FlashRechargeShort");
        _ghostAware = Resources.Load<AudioClip>("Audio/Sounds/ToasterGhostChasing");
        _scream = Resources.Load<AudioClip>("Audio/Sounds/DemonicScream");
        _keyPickup = Resources.Load<AudioClip>("Audio/Sounds/KeyPickup");
        _doorCreak = Resources.Load<AudioClip>("Audio/Sounds/DoorCreak");
        _rechargeClick = Resources.Load<AudioClip>("Audio/Sounds/FlashRechargeClick");
        _firstGhostSighting = Resources.Load<AudioClip>("Audio/Sounds/GhostMeetup");

        _audioSource = GetComponent<AudioSource>();
    }
    
    public static void PlaySoundEffect(string soundEffectName)
    {
        switch (soundEffectName)
        {
            case "Shutter":
                _audioSource.PlayOneShot(_shutterSound);
                _audioSource.PlayOneShot(_flashRechargeSound);
                break;            
            case "ShutterEcho":
                _audioSource.PlayOneShot(_shutterSoundEcho);
                _audioSource.PlayOneShot(_flashRechargeSound);
                break;            
            case "ShutterEchoShort":
                _audioSource.PlayOneShot(_shutterSoundEcho);
                _audioSource.PlayOneShot(_flashRechargeSoundShort);
                break;
            case "RechargeClick":
                _audioSource.PlayOneShot(_rechargeClick);
                break;
            case "DoorCreak":
                _audioSource.PlayOneShot(_doorCreak);
                break;
            case "Death":
                _audioSource.PlayOneShot(_deathSound);
                break;
            case "Scream":
                _audioSource.PlayOneShot(_scream);
                break;
            case "KeyPickup":
                _audioSource.PlayOneShot(_keyPickup);
                break;
            case "GhostSighting":
                _audioSource.PlayOneShot(_firstGhostSighting);
                break;
        }
    }


}
