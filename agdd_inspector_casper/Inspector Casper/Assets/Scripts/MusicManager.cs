using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static AudioSource _musicSource;
    private static AudioClip _gameplaySong1;
    private static AudioClip _gameplaySong2;
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
    void Awake()
    {
        _gameplaySong1 = Resources.Load<AudioClip>("Audio/Music/bensound-ofeliasdream");
        _gameplaySong2 = Resources.Load<AudioClip>("Audio/Music/bensound-instinct");
        if(_instance == null)
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
        _musicSource.clip = _gameplaySong1;
        PlayPart1();
    }
    

    public void PlayPart1()
    {
        if (_musicSource.isPlaying) return;
        _musicSource.enabled = true;
        _musicSource.Play();
    }
     public void PlayPart2()
     {
         _musicSource.volume = 0.3f;
         _musicSource.clip = _gameplaySong2;
         _musicSource.Play();
     }
 
    public void StopMusic()
    {
        _musicSource.Stop();
    }
}