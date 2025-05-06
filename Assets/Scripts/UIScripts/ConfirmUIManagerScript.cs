using UnityEngine;
using UnityEngine.UI;

public class ConfirmUIManagerScript : MonoBehaviour
{
    ///// PRIVATE VARIABLES /////
    private AudioManagerScript _audioManagerScript;
    private GameObject _settingsMenuUI;
    private ToggleVisibilityScript _toggleVisibilityScript;

    ///// GIVEN METHODS /////
    void Start()
    {
        _audioManagerScript = FindFirstObjectByType<AudioManagerScript>();

        _settingsMenuUI = GameObject.FindGameObjectWithTag("SettingsUI");
        if (_settingsMenuUI == null)  // if we're on the title screen, there is no settings menu
        {
            _toggleVisibilityScript = FindFirstObjectByType<ToggleVisibilityScript>();
            return;
        }
        else
        {
            _settingsMenuUI.SetActive(false);    
        }
    }

    ///// BUTTONS /////
    public void ExitYesButton()
    {
        Application.Quit();
        Debug.Log("Application.Quit() called; this will only work in a built game.");
    }

    public void ExitNoButton()
    {
        Destroy(transform.root.gameObject);
        _audioManagerScript?.PlayClickOutSound();
        
        if (_settingsMenuUI == null)
        {
            _toggleVisibilityScript?.ShowVisuals();
            return;
        }
        
        _settingsMenuUI.SetActive(true);
        _audioManagerScript?.PlayClickOutSound();
    }
}
