using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallNextLevelScript : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {    
    }


    public void BallNextLevel(int level)
    {
        string LevelName = "Level" + level.ToString();
        Debug.Log("BallNextLevel function prompted");
        StartCoroutine(NextLevelCoroutine(LevelName));
    }

    private IEnumerator NextLevelCoroutine(String level)
    {
        Debug.Log("NextLevelCoroutine prompted");
        // Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(level, LoadSceneMode.Single);
        this.transform.position = Vector3.zero;
    
        // EOnPlayerArrive?.Invoke();       // add an animation?
        yield return null;
    }

    public void BallRestart()
    {

    }
}
