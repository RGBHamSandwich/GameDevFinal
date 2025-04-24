using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;

public class TitleUIManagerScript : MonoBehaviour
{
    public NextLevelScript nextLevelScript;
    void Start()
    {
    }

    void Update()
    {
    }

    public void StartButton()
    {
        // switch to the first level scene!
        Debug.Log("Start Button Pressed");
        nextLevelScript.cueNextLevel(1);   
    }

    public void MenuButton()
    {
        // switch to a menu scene? cue a popup??? hrm
    }
}
