using UnityEngine;
using UnityEngine.UI;

public class SettingsUIManagerScript : MonoBehaviour
{
    ///// PUBLIC VARIABLES /////
    [Header("Menu Prefab")]
    public GameObject confirmMenu;
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
    private AudioManagerScript _audioManagerScript;
    private BallController _ballControllerScript;
    private ToggleVisibilityScript _toggleVisibilityScript;
    // private ConfirmUIManagerScript _confirmUIManagerScript;

    ///// METHODS /////
    void Start()
    {
        _audioManagerScript = FindFirstObjectByType<AudioManagerScript>();
        _ballControllerScript = FindFirstObjectByType<BallController>();
        _toggleVisibilityScript = FindFirstObjectByType<ToggleVisibilityScript>();
    }

    public void SettingsCloseButton()
    {
        _toggleVisibilityScript.ShowVisuals();       // initializes values for levelEnvironment, levelGameplay, titleScreenUI, and levelUI
        _audioManagerScript?.PlayClickOutSound();   
        Destroy(transform.root.gameObject);
    }

    ///// EXIT BUTTONS /////
    public void ExitGameButton()
    {
        _audioManagerScript?.PlayClickOutSound();
        InitializeConfirmMenu();
    }

    public void InitializeConfirmMenu()
    {
        Instantiate(confirmMenu, transform.position, Quaternion.identity);
    }


    ///// SOUND BUTTONS /////
    public void MusicButton()
    {
        if (musicToggle.isOn)
        {
            _audioManagerScript?.ToggleMusicVolumeOn();
            _audioManagerScript?.PlayClickInSound();
        }
        else 
        {
            _audioManagerScript?.ToggleMusicVolumeOff();
            _audioManagerScript?.PlayClickOutSound();
        }
    }

    public void MusicSlider()
    {
        _audioManagerScript?.UpdateMusicVolume(1 - musicSlider.value);
    }

    public void SFXButton()
    {
        if (sfxToggle.isOn)
        {
            _audioManagerScript?.ToggleSFXVolumeOn();
            _audioManagerScript?.PlayClickInSound();
        }
        else
        {
            _audioManagerScript?.ToggleSFXVolumeOff();
            _audioManagerScript?.PlayClickOutSound();
        }
    }

    public void SFXSlider()
    {
        _audioManagerScript?.UpdateSFXVolume(1 - sfxSlider.value);
    }
}
