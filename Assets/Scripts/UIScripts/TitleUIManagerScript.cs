using UnityEngine;

public class TitleUIManagerScript : MonoBehaviour
{
    ///// PRIVATE VARIABLES /////
    private LevelStatManager _levelStatManager;
    private AudioManagerScript _audioManagerScript;
    private LevelUIManagerScript _menuUIManagerScript;

    ///// GIVEN METHODS /////
    void Start()
    {
        _levelStatManager = FindFirstObjectByType<LevelStatManager>();
        _audioManagerScript = FindFirstObjectByType<AudioManagerScript>();
        _menuUIManagerScript = FindFirstObjectByType<LevelUIManagerScript>();
    }

    ///// TITLE SCREEN BUTTONS /////
    public void StartButton()
    {
        _levelStatManager?.StartGame();
        _audioManagerScript?.PlayStartButtonSound();
        _audioManagerScript?.PlayLevelMusic();
    }

    public void MenuButton()
    {
        _audioManagerScript?.PlayMenuButtonSound();
        _menuUIManagerScript?.SettingsButton();
    }
}
