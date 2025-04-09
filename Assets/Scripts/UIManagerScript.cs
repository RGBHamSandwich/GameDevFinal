using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;

public class UIManagerScript : MonoBehaviour
{
    public BallNextLevelScript ballNextLevelScript;
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
        ballNextLevelScript.BallNextLevel(1);   
    }

    public void MenuButton()
    {
        // switch to a menu scene? cue a popup??? hrm
    }
}
