using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelScript : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {    
    }


    public void cueNextLevel(int level)
    {
        string LevelName = "Level" + level.ToString();
        Debug.Log("cueNextLevel prompted");
        StartCoroutine(NextLevelCoroutine(LevelName));

        if(level == 1)
        {
            AudioManagerScript.instance.PlayLevelMusic();
        }
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
