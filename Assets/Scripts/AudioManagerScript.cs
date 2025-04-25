using Unity.VisualScripting;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    
    ///// PUBLIC VARIABLES /////
    public AudioSource musicSource;
    public float musicVolume = 0.3f;
    public AudioSource sfxSource;
    [Header("Audio Clips")]
    public AudioClip titleMusic;
    public AudioClip levelMusic;
    public AudioClip hitSound;
    // etc...
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
        sfxSource.PlayOneShot(hitSound, 1f);
        Debug.Log("Hit sound played!");
    }
}
