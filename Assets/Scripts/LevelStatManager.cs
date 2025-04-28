using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelStatManager : MonoBehaviour
{
    ///// PUBLIC VARIABLES /////
    [Header("UI Elements")]
    private TextMeshProUGUI StrokeCounter;
    private TextMeshProUGUI StrokeToBeat;
    private TextMeshProUGUI LevelCounter;
    [Header("Level Stats")]
    public int strokes = 0;
    public int strokesToBeat = 20;
    public int level = 0;
    public static LevelStatManager instance;
    
    ///// PRIVATE VARIABLES /////    
    private BallEnvironmentInteractionScript _ball;

    ///// ANIMATION /////
    public delegate void OnPlayerCry();
    public static event OnPlayerCry EOnPlayerCry;
    
    ///// GIVEN METHODS /////    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        _ball = FindFirstObjectByType<BallEnvironmentInteractionScript>();

        FindLevelText();
        // StrokeCounter.text = "Stroke " + strokes.ToString();
        // StrokeToBeat.text = "To Beat: " + strokesToBeat.ToString();
        // LevelCounter.text = "Level: " + level.ToString();
    }

    void Update()
    {
    }

    ///// COROUTINES /////
    private IEnumerator NextLevelCoroutine(System.String level)
    {
        // Debug.Log("NextLevelCoroutine prompted");
        SceneManager.LoadScene(level, LoadSceneMode.Single);
        
        _ball = FindFirstObjectByType<BallEnvironmentInteractionScript>();
        _ball?.ResetBall();
        // FindLevelText();
        yield return null;
    } 

    ///// ~ fancy ~  METHODS /////   
    public void StartGame()
    {
        Debug.Log("StartGame prompted");
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
        level = 1;
        strokes = 0;
        FindLevelText();
    }

    public void cueNextLevel(int level)
    {
        if (level <= 0)
        {
            StartCoroutine(NextLevelCoroutine("TitleScene"));   // reset the game
            return;
        }
        string LevelName = "Level" + level.ToString();
        // Debug.Log("cueNextLevel prompted");
        StartCoroutine(NextLevelCoroutine(LevelName));
    }

    public void FindLevelText()
    {
        StrokeCounter = GameObject.Find("StrokeCounter")?.GetComponent<TextMeshProUGUI>();
        if (StrokeCounter == null)
        {
            Debug.LogError("StrokeCounter TextMeshProUGUI not found!");
        }
        else
        {
            StrokeCounter.text = "Stroke " + strokes.ToString();
        }

        StrokeToBeat = GameObject.Find("StrokeToBeat")?.GetComponent<TextMeshProUGUI>();
        if (StrokeToBeat == null)
        {
            Debug.LogError("StrokeToBeat TextMeshProUGUI not found!");
        }
        else
        {
            StrokeToBeat.text = "To Beat: " + strokesToBeat.ToString();
        }

        LevelCounter = GameObject.Find("LevelCounter")?.GetComponent<TextMeshProUGUI>();
        if (LevelCounter == null)
        {
            Debug.LogError("LevelCounter TextMeshProUGUI not found!");
        }
        else
        {
            LevelCounter.text = "Level: " + level.ToString();
        }
    }

    public void AddStroke()
    {
        strokes++;
        StrokeCounter.text = "Stroke " + strokes.ToString();

        if(strokes > strokesToBeat)
        {
            TooManyStrokes();
        }
    }

    public void TooManyStrokes()
    {
        // implement screen of failure
        ResetStrokes();
        cueNextLevel(0);
        EOnPlayerCry?.Invoke();
    }

    public void ResetStrokes()
    {
        strokes = 0;
        StrokeCounter.text = "Stroke " + strokes.ToString();
    }

    public void SetStrokesToBeat()
    {
        // implement setting a new low score !!
        strokesToBeat = strokes;
        StrokeToBeat.text = "To Beat: " + strokesToBeat.ToString();
    }

    public void AddLevel()
    {
        if(level == 0)
        {
            // we're on the title screen, so we don't want to increment the level
            return;
        }

        level++;
        LevelCounter.text = "Level: " + level.ToString();

        if(level > 4) // max level
        {
            // prompt the UIManager to show the "you win!" scene
            SetStrokesToBeat();
            return;
        }
        cueNextLevel(level);   
    }
}
