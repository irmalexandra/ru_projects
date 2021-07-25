using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static AudioSource _musicSource;
    private static AudioClip _gameplaySong;
    private static MusicManager _instance;

    public static MusicManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<MusicManager>();
                DontDestroyOnLoad(_instance.gameObject);
            }
 
            return _instance;
        }
    }
    private void Awake()
    {        if(_instance == null)
        {

            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if(this != _instance)
                Destroy(gameObject);
        }
        _musicSource = gameObject.GetComponent<AudioSource>();
        _musicSource.playOnAwake = false;
        PlayMusic();
    }

    public void PlayMusic()
    {
        if (_musicSource.isPlaying) return;
        _musicSource.enabled = true;
        _musicSource.Play();
    }
 
    public void StopMusic()
    {
        _musicSource.Stop();
    }
}