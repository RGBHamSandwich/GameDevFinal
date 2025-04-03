using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    /// PUBLIC ///
    public TextMeshProUGUI StrokeCounter;
    public TextMeshProUGUI StrokeToBeat;
    public BallEnvironmentInteractionScript Ball;
    public int strokes = 0;
    public int strokesToBeat = 50;
    
    /// METHODS ///
    void Start()
    {
        StrokeCounter.text = "Stroke " + strokes.ToString();
        StrokeToBeat.text = "To Beat: " + strokesToBeat.ToString();
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
}
