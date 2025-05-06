using UnityEngine;
using UnityEngine.UI;

public class SettingsUIManagerScript : MonoBehaviour
{
    ///// PUBLIC VARIABLES /////
    [Header("Settings Auto-Assigned Values")]
    public GameObject ENVIRONMENT;
    public GameObject GAMEPLAY;
    [Header("Menu Buttons")]
    public Toggle musicToggle;
    public Slider musicSlider;
    public Toggle sfxToggle;
    public Slider sfxSlider;

    ///// PRIVATE VARIABLES /////
    private LevelStatManager _levelStatManager;
    private AudioManagerScript _audioManagerScript;
    private BallController _ballControllerScript;
    void Start()
    {
        _levelStatManager = FindFirstObjectByType<LevelStatManager>();
        _audioManagerScript = FindFirstObjectByType<AudioManagerScript>();
    }

    void Update()
    {
    }

    public void SettingsCloseButton()
    {
        Debug.Log("Settings Close Button Pressed");
        _audioManagerScript?.PlayClickOutSound();   
        
        if (_levelStatManager.level <= 0)
        {
            GameObject titleScreenUI = GameObject.FindGameObjectWithTag("TitleScreenUI");
            Canvas titleScreenCanvas = titleScreenUI.GetComponent<Canvas>();
            titleScreenCanvas.enabled = true;
        }

        GameObject levelUI = GameObject.FindGameObjectWithTag("LevelUI");
        Canvas levelUICanvas = levelUI.GetComponent<Canvas>();
        levelUICanvas.enabled = true;

        ENVIRONMENT.SetActive(true);

        GAMEPLAY.SetActive(true);
        _ballControllerScript = FindFirstObjectByType<BallController>();
        _ballControllerScript?.TrueCanHitBall();

        Destroy(transform.root.gameObject);
    }

    ///// EXIT BUTTONS /////
    public void ExitGameButton()
    {
        Debug.Log("Exit Button Pressed");
        _audioManagerScript?.PlayClickInSound();

        Debug.Log("Level: " + _levelStatManager.level.ToString());
        // _ballControllerScript?.FalseCanHitBall();
        //prompt an "Are You Sure?" popup?
    }

    public void ExitYesButton()
    {
        Debug.Log("Exit Yes Button Pressed");
        Application.Quit();
        // sound?
    }

    public void ExitNoButton()
    {
        Debug.Log("Exit No Button Pressed");
        // close the "Are you sure?" popup
        _audioManagerScript?.PlayClickOutSound();
    }

    ///// SOUND BUTTONS /////
    public void MusicButton()
    {
        Debug.Log("Music Off Button Pressed");
        Debug.Log("isOn: " + musicToggle.isOn.ToString());
        _audioManagerScript?.PlayClickInSound();

        if (musicToggle.isOn)
        {
            _audioManagerScript?.ToggleMusicVolumeOn();
        }
        else 
        {
            _audioManagerScript?.ToggleMusicVolumeOff();
        }
    }

    public void MusicSlider()
    {
        Debug.Log("Music Slider Pressed");
        Debug.Log("Volume: " + musicSlider.value.ToString());
        _audioManagerScript?.PlayClickInSound();
        _audioManagerScript?.UpdateMusicVolume(1 - musicSlider.value);
    }

    public void SFXButton()
    {
        Debug.Log("SFX Button Pressed");
        Debug.Log("isOn: " + sfxToggle.isOn.ToString());
        _audioManagerScript?.PlayClickInSound();

        if (sfxToggle.isOn)
        {
            _audioManagerScript?.ToggleSFXVolumeOn();
        }
        else
        {
            _audioManagerScript?.ToggleSFXVolumeOff();
        }
    }

    public void SFXSlider()
    {
        Debug.Log("SFX Slider Pressed");
        Debug.Log("Volume: " + sfxSlider.value.ToString());
        _audioManagerScript?.PlayClickInSound();
        _audioManagerScript?.UpdateSFXVolume(1 - sfxSlider.value);
    }
}
