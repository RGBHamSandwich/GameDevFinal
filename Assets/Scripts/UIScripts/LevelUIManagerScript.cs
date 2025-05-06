using UnityEditor.SearchService;
using UnityEngine;

public class LevelUIManagerScript : MonoBehaviour
{
    ///// PUBLIC VARIABLES /////
    public GameObject settingsMenu;
    ///// PRIVATE VARIABLES /////
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
        Debug.Log("Exit Button Pressed");
        _audioManagerScript?.PlayClickInSound();

        if(_levelStatManager.level <= 0)    // if we're on the title screen
        {
            // exit the game
            Application.Quit();
            Debug.Log("Application.Quit() called from Title Screen");
            return;
        }
        Debug.Log("Level: " + _levelStatManager.level.ToString());
        _ballControllerScript?.FalseCanHitBall();
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

    public void SettingsButton()
    {
        Debug.Log("Settings Button Pressed");
        _audioManagerScript?.PlayClickInSound();
        _ballControllerScript?.FalseCanHitBall();
        
        if (_levelStatManager.level <= 0)
        { 
            GameObject titleScreenUI = GameObject.FindGameObjectWithTag("TitleScreenUI");
            Canvas titleScreenCanvas = titleScreenUI.GetComponent<Canvas>();
            titleScreenCanvas.enabled = false;
        }

        GameObject levelUI = GameObject.FindGameObjectWithTag("LevelUI");
        Canvas levelUICanvas = levelUI.GetComponent<Canvas>();
        levelUICanvas.enabled = false; 

        GameObject levelEnvironment = GameObject.FindGameObjectWithTag("ENVIRONMENT");
        levelEnvironment.SetActive(false);

        GameObject levelGameplay = GameObject.FindGameObjectWithTag("GAMEPLAY");
        levelGameplay.SetActive(false);

        if(GameObject.FindGameObjectWithTag("SettingsUI") != null)
        {
            Debug.Log("Settings Menu already exists, closing it.");
            _settingsUIManagerScript.SettingsCloseButton();
            return;
        }

        Instantiate(settingsMenu, transform.position, Quaternion.identity);
        _settingsUIManagerScript = FindFirstObjectByType<SettingsUIManagerScript>();
        _settingsUIManagerScript.ENVIRONMENT = levelEnvironment;
        _settingsUIManagerScript.GAMEPLAY = levelGameplay;
        Debug.Log("Settings Menu Opened");

    }
}
