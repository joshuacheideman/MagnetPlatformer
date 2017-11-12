using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    /// <summary>
    /// Text noise, pitch will be altered to must ba a separate audio source
    /// </summary>
    AudioSource textSound;
    /// <summary>
    /// All other sound effects are played through this source
    /// </summary>
    AudioSource sfx;
    /// <summary>
    /// Plays music, gets added to object at runtime
    /// </summary>
    AudioSource music;
    /// <summary>
    /// Private singleton instance of SoundManager
    /// </summary>
    private static SoundManager _instance;
    /// <summary>
    /// Stores volume of sfx and textSound
    /// </summary>
    private float _sfxVolume;
    /// <summary>
    /// Public variable to access/set sfx/textsound volume
    /// </summary>
    public float sfxVolume
    {
        get
        {
            return _sfxVolume;
        }
        set
        {
            _sfxVolume = value;
            sfx.volume = _sfxVolume;
            textSound.volume = _sfxVolume;
        }
    }
    /// <summary>
    /// Stores volume of music
    /// </summary>
    private float _musicVolume;
    /// <summary>
    /// Public variable to access/set music volume
    /// </summary>
    public float musicVolume
    {
        get
        {
            return _musicVolume;
        }
        set
        {
            _musicVolume = value;
            music.volume = _musicVolume;
        }
    }

    /// <summary>
    /// Public instance variables
    /// </summary>
    public static SoundManager instance
    {
        get
        {
            return _instance;
        }
    }

    /// <summary>
    /// Make SoundManager a singleton
    /// </summary>
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(transform.parent);
            _instance = this;
            Initialize();

        }
    }

    /// <summary>
    /// Sets text and sfx AudioSources and adds music AudioSources
    /// </summary>
    void Initialize()
    {
        textSound = GetComponents<AudioSource>()[0];
        sfx = GetComponents<AudioSource>()[1];
        music = GetComponents<AudioSource>()[2];
        music.loop = true;
    }

    /// <summary>
    /// Gets added to scene load by GameManager
    /// Changes song to corresponding level onLoad
    /// </summary>
    /// <param name="sceneName"></param>
    public void OnSceneLoad(string sceneName)
    {
        playSong(sceneName);
    }

    /// <summary>
    /// Stops currently playing sounds
    /// </summary>
    private void StopAll()
    {
        music.Stop();
        sfx.Stop();
        textSound.Stop();
    }

    /// <summary>
    /// Plays song with given name from Resources/Songs
    /// </summary>
    /// <param name="song"></param>
    public void playSong(string song)
    {
        StopAll();
        switch (song)
        {
            case "MainMenu":
                music.clip = Resources.Load("Songs/MagnetBoy") as AudioClip;
                break;
            case "Level1":
                music.clip = Resources.Load("Songs/GroovyFields") as AudioClip;
                break;
            default:
                Debug.Log("No music for this level");
                return;
        }
        music.Play();
    }

    /// <summary>
    /// Plays menu button select noise
    /// </summary>
    public void PlayMenuNoise()
    {
        PlaySound("MenuNoise");
    }

    /// <summary>
    /// Plays given sound from Resources folder
    /// </summary>
    /// <param name="sound"></param>
    public void PlaySound(string sound)
    {
        AudioClip clip = Resources.Load("Sounds/" + sound) as AudioClip;
        sfx.PlayOneShot(clip);
    }

    /// <summary>
    /// Plays sound of a character being printed
    /// Randomized pitch
    /// </summary>
    public void PlayTextSound()
    {
        textSound.pitch = Random.Range(1f, 1.3f);
        textSound.Play();
    }
}
