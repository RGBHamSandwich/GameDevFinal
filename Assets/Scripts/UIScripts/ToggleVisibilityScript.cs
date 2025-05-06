using UnityEngine;

public class ToggleVisibilityScript : MonoBehaviour
{
    ///// PUBLIC VARIABLES /////
    [Header("Auto-Assigned Values")]
    public GameObject levelEnvironment;
    public GameObject levelGameplay;
    public GameObject titleScreenUI;
    public GameObject levelUI;
    ///// PRIVATE VARIABLES /////
    private LevelStatManager _levelStatManager;
    private BallController _ballControllerScript;
    private PlayerMovementController _playerMovementControllerScript;

    void Start()
    {
        _levelStatManager = FindFirstObjectByType<LevelStatManager>();
        _ballControllerScript = FindFirstObjectByType<BallController>();
        _playerMovementControllerScript = FindFirstObjectByType<PlayerMovementController>();
        FindLevelObjects();
    }

    public void FindLevelObjects()
    {
        levelEnvironment = GameObject.FindGameObjectWithTag("ENVIRONMENT");
        levelGameplay = GameObject.FindGameObjectWithTag("GAMEPLAY");
        titleScreenUI = GameObject.FindGameObjectWithTag("TitleScreenUI");
        levelUI = GameObject.FindGameObjectWithTag("LevelUI");
    }

    public void HideVisuals()
    {
        FindLevelObjects();
        if (_levelStatManager.level <= 0)
        {
            Canvas titleScreenCanvas = titleScreenUI.GetComponent<Canvas>();
            titleScreenCanvas.enabled = false;
        }

        Canvas levelUICanvas = levelUI.GetComponent<Canvas>();
        levelUICanvas.enabled = false; 

        levelEnvironment.SetActive(false);

        levelGameplay.SetActive(false);
        _ballControllerScript?.FalseCanHitBall();
    }

    public void ShowVisuals()
    {
        if (_levelStatManager.level <= 0)
        {
            Canvas titleScreenCanvas = titleScreenUI.GetComponent<Canvas>();
            titleScreenCanvas.enabled = true;
        }

        Canvas levelUICanvas = levelUI.GetComponent<Canvas>();
        levelUICanvas.enabled = true;

        levelEnvironment.SetActive(true);

        levelGameplay.SetActive(true);
        _ballControllerScript?.TrueCanHitBall();

        _playerMovementControllerScript?.MovePlayer(); // moves the player in case it was interrupted
    }

}
