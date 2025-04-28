using UnityEngine;

public class TitleUIManagerScript : MonoBehaviour
{
    ///// PRIVATE VARIABLES /////
    private LevelStatManager _levelStatManager;
    private AudioManagerScript _audioManagerScript;
    private MenuUIManagerScript _menuUIManagerScript;

    ///// GIVEN METHODS /////
    void Start()
    {
        _levelStatManager = FindFirstObjectByType<LevelStatManager>();
        _audioManagerScript = FindFirstObjectByType<AudioManagerScript>();
        _menuUIManagerScript = FindFirstObjectByType<MenuUIManagerScript>();
    }

    void Update()
    {
    }

    ///// TITLE SCREEN BUTTONS /////
    public void StartButton()
    {
        // switch to the first level scene!
        Debug.Log("Start Button Pressed");
        _levelStatManager?.StartGame();

        if (_levelStatManager == null)
        {
            Debug.LogError("LevelStatManager not found!");
            return;
        }

        _audioManagerScript?.PlayStartButtonSound();
        _audioManagerScript?.PlayLevelMusic();
    }

    public void MenuButton()
    {
        Debug.Log("Menu Button Pressed");
        _audioManagerScript?.PlayMenuButtonSound();
        // refer to the menu UI manager script to open the menu to be consistent :)
        _menuUIManagerScript?.SettingsButton();
    }
}
