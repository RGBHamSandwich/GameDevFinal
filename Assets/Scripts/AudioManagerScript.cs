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
    public AudioClip bounceSound;     // wall sound? or just use Unity's physics material?
    public AudioClip brotherHitSound;
    [Header("UI SFX")]
    public AudioClip startButtonSound;
    public AudioClip menuButtonSound;
    public AudioClip ClickInSound;
    public AudioClip ClickOutSound;
    public static AudioManagerScript instance;

    ///// METHODS /////
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
        musicSource.PlayDelayed(0.5f); // optional delay
        Debug.Log("Title music played!");
    }

    public void PlayLevelMusic()
    {
        musicSource.Stop();
        musicSource.clip = levelMusic;
        musicSource.loop = true;
        musicSource.volume = musicVolume;
        musicSource.PlayDelayed(2f); // delay for button sfx to play
        Debug.Log("Level music played!");
    }

    ///// TOGGLE MUSIC AND SFX VOLUME /////
    public void ToggleMusicVolumeOff()
    {
        musicMuted = true;
        Debug.Log("Music volume toggled!");
    }

    public void ToggleMusicVolumeOn()
    {
        musicMuted = false;
        Debug.Log("Music volume toggled!");
    }

    public void ToggleSFXVolumeOff()
    {
        sfxMuted = true;
        Debug.Log("SFX volume toggled!");
    }

    public void ToggleSFXVolumeOn()
    {
        sfxMuted = false;
        Debug.Log("SFX volume toggled!");
    }

    ////// GAMEPLAY SOUNDS //////
    public void PlayHitSound()
    {
        sfxSource.PlayOneShot(hitSound, sfxVolume);
        Debug.Log("Hit sound played!");
    }

    public void PlayHoleSound()
    {
        sfxSource.PlayOneShot(holeSound, sfxVolume);
        Debug.Log("Hole sound played!");
    }

    public void PlaySandSound()
    {
        sfxSource.PlayOneShot(sandSound, sfxVolume / 2);
        Debug.Log("Sand sound played!");
    }

    public void PlayWaterSound()
    {
        sfxSource.PlayOneShot(waterSound, sfxVolume / 3);
        Debug.Log("Water sound played!");
    }

    public void PlayBounceSound()
    {
        sfxSource.PlayOneShot(bounceSound, sfxVolume);
        Debug.Log("Wall sound played!");
    }

    public void PlayBrotherHitSound()
    {
        sfxSource.PlayOneShot(brotherHitSound, sfxVolume);
        Debug.Log("Brother hit sound played!");
    }

    ////// UI SOUNDS //////
    public void PlayStartButtonSound()
    {
        sfxSource.PlayOneShot(startButtonSound, sfxVolume * 2f);
        Debug.Log("Start button sound played!");
    }

    public void PlayMenuButtonSound()
    {
        sfxSource.PlayOneShot(menuButtonSound, sfxVolume * 2f);
        Debug.Log("Menu button sound played!");
    }

    public void PlayClickInSound()
    {
        sfxSource.PlayOneShot(ClickInSound, sfxVolume * 2f);
        Debug.Log("Click in sound played!");
    }

    public void PlayClickOutSound()
    {
        sfxSource.PlayOneShot(ClickOutSound, sfxVolume * 2f);
        Debug.Log("Click out sound played!");
    }

}
