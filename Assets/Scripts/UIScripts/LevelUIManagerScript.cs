using UnityEditor.SearchService;
using UnityEngine;

public class LevelUIManagerScript : MonoBehaviour
{
    ///// PUBLIC VARIABLES /////
    [Header("Menu Prefabs")]
    public GameObject settingsMenu;
    public GameObject confirmMenu;
    ///// PRIVATE VARIABLES /////
    private AudioManagerScript _audioManagerScript;
    private ToggleVisibilityScript _toggleVisibilityScript;

    ///// GIVEN METHODS /////
    void Start()
    {
        _audioManagerScript = FindFirstObjectByType<AudioManagerScript>();
        _toggleVisibilityScript = FindFirstObjectByType<ToggleVisibilityScript>();
    }

    ///// MENU BUTTONS /////
    public void ExitGameButton()
    {
        _audioManagerScript?.PlayClickInSound();
        _toggleVisibilityScript?.HideVisuals();
        InitializeConfirmMenu();
    }

    public void SettingsButton()
    {
        _audioManagerScript?.PlayClickInSound();
        _toggleVisibilityScript?.HideVisuals();
        InitalizeSettingsMenu();
    }


    public void InitalizeSettingsMenu()
    {
        Instantiate(settingsMenu, transform.position, Quaternion.identity);
    }

    public void InitializeConfirmMenu()
    {
        Instantiate(confirmMenu, transform.position, Quaternion.identity);
    }
}
