using UnityEngine;
using UnityEngine.UI;

public class ConfirmUIManagerScript : MonoBehaviour
{
    ///// PRIVATE VARIABLES /////
    private AudioManagerScript _audioManagerScript;
    private GameObject _settingsMenuUI;
    private ToggleVisibilityScript _toggleVisibilityScript;

    ///// METHODS /////
    void Start()
    {
        _audioManagerScript = FindFirstObjectByType<AudioManagerScript>();

        _settingsMenuUI = GameObject.FindGameObjectWithTag("SettingsUI");
        if (_settingsMenuUI == null)  // if we're on the title screen, there is no settings menu
        {
            Debug.Log("Settings Menu Canvas not found!");
            _toggleVisibilityScript = FindFirstObjectByType<ToggleVisibilityScript>();
            return;
        }
        else
        {
            Debug.Log("Settings Menu Canvas found!"); 
            _settingsMenuUI.SetActive(false);    
        }
    }

    public void ExitYesButton()
    {
        Application.Quit();
        Debug.Log("Application.Quit() called; this will only work in a built game.");               // necessary debug log
    }

    public void ExitNoButton()
    {
        Destroy(transform.root.gameObject);
        _audioManagerScript?.PlayClickOutSound();
        if (_settingsMenuUI == null)  // if we're on the title screen, there is no settings menu
        {
            _toggleVisibilityScript?.ShowVisuals();  // show the title screen UI
            return;
        }
        
        _settingsMenuUI.SetActive(true);  // if we're on the level screen, there is a settings menu
        _audioManagerScript?.PlayClickOutSound();
    }
}
