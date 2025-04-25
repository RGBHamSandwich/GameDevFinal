using Unity.VisualScripting;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    
    ///// PUBLIC VARIABLES /////
    [Header("Audio Sources")]
    public AudioSource musicSource;
    public float musicVolume = 0.3f;
    public AudioSource sfxSource;
    public float sfxVolume = 0.5f;
    [Header("Audio Clips")]
    public AudioClip titleMusic;
    public AudioClip levelMusic;
    public AudioClip hitSound;
    public AudioClip holeSound;
    public AudioClip sandSound;
    public AudioClip waterSound;
    public AudioClip wallSound;     // wall sound? or just use Unity's physics material?
    public AudioClip brotherHitSound;
    public static AudioManagerScript instance;

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
        
    }

    public void PlayLevelMusic()
    {
        musicSource.Stop();
        musicSource.clip = levelMusic;
        musicSource.loop = true;
        musicSource.volume = musicVolume;
        musicSource.PlayDelayed(0.5f); // delay heheh
        Debug.Log("Level music played!");
    }

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
        sfxSource.PlayOneShot(sandSound, sfxVolume);
        Debug.Log("Sand sound played!");
    }

    public void PlayWaterSound()
    {
        sfxSource.PlayOneShot(waterSound, sfxVolume);
        Debug.Log("Water sound played!");
    }

    public void PlayWallSound()
    {
        sfxSource.PlayOneShot(wallSound, sfxVolume);
        Debug.Log("Wall sound played!");
    }

    public void PlayBrotherHitSound()
    {
        sfxSource.PlayOneShot(brotherHitSound, sfxVolume);
        Debug.Log("Brother hit sound played!");
    }

}
