using UnityEngine;

public class TitleUIManagerScript : MonoBehaviour
{
    private NextLevelScript nextLevelScript;
    private AudioManagerScript audioManagerScript;
    void Start()
    {
        nextLevelScript = FindFirstObjectByType<NextLevelScript>();
        audioManagerScript = FindFirstObjectByType<AudioManagerScript>();
    }

    void Update()
    {
    }

    public void StartButton()
    {
        // switch to the first level scene!
        Debug.Log("Start Button Pressed");
        nextLevelScript?.cueNextLevel(1);
        audioManagerScript?.PlayStartButtonSound();
        audioManagerScript?.PlayLevelMusic();
    }

    public void MenuButton()
    {
        Debug.Log("Menu Button Pressed");
        // switch to a menu scene? cue a popup??? hrm
        audioManagerScript?.PlayMenuButtonSound();
    }
}
