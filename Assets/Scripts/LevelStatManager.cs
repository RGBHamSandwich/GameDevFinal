using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelStatManager : MonoBehaviour
{
    /// PUBLIC ///
    public TextMeshProUGUI StrokeCounter;
    public TextMeshProUGUI StrokeToBeat;
    public TextMeshProUGUI LevelCounter;
    public NextLevelScript nextLevelScript;
    public BallEnvironmentInteractionScript Ball;
    public int strokes = 0;
    public int strokesToBeat = 50;
    public int level = 0;
    
    /// METHODS ///
    void Start()
    {
        StrokeCounter.text = "Stroke " + strokes.ToString();
        StrokeToBeat.text = "To Beat: " + strokesToBeat.ToString();
        LevelCounter.text = "Level: " + level.ToString();
    }

    void Update()
    {

    }

    public void AddStroke()
    {
        strokes++;
        StrokeCounter.text = "Stroke " + strokes.ToString();

        if(strokes > strokesToBeat)
        {
            // screen of failure
            ResetStrokes();
            // reset game
            Ball.ResetBall();
        }
    }

    public void ResetStrokes()
    {
        strokes = 0;
        StrokeCounter.text = "Stroke " + strokes.ToString();
    }

    public void SetStrokesToBeat()  // used when the player gets a new low (?) score
    {
        strokesToBeat = strokes;
        StrokeToBeat.text = "To Beat: " + strokesToBeat.ToString();
    }

    public void AddLevel()
    {
        level++;
        LevelCounter.text = "Level: " + level.ToString();

        if(level > 8)           ///////////// INSERT MAX LEVEL
        {
            // prompt the UIManager to show the "you win!" scene
            SetStrokesToBeat();
        }

        nextLevelScript.cueNextLevel(level);   

    }
}
