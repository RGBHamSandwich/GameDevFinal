using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    ///// PUBLIC VARIABLES /////
    [Header("Audio Sources")]
    public AudioSource musicSource;
    public float musicVolume = 0.3f;
    public bool musicMuted = false;
    public AudioSource sfxSource;
    public float sfxVolume = 0.1f;
    public bool sfxMuted = false;
    [Header("Themes")]
    public AudioClip titleMusic;
    public AudioClip levelMusic;
    [Header("Gameplay SFX")]
    public AudioClip hitSound;
    public AudioClip holeSound;
    public AudioClip sandSound;
    public AudioClip waterSound;
    public AudioClip bounceSound;
    [Header("UI SFX")]
    public AudioClip startButtonSound;
    public AudioClip menuButtonSound;
    public AudioClip ClickInSound;
    public AudioClip ClickOutSound;
    public static AudioManagerScript instance;

    ///// GIVEN METHODS /////
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        musicSource.clip = titleMusic;
        musicSource.loop = true;
        musicSource.volume = musicVolume;
        musicSource.PlayDelayed(0.5f); // delay heheh
    }

    void Update()
    {
        if(musicMuted)
        {
            musicSource.volume = 0f;
        }
        else
        {
            musicSource.volume = musicVolume;
        }
        
        if(sfxMuted)
        {
            sfxSource.volume = 0f;
        }
        else
        {
            sfxSource.volume = sfxVolume;
        }
    }

    ///// THEMES /////
    public void PlayTitleMusic()
    {
        musicSource.Stop();
        musicSource.clip = titleMusic;
        musicSource.loop = true;
        musicSource.volume = musicVolume;
        musicSource.PlayDelayed(0.5f);
    }

    public void PlayLevelMusic()
    {
        musicSource.Stop();
        musicSource.clip = levelMusic;
        musicSource.loop = true;
        musicSource.volume = musicVolume;
        musicSource.PlayDelayed(2f); // delay for button sfx to play
    }

    ///// TOGGLE MUSIC AND SFX VOLUME /////
    public void ToggleMusicVolumeOff()
    {
        musicMuted = true;
    }

    public void ToggleMusicVolumeOn()
    {
        musicMuted = false;
    }

    public void UpdateMusicVolume(float volume)
    {
        musicVolume = volume;
        musicSource.volume = musicVolume;
    }

    public void ToggleSFXVolumeOff()
    {
        sfxMuted = true;
    }

    public void ToggleSFXVolumeOn()
    {
        sfxMuted = false;
    }

    public void UpdateSFXVolume(float volume)
    {
        sfxVolume = volume;
        sfxSource.volume = sfxVolume;
    }

    ////// GAMEPLAY SOUNDS //////
    public void PlayHitSound()
    {
        sfxSource.PlayOneShot(hitSound, sfxVolume);
    }

    public void PlayHoleSound()
    {
        sfxSource.PlayOneShot(holeSound, sfxVolume);
    }

    public void PlaySandSound()
    {
        sfxSource.PlayOneShot(sandSound, sfxVolume / 2);
    }

    public void PlayWaterSound()
    {
        sfxSource.PlayOneShot(waterSound, sfxVolume / 3);
    }

    public void PlayBounceSound()
    {
        sfxSource.PlayOneShot(bounceSound, sfxVolume);
    }

    ////// UI SOUNDS //////
    public void PlayStartButtonSound()
    {
        sfxSource.PlayOneShot(startButtonSound, sfxVolume * 2f);
    }

    public void PlayMenuButtonSound()
    {
        sfxSource.PlayOneShot(menuButtonSound, sfxVolume * 2f);
    }

    public void PlayClickInSound()
    {
        sfxSource.PlayOneShot(ClickInSound, sfxVolume * 2f);
    }

    public void PlayClickOutSound()
    {
        sfxSource.PlayOneShot(ClickOutSound, sfxVolume * 2f);
    }

}
