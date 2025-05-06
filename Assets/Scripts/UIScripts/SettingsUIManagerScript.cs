using UnityEngine;
using UnityEngine.UI;

public class SettingsUIManagerScript : MonoBehaviour
{
    ///// PUBLIC VARIABLES /////
    [Header("Menu Prefab")]
    public GameObject confirmMenu;
    [Header("Menu Buttons")]
    public Toggle musicToggle;
    public Slider musicSlider;
    public Toggle sfxToggle;
    public Slider sfxSlider;

    ///// PRIVATE VARIABLES /////
    private AudioManagerScript _audioManagerScript;
    private ToggleVisibilityScript _toggleVisibilityScript;
    // private ConfirmUIManagerScript _confirmUIManagerScript;

    ///// GIVEN METHODS /////
    void Start()
    {
        _audioManagerScript = FindFirstObjectByType<AudioManagerScript>();
        _toggleVisibilityScript = FindFirstObjectByType<ToggleVisibilityScript>();
    }

    ///// SETTINGS BUTTONS /////
    public void SettingsCloseButton()
    {
        _toggleVisibilityScript.ShowVisuals();
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
