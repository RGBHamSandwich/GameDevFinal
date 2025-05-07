using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelStatManager : MonoBehaviour
{
    ///// PUBLIC VARIABLES /////
    [Header("Level Stats")]
    public int numberOfLevels = 1;
    public int strokes = 0;
    public int strokesToBeat = 3;
    public int level = 0;
    [Tooltip("How tall the menu bar is in pixels.")]
    public float _levelStatUIHeight = 50f;
    [Tooltip("Set to true to restart the game's low score.")]
    public bool resetLowScore = false;
    public static LevelStatManager instance;
    
    ///// PRIVATE VARIABLES /////    
    private TextMeshProUGUI StrokeCounter;
    private TextMeshProUGUI StrokeToBeat;
    private TextMeshProUGUI LevelCounter;
    private BallEnvironmentInteractionScript _ball;
    private AudioManagerScript _audioManagerScript;
    private ToggleVisibilityScript _toggleVisibilityScript;
    
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
            return;
        }

        if (resetLowScore)
        {
            PlayerPrefs.SetInt("strokesToBeat", strokesToBeat);  // overwrite with current field value
            PlayerPrefs.Save();
        }
        else
        {
            // Load saved value, or default to 30 if none exists
            strokesToBeat = PlayerPrefs.GetInt("strokesToBeat", 30);
        }
    }


    void Start()
    {
        _ball = FindFirstObjectByType<BallEnvironmentInteractionScript>();
        _audioManagerScript = FindFirstObjectByType<AudioManagerScript>();
        _toggleVisibilityScript = FindFirstObjectByType<ToggleVisibilityScript>();

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

        _toggleVisibilityScript?.FindLevelObjects();
        _toggleVisibilityScript?.ShowVisuals();             // if visuals fuck up, it's this line

        yield return null;
    } 

    ///// GIVEN METHODS /////
    public void OnDestroy()
    {
        PlayerPrefs.SetInt ("strokesToBeat", strokesToBeat);
        PlayerPrefs.Save();
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

    public void cueNextLevel()
    {
        if(level == 0)
        {
            // we're on the title screen, so we don't want to increment the level
            // to start the game, the Start Button accesses StartGame() directly
            ResetText();
            return;
        }

        level++;
        LevelCounter.text = "Level: " + level.ToString();

        if(level > numberOfLevels)
        {
            if(strokes <= strokesToBeat)
            {
                SetStrokesToBeat(); 
                cueVictoryScene();
                return;
            }

            TooManyStrokes();
            return;
        }   

        if (level < 0)
        {
            cueTitleScene();
            return;
        }

        string LevelName = "Level" + level.ToString();
        StartCoroutine(NextLevelCoroutine(LevelName));
    }

    public void cueTitleScene()
    {
        ResetText();
        _audioManagerScript?.PlayTitleMusic();
        StartCoroutine(NextLevelCoroutine("TitleScene"));
    }
    
    public void cueVictoryScene()
    {
        _audioManagerScript?.PlayTitleMusic();
        StartCoroutine(NextLevelCoroutine("Victory"));
    }

    public void cueDefeatScene()
    {
        _audioManagerScript?.PlayTitleMusic();
        StartCoroutine(NextLevelCoroutine("Defeat"));
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
        if(level != 0)
        {
            cueDefeatScene();
        }
        ResetText();
        // EOnPlayerCry?.Invoke();
    }

    public void ResetText()
    {
        strokes = 0;
        StrokeCounter.text = "Stroke " + strokes.ToString();

        level = 0;
        LevelCounter.text = "Level: " + level.ToString();

        FindLevelText();
    }

    public void SetStrokesToBeat()
    {
        strokesToBeat = strokes;
        StrokeToBeat.text = "To Beat: " + strokesToBeat.ToString();
    }
}
