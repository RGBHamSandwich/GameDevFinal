using UnityEngine;

public class MenuUIManagerScript : MonoBehaviour
{
    private AudioManagerScript _audioManagerScript;
    void Start()
    {
        _audioManagerScript = FindFirstObjectByType<AudioManagerScript>();
    }

    void Update()
    {
        
    }

    public void ExitButton()
    {
        Debug.Log("Exit Button Pressed");
        //prompt an "Are You Sure?" popup?
        _audioManagerScript?.PlayClickInSound();
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
        // close the popup
        _audioManagerScript?.PlayClickOutSound();
    }

    public void SettingsButton()
    {
        Debug.Log("Settings Button Pressed");
        // open the settings menu
        _audioManagerScript?.PlayClickInSound();
    }


    public void SettingsCloseButton()
    {
        Debug.Log("Settings Close Button Pressed");
        // close the settings menu
        _audioManagerScript?.PlayClickOutSound();    
    }

    public void MusicOffButton()
    {
        Debug.Log("Music Off Button Pressed");
        _audioManagerScript?.ToggleMusicVolumeOff();
        _audioManagerScript?.PlayClickInSound();
    }

    public void MusicOnButton()
    {
        Debug.Log("Music On Button Pressed");
        _audioManagerScript?.ToggleMusicVolumeOn();
        _audioManagerScript?.PlayClickInSound();
    }

    public void SFXOffButton()
    {
        Debug.Log("SFX Button Pressed");
        _audioManagerScript?.ToggleSFXVolumeOff();
        _audioManagerScript?.PlayClickInSound();
    }

    public void SFXOnButton()
    {
        Debug.Log("SFX Button Pressed");
        _audioManagerScript?.ToggleSFXVolumeOn();
        _audioManagerScript?.PlayClickInSound();
    }
}
