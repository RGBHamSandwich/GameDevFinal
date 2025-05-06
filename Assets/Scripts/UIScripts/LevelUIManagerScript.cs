using UnityEditor.SearchService;
using UnityEngine;

public class LevelUIManagerScript : MonoBehaviour
{
    ///// PUBLIC VARIABLES /////
    [Header("Menu Prefabs")]
    public GameObject settingsMenu;
    public GameObject confirmMenu;
    ///// PRIVATE VARIABLES /////
    private LevelStatManager _levelStatManager;
    private AudioManagerScript _audioManagerScript;
    private BallController _ballControllerScript;
    private ToggleVisibilityScript _toggleVisibilityScript;
    private SettingsUIManagerScript _settingsUIManagerScript;

    ///// GIVEN METHODS /////
    void Start()
    {
        _levelStatManager = FindFirstObjectByType<LevelStatManager>();
        _audioManagerScript = FindFirstObjectByType<AudioManagerScript>();
        _ballControllerScript = FindFirstObjectByType<BallController>();
        _toggleVisibilityScript = FindFirstObjectByType<ToggleVisibilityScript>();
    }

    ///// MENU BUTTONS /////
    public void ExitGameButton()
    {
        Debug.Log("Exit Game Button Pressed");
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
        _settingsUIManagerScript = FindFirstObjectByType<SettingsUIManagerScript>();
        _settingsUIManagerScript.ENVIRONMENT = _toggleVisibilityScript.levelEnvironment;
        _settingsUIManagerScript.GAMEPLAY = _toggleVisibilityScript.levelGameplay;
        _settingsUIManagerScript.titleScreenUI = _toggleVisibilityScript.titleScreenUI;
        _settingsUIManagerScript.levelUI = _toggleVisibilityScript.levelUI;
        Debug.Log("Settings Menu Opened");
    }

    public void InitializeConfirmMenu()
    {
        Instantiate(confirmMenu, transform.position, Quaternion.identity);
    }
}
