using UnityEditor.SearchService;
using UnityEngine;

public class LevelUIManagerScript : MonoBehaviour
{
    ///// PUBLIC VARIABLES /////
    public GameObject settingsMenu;
    public GameObject exitMenu;
    ///// PRIVATE VARIABLES /////
    private GameObject levelEnvironment;
    private GameObject levelGameplay;
    private GameObject titleScreenUI;
    private GameObject levelUI;
    private LevelStatManager _levelStatManager;
    private AudioManagerScript _audioManagerScript;
    private BallController _ballControllerScript;
    private SettingsUIManagerScript _settingsUIManagerScript;

    ///// GIVEN METHODS /////
    void Start()
    {
        _levelStatManager = FindFirstObjectByType<LevelStatManager>();
        _audioManagerScript = FindFirstObjectByType<AudioManagerScript>();
        _ballControllerScript = FindFirstObjectByType<BallController>();
    }

    void Update()
    { 
    }

    ///// MENU BUTTONS /////
    public void ExitGameButton()
    {
        Debug.Log("Exit Game Button Pressed");
        _audioManagerScript?.PlayClickInSound();
        _ballControllerScript?.FalseCanHitBall();
        Instantiate(exitMenu, transform.position, Quaternion.identity);
    }

    public void ExitYesButton()
    {
        Application.Quit();
        Debug.Log("Application.Quit() called; this will only work in a built game.");               // necessary debug log
        // sound?
    }

    public void ExitNoButton()
    {
        Debug.Log("Exit No Button Pressed");
        // close the "Are you sure?" popup
        _audioManagerScript?.PlayClickOutSound();
    }

    public void SettingsButton()
    {
        if(GameObject.FindGameObjectWithTag("SettingsUI") != null)
        {
            Debug.Log("Settings Menu already exists, closing it.");
            _settingsUIManagerScript.SettingsCloseButton();
            return;
        }

        Debug.Log("Settings Button Pressed");
        _audioManagerScript?.PlayClickInSound();
        _ballControllerScript?.FalseCanHitBall();
        HideAllVisuals();       // initializes values for levelEnvironment, levelGameplay, titleScreenUI, and levelUI
        InitalizeSettingsMenu();    // gives them to the settings menu
    }

    public void HideAllVisuals()
    {
        if (_levelStatManager.level <= 0)
        { 
            titleScreenUI = GameObject.FindGameObjectWithTag("TitleScreenUI");
            Canvas titleScreenCanvas = titleScreenUI.GetComponent<Canvas>();
            titleScreenCanvas.enabled = false;
        }

        levelUI = GameObject.FindGameObjectWithTag("LevelUI");
        Canvas levelUICanvas = levelUI.GetComponent<Canvas>();
        levelUICanvas.enabled = false; 

        
        levelEnvironment = GameObject.FindGameObjectWithTag("ENVIRONMENT");
        levelEnvironment.SetActive(false);

        levelGameplay = GameObject.FindGameObjectWithTag("GAMEPLAY");
        levelGameplay.SetActive(false);
    }

    public void InitalizeSettingsMenu()
    {
        Instantiate(settingsMenu, transform.position, Quaternion.identity);
        _settingsUIManagerScript = FindFirstObjectByType<SettingsUIManagerScript>();
        _settingsUIManagerScript.ENVIRONMENT = levelEnvironment;
        _settingsUIManagerScript.GAMEPLAY = levelGameplay;
        _settingsUIManagerScript.titleScreenUI = titleScreenUI;
        _settingsUIManagerScript.levelUI = levelUI;
        Debug.Log("Settings Menu Opened");
    }

}
