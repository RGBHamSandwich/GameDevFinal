using UnityEngine;
using UnityEngine.UI;

public class SettingsUIManagerScript : MonoBehaviour
{
    ///// PUBLIC VARIABLES /////
    [Header("Settings Auto-Assigned Values")]
    public GameObject ENVIRONMENT;
    public GameObject GAMEPLAY;
    public GameObject titleScreenUI;
    public GameObject levelUI;
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
        RevealAllVisuals();
        _ballControllerScript = FindFirstObjectByType<BallController>();
        _ballControllerScript?.TrueCanHitBall();
        _audioManagerScript?.PlayClickOutSound();   
        Destroy(transform.root.gameObject);
    }

    public void RevealAllVisuals()
    {
        if (_levelStatManager.level <= 0)
        {
            Canvas titleScreenCanvas = titleScreenUI.GetComponent<Canvas>();
            titleScreenCanvas.enabled = true;
        }

        Canvas levelUICanvas = levelUI.GetComponent<Canvas>();
        levelUICanvas.enabled = true;

        ENVIRONMENT.SetActive(true);
        GAMEPLAY.SetActive(true);
    }

    ///// EXIT BUTTONS /////
    public void ExitGameButton()
    {
        _audioManagerScript?.PlayClickInSound();
        _ballControllerScript?.FalseCanHitBall();
        //prompt an "Are You Sure?" popup?
    }

    public void ExitYesButton()
    {
        Application.Quit();
        Debug.Log("Application.Quit() called; this will only work in a built game.");               // necessary debug log
    }

    public void ExitNoButton()
    {
        _audioManagerScript?.PlayClickInSound();
        _ballControllerScript?.TrueCanHitBall();
        // close the "Are you sure?" popup
        _audioManagerScript?.PlayClickOutSound();
    }

    ///// SOUND BUTTONS /////
    public void MusicButton()
    {
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
        _audioManagerScript?.PlayClickInSound();
        _audioManagerScript?.UpdateMusicVolume(1 - musicSlider.value);
    }

    public void SFXButton()
    {
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
        _audioManagerScript?.PlayClickInSound();
        _audioManagerScript?.UpdateSFXVolume(1 - sfxSlider.value);
    }
}
